using System;
using Compiler.Ast;
using Compiler.Ast.Nodes;
using Compiler.Exceptions;
using Compiler.LexicalAnalysis;
using Microsoft.Extensions.Logging;

namespace Compiler.SyntacticAnalysis
{
    internal class SyntaxAnalyser : ISyntaxAnalyser
    {
        private readonly ILexer lexer;
        private readonly IAbstractSyntaxTree ast;
        private readonly ISymbolTable symbolTable;
        private readonly ILogger<SyntaxAnalyser> logger;
        private Lexeme currentLexeme;

        private readonly LexemeSet startExpression = LexemeSet.From(LexemeType.LBracket, LexemeType.Identifier, LexemeType.Constant, LexemeType.Read); 
        private readonly LexemeSet startBlock = LexemeSet.From(LexemeType.Begin, LexemeType.Identifier, LexemeType.If, LexemeType.While, LexemeType.Write); 
        
        public SyntaxAnalyser(ILexer lexer, IAbstractSyntaxTree ast, ISymbolTable symbolTable, ILogger<SyntaxAnalyser> logger)
        {
            this.lexer = lexer;
            this.ast = ast;
            this.symbolTable = symbolTable;
            this.logger = logger;
        }
        
        public void Scan()
        {
            NextLexeme();
            Program();
        }
        
        private void Program()
        {
            // check if variables are being defined
            if (currentLexeme.Type == LexemeType.Var)
            {
                // skip the VAR keyword
                NextLexeme();
                
                // read a comma seperated list of IDS, terminated by a semicolon
                IdList(LexemeSet.From(LexemeType.Begin, LexemeType.Semicolon, LexemeType.Dot));
            }

            do
            {
                ast.Root = new BlockNode();
                Block(LexemeSet.From(LexemeType.Dot), ast.Root);
                // wile we are not at the terminating period
            } while (!CheckOrSkip(LexemeType.Dot, LexemeSet.Empty));

            ast.Display();

        }

        
        private void IdList(LexemeSet stopSet )
        {
            bool complete;
            do
            {
                // we expect an identifier
                if (CheckOrSkip(LexemeSet.From(LexemeType.Identifier), stopSet+ LexemeType.Semicolon + LexemeType.Comma))
                {
                    // we got an identifier add it to the symbol table
                    symbolTable.Declare(currentLexeme.Value.ToString());
                    NextLexeme();
                }

                // move past the training comma or the terminating semi colon
                CheckOrSkip(LexemeSet.From(LexemeType.Semicolon, LexemeType.Comma), stopSet + LexemeType.Identifier + LexemeType.Comma);
                
                // were are complete if the next token _isn't_ a comma or another identifier
                complete = currentLexeme.Type is not (LexemeType.Comma or LexemeType.Identifier);

                // if we are on a comma step over it
                StepOverCurrentLexemeIfItsA(LexemeType.Comma);
                
            } while (!complete);
            
            StepOverCurrentLexemeIfItsA(LexemeType.Semicolon);
        }

        
        private void Block(LexemeSet stopSet, BlockNode containingBlock)
        {
            logger.LogDebug($"Block: {currentLexeme} [{stopSet}]");
            CheckOrSkip(stopSet+ startBlock, stopSet + startBlock);
            if (currentLexeme.Type == LexemeType.Begin)
            {
                NextLexeme();

                Block(stopSet + LexemeType.Semicolon + LexemeType.End, containingBlock);
                
                CheckOrSkip(LexemeSet.From(LexemeType.Semicolon, LexemeType.End), stopSet + LexemeType.Semicolon + startBlock);
                
                while ((startBlock + LexemeType.Semicolon).Contains(currentLexeme.Type))
                {
                    StepOverCurrentLexemeIfItsA(LexemeType.Semicolon);
                    Block(stopSet+ LexemeType.Semicolon + LexemeType.End, containingBlock);
                    CheckOrSkip(LexemeSet.From(LexemeType.Semicolon, LexemeType.End), stopSet + LexemeType.Semicolon + startBlock);
                }
                StepOverCurrentLexemeIfItsA(LexemeType.End);
            }
            else
            {
               // StepOverCurrentLexemeIfItsA(LexemeType.Semicolon);
                var statement = Statement(stopSet);
                if (statement != null)
                {
                    containingBlock.Nodes.Add(statement);
                }
            }
        }

