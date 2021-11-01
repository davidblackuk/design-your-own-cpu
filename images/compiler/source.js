// v0.1 language spec

// program
ComplexDiagram(
    Optional(
        Sequence('VAR', NonTerminal('idList'))),
    Sequence(
        NonTerminal('block'),
        Terminal('.')
    )
)

// block
ComplexDiagram(
    Choice(0,
        NonTerminal('statement'),

        Sequence(
            'BEGIN',

            OneOrMore(
                NonTerminal('block'),
                ';'
            ),
            'END'
        )
    )
)

// idlist
ComplexDiagram(
    Sequence(
        OneOrMore(
            NonTerminal('identifier'),
            ','
        ),
        ';'
    )
)

// statement
ComplexDiagram(

    Choice(0,
        Sequence(NonTerminal('identifier'), ':=', NonTerminal('expression')),
        Sequence('write', '(', NonTerminal('expression'), ')'),
        Sequence('IF', NonTerminal('comparison'), 'THEN', NonTerminal('block')),
        Sequence('WHILE', NonTerminal('comparison'), 'DO', NonTerminal('block')),
    )
)

// comparison
ComplexDiagram(
    Sequence(
        Sequence(NonTerminal('expression'), NonTerminal('relop'), NonTerminal('expression')),
    )
)


// expression
ComplexDiagram( 
   Sequence(NonTerminal('term'), ZeroOrMore( Sequence(NonTerminal('addop'), NonTerminal('term')))),
)

// addop
ComplexDiagram(
  
    Choice(0, '+','-')
 )
 

// term
ComplexDiagram( 
    Sequence(NonTerminal('factor'), ZeroOrMore( Sequence(NonTerminal('mulop'), NonTerminal('factor')))),
 )


 // mulop
 ComplexDiagram(
  
    Choice(0, '*','/')
 )
 
 // factor
 ComplexDiagram(
  
    Choice(0, NonTerminal('identifier'), NonTerminal('constant'), 'read',
    Sequence('(',NonTerminal('expression'),')')
    )
 )
 

 // relop
ComplexDiagram(
    Choice(0, '<', '<=', '<>', '>', '>=', '='
    )
)

 
// identifier
ComplexDiagram(OneOrMore('upper or lowercase letter'))

// constant
ComplexDiagram(OneOrMore('decimal digit'))


// comment
ComplexDiagram(Sequence('{', ZeroOrMore('any character but \'}\''), '}'))