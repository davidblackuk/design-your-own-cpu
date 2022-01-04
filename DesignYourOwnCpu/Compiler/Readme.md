# The compiler and its syntax

Before leaping into the syntax of the language here's a demo program that averages a series of numbers, this should give you a feel for how a program hangs together.

```pascal
{ Average a series of numbers }
{ enter one poitive number per line and terminate with a zero }

var sum, number, current;
  begin
    sum := 0;           { sum of all non-zero values read so far }
    number := 0;        { number of values read }
    current := read;    { reads the first int }

    while current <> 0 do
    begin
        sum := sum + t;
        number := number + 1;
        current := read
    end;

    write(number);
    write(sum);

    if number <> 0 then 
        write(sum / number); { the average valiue }
end.

```


## Syntax

The following railroad diagrams show the syntax of the version 0.1 of the Pascal-ish compiler. Many simplyfying assumptions have been made to bootstrap the compiler that may be revisited later.

In particular there are no notions of

+ Procedures or functions
+ the `else` or `elif` components of an `if` statement;
+ signed numbers (all constants are unsigned 16 bit integers) 

Almost all of the diagrams here will be implemented as methods in the recursive descent parser. However some items like the comments are just there for completeness as they are stripped during the lexical analysis phase.


### program

A program consists of an optional declaration of variables,  a  `block` and is terminated by a `.` 

![program definition](Images/program.svg)


### block

A `block` is either a single statement, or the keyword `BEGIN` followed by a semicolon separated set of nested blocks and a terminating `END` keyword.

![program definition](Images/block.svg)


### idList

An `idList` consists of a comma separated list of identifiers, terminated with a "`;`". Variables in the language are currently 16 bit integers, this maps onto the register size of our CPU design. 

![program definition](Images/idlist.svg)


### statement

Statements form the main meat of the source program. They are composed of 

+ An assignment statement, that assigns the result of an expression to a variable
+ The `write` function that outputs an expression to the console
+ An `if` statement that optionally executes a block of code based on a condition
+ A `while` loop that iterates while a particular condition is true 

![program definition](Images/statement.svg)


### comparison

Comparisons are composed of two expressions seperated by a `relop` (a relational operator such as '`>`', '`<`' etc)

![program definition](Images/comparison.svg)


### expression

For an expression we have a `term`, or a series of `term`s separated by `addop`s ("`+`" or "`-`").

![program definition](Images/expression.svg)


### addop

An addop is either a plus or minus.

![program definition](Images/addop.svg)



### term
For an term we have a `factor`, or a series of `factor`s separated by `mulop`s ("`*+*`" or "`/`"). 

Note due to the recursive nature of this, the `term` consists of `factor`s separated by `mulops`. `Factor`s in turn can hold `expression`s, `expression`s are `term`s separated by `addops`.

This splitting of the addition and multiplication  enforces the rule that `/` or `*` are processed before `+` or `-`. This enforces operator president and implements BODMAS.

![program definition](Images/term.svg)



### mulop

A `mulop` is either a star or a slash

![program definition](Images/mulop.svg)


### factor

A factor is either

+ An identifier
+ A constant value
+ the read function (that returns an integer read from the console)
+ a bracket delimited expression

![program definition](Images/factor.svg)

 
### relop

THe supported relational operators are:

![program definition](Images/relop.svg)

### identifier

Identifiers are composed of one or more upper or lower case letters. Identifiers are not case sensitive in the language.

![program definition](Images/identifier.svg)

 
### constant

Contsants are composed of one or more decimal digits.

![program definition](Images/constant.svg)

### comment

A comment is any series of characters inside a pair of curly braces. Comments **do not nest**

![program definition](Images/comment.svg)
 