        private AstNode Statement(LexemeSet stopSet)
        {
            AstNode res = null;
            logger.LogDebug($"Statement: {currentLexeme} [{stopSet}]");
            if (CheckOrSkip(stopSet + startBlock, stopSet))
            {
                switch (currentLexeme.Type)
                {
                    case LexemeType.Identifier:
                        string identifier = currentLexeme.Value.ToString();
                        // check id exists in the symbol table in the semantic analysis pass
                        NextLexeme();

                        if (CheckOrSkip(LexemeType.Assign, stopSet+ startExpression))
                        {
                            NextLexeme();
                            res = new AssignmentNode()
                            {
                                Identifier = identifier,
                                Expression =  Expression(stopSet),
                            };
                        }
                        break;
                    case LexemeType.If:
                        NextLexeme();
                        
                        IfNode ifNode = new IfNode();
                        
                        ifNode.Comparison = Comparison(stopSet + LexemeType.Then + startBlock - LexemeType.Identifier);
                        if (CheckOrSkip(LexemeType.Then, stopSet + startBlock))
                        {
                            NextLexeme();
                        }
                        Block(stopSet, ifNode.Statements);
                        res = ifNode;
                        break;
                    case LexemeType.While:
                        NextLexeme();
                        
                        WhileNode whileNode = new WhileNode();
                        whileNode.Comparison = Comparison(stopSet + LexemeType.Do + startBlock - LexemeType.Identifier);
                        if (CheckOrSkip(LexemeType.Do, stopSet + startBlock))
                        {
                            NextLexeme();
                        }
                        Block(stopSet, whileNode.Statements);
                        res = whileNode;
                        break;
                    case LexemeType.Write:
                        NextLexeme();
                        if (CheckOrSkip(LexemeType.LBracket, stopSet + LexemeType.RBracket + startExpression))
                        {
                            NextLexeme();
                        }

                        WriteNode writeNode = new WriteNode();
                        writeNode.Expression = Expression(stopSet + LexemeType.RBracket);
                        if (CheckOrSkip(LexemeType.RBracket, stopSet))
                        {
                            NextLexeme();
                        }

                        res = writeNode;
                        break;
                }
            }

            return res;
        }

        private PairNode Comparison(LexemeSet stopSet)
        {
            logger.LogDebug($"Comparison: {currentLexeme} [{stopSet}]");
            
            // left
            var left = Expression(stopSet + LexemeType.RelOp);
            object operation = null;
            if (CheckOrSkip(LexemeType.RelOp, stopSet + startExpression))
            {
                operation = currentLexeme.Value;
                NextLexeme();
            }
            
            // right
            var right = Expression(stopSet + startBlock - LexemeType.Identifier);

            return new PairNode()
            {
                Left = left,
                Right = right,
                Operator = operation.ToString()
            };
        }

  
        private AstNode Expression(LexemeSet stopSet)
        {
            logger.LogDebug($"Expression: {currentLexeme} [{stopSet}]");
            
            // left
            var res = Term(stopSet + LexemeType.AddOp);
            var currentOp = currentLexeme.Value;
            while (currentLexeme.Type == LexemeType.AddOp)
            {
                logger.LogDebug($"Expression: {currentLexeme} [{stopSet}]");
                NextLexeme();
                
                var nextTerm = Term(stopSet + LexemeType.AddOp);
                res = new ExpressionNode { Left = res, Right = nextTerm, Operator = currentOp.ToString() };

            }

            return res;
        }     
        
        private AstNode Term(LexemeSet stopSet)
        {
            logger.LogDebug($"Term: {currentLexeme} [{stopSet}]");
            var res = Factor(stopSet + LexemeType.MulOp);
            var currentOp = currentLexeme.Value;
            logger.LogDebug($"first factor: {res}");
            while (currentLexeme.Type == LexemeType.MulOp)
            {
                logger.LogDebug($"Term: {currentLexeme} [{stopSet}]");
                NextLexeme();
                var nextFactor = Factor(stopSet + LexemeType.MulOp);
                logger.LogDebug($"next factor: {nextFactor}");
                res = new PairNode() { Left = res, Right = nextFactor, Operator = currentOp.ToString()};
            }

            return res;
        }
        
        private AstNode Factor(LexemeSet stopSet)
        {
            logger.LogDebug($"Factor: {currentLexeme} [{stopSet}]");
            if (CheckOrSkip(startExpression, stopSet))
            {
                switch (currentLexeme.Type)
                {
                    case LexemeType.Identifier:
                        // TODO: check that id exists in the symbol table in the semantic analysis pass
                        var name = currentLexeme.Value.ToString();
                        NextLexeme();
                        return new IdentifierNode(name);
                    case LexemeType.Constant:
                        var value = (ushort)currentLexeme.Value;
                        NextLexeme();
                        return new ConstantNode(value);
                    case LexemeType.LBracket:
                        NextLexeme();
                        var res = Expression((stopSet + LexemeType.RBracket));
                        if (CheckOrSkip(LexemeType.RBracket, stopSet))
                        {
                            NextLexeme();
                        }
                        return res;
                    case LexemeType.Read:
                        NextLexeme();
                        return new ReadNode();
                }
            }

            throw new CompilerException($"Could not parse factor from {currentLexeme}");
        }
        
        private bool CheckOrSkip(LexemeType expected, LexemeSet skipList)
        {
            return CheckOrSkip(LexemeSet.From(expected), skipList);
        }
        
        private bool CheckOrSkip(LexemeSet okSet, LexemeSet skipList)
        {
            if (okSet.Contains(currentLexeme.Type))
            {
                return true;
            }

            // Error message
            logger.LogDebug($"Line {currentLexeme.LineNumber}: Found token: '{currentLexeme.Type}', Expected (one of): [{okSet}]");

            while (!skipList.Contains(currentLexeme.Type))
            {
                logger.LogDebug("skipped lexeme " + currentLexeme.Type);
                NextLexeme();
            }

            return false;
        }

        private void StepOverCurrentLexemeIfItsA(LexemeType lexemeType)
        {
            if (currentLexeme.Type == lexemeType)
            {
                NextLexeme();
            }
        }
        
        private void NextLexeme()
        {
            currentLexeme = lexer.GetLexeme();
        }


    }
}