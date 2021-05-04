# Simple assembler and emulator

A implementation of the assembler and emulator for the simple processor design presented in the
[Gary explains videos][1] on youtube.

Specific videos:
 - [Design your own instruction format][3]
 - [Write Your Own Assembler for Your Own CPU][4]
 - [As yet un released video][5]

Gary's [orininal source][2] is availble on Github


## Assembly language

Asembly language is written one instruction per line. Lines starting with the commen character `#` are ignored. Any lines with training comments 

    LD R1, 65535 # set all bits to 1

will have the training comment removed before parsing.

Instructions are parsed in a case insensitive manner. The following two lines are the same

    LD R1, 0xFE03
    ld r1, 0xfe03




Numeric constants can be decimal, hexadecimal, or octal.

| Base | Example | Description |
| --- | --- | --- |
| 16 | 0x3421 | Prefixed with 0x and containing the digits `0 -f` (case insensitive)
| 10 | 32768 | Decimal numbers have no prefix, first digit cannot be 0, digits are `0 - 9`
| 8 | 03422 | Octal numbers are prefixed with a 0, digits are `0 - 7`

## Registers

There are eight general purpose registers `r0` ... `r7`, available to user code. Register `r8` is the program counter
and `r9` the stack pointer, neither of which can be accessed by user code. So there.

## Instruction set

Instruction definitions. All instructions are 32 bits long, unsed bytes set to 0. Registers are 16 bit.


|   Instruction   | Opcode | Register | Data H | Data L | Description |
|-----------------|--------|----------|--------|--------|-------------|
| **Load** |
| `LD R1, 0xDEBA`    | `0x00`   | `0..7` | `0xDE` | `0xBA` | Load register with constant value         |
| `LD R1, R2`        | `0x01`   | `0..7` | `0x00` | `0..7` | Load register from another register       |
| `LD R1, (0xBEAD)`  | `0x02`   | `0..7` | `0xBE` | `0xAD` | Load register direct from a memory address |
| **Store** |
| `ST  R1, (0xDEBA)` | `0x10`   | `0..7` | `0xDE` | `0xBA` | store register direct to address      |
| `STL R1, (0xDEBA)` | `0x11`   | `0..7` | `0xDE` | `0xBA` | store low byte of register direct to address     |
| `STH R1, (0xDEBA)` | `0x12`   | `0..7` | `0xDE` | `0xBA` | store high byte of register direct to address   |
| `ST  R1, (R2)`     | `0x13`   | `0..7` | `0x00` | `0..7` | store register to indirect address held in second register |
| `STL R1, (R2)`     | `0x14`   | `0..7` | `0x00` | `0..7` | store low byte of register to indirect address held in second register |
| `STH R1, (R2)`     | `0x15`   | `0..7` | `0x00` | `0..7` | store high byte of register to indirect address held in second register       |
| **Comparison** |
| `CMP R1, R2`       | `0x20`   | `0..7` | `0x00` | `0..7` | Compare R1 with R2       |
| `CMP R1, 0xDEBA`   | `0x21`   | `0..7` | `0xDE` | `0xBA` | compare register with constant value         |
| **Branching** |
| `BEQ 0xDEBA`   | `0x30`   | `0x00` | `0xDE` | `0xBA` | Branch if last comp was equal, to address (or label)   |
| `BGT .LOOP`   | `0x31`   | `0x00` | `0xDE` | `0xBA` | Branch if last comp was greater than, to address (or label)   |
| `BLT 0xDEBA`   | `0x32`   | `0x00` | `0xDE` | `0xBA` | Branch if last comp was less than, to address (or label)   |
| `BRA .HALT_APP`   | `0x33`   | `0x00` | `0xDE` | `0xBA` | Branch always to address (or label)   |
| **Arithmetic** |
| `ADD R1, 0xabcd` | `0x40` | `0..7` | `0xAB` | `0xCD` | add constant value to register|
| `SUB R1, 0xabcd` | `0x41` | `0..7` | `0xAB` | `0xCD` | subtract constant valu from register|
| `ADD R1, R2`     | `0x42` | `0..7` | `0x00` |  `0..7` | add register 2 to register 1 (result in r1) |
| `SUB R1, R2`     | `0x43` | `0..7` | `0x00` |  `0..7` | subtract register 2 from register 1 (result in r1)|
| **Miscelanious** |
| `HALT`             | `0xFE`   | `0x00` | `0x00` | `0x00` | Stops the processor from executing        |
| `NOOP`             | `0xFF`   | `0x00` | `0x00` | `0xoo` | Does nothing, with no side effects        |
| **Assember** |
| `.label`             |    |  |  |  | defines a label that holds the current address. placed on a line by itself        |




[1]: https://www.youtube.com/c/GaryExplains
[2]: https://github.com/garyexplains/examples
[3]: https://www.youtube.com/watch?v=wjHlvQfo5uI&list=PLxLxbi4e2mYGvzNw2RzIsM_rxnNC8m2Kz&index=4
[4]: https://www.youtube.com/watch?v=5ImTvOyvH2w&list=PLxLxbi4e2mYGvzNw2RzIsM_rxnNC8m2Kz&index=1

[5]: https://www.google.com
