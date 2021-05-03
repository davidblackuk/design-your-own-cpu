﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks.Sources;
using Shared;

namespace Assembler.Instructions
{
    public class BranchInstruction : Instruction, IInstruction
    {
        private readonly string instructionName;

        
        /// <summary>
        /// Constructs a new instance of a branch instruction. This is a base class that is
        /// used to handle all common tasks processing the instruction (ie most of it)
        /// </summary>
        /// <param name="instructionName">The instruction name (beq, bra etc)</param>
        /// <param name="opCode">Op code for this instruction</param>
        public BranchInstruction(string instructionName, byte opCode)
        {
            this.instructionName = instructionName;
            this.OpCode = opCode;
        }

        /// <summary>
        /// Parses the branch instruction and 
        /// </summary>
        /// <param name="source"></param>
        public void Parse(string source)
        {
            if (IsLabel(source))
            {
                RecordSymbolForResolution(source);
            }
            else
            {
                ParseValue(source);
            }
        }
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            string address = $"0x{ByteHigh:X2}{ByteLow:X2}";
            string postfix = RequresSymbolResolution ? $" => {Symbol}" : "";
            return $"{instructionName} {address}{postfix}";
                    
        }
    }
    
    public class BranchEqualInstruction: BranchInstruction
    {
        public const string InstructionName = "beq";

        public BranchEqualInstruction(): base(InstructionName, OpCodes.BranchEqual)
        {
            
        }
    }
    public class BranchGreaterThanInstruction: BranchInstruction
    {
        public const string InstructionName = "bgt";

        public BranchGreaterThanInstruction(): base(InstructionName, OpCodes.BranchGreaterThan)
        {
            
        }
    }
    public class BranchLessThanInstruction: BranchInstruction
    {
        public const string InstructionName = "blt";

        public BranchLessThanInstruction(): base(InstructionName, OpCodes.BranchLessThan)
        {
            
        }
    }
    public class BranchAlwaysInstruction: BranchInstruction
    {
        public const string InstructionName = "bra";

        public BranchAlwaysInstruction(): base(InstructionName, OpCodes.Branch)
        {
            
        }
    }
}