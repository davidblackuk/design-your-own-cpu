# Simple assembler and emulator

A implementation of the assembler and emulator for the simple processor design presented in the
[Gary explains videos][1] on youtube.

Specific videos:
 - [Design your own instruction format][3]
 - [Write Your Own Assembler for Your Own CPU][4]
 - [As yet un released video][5]

Gary's [orininal source][2] is availble on Github


# My CPU definition

Instruction definitions. All instructions are 32 bits long, unsed bytes set to 0. Registers are 16 bit.

|   Instruction   | Opcode | Register | Data H | Data L | Description |
|-----------------|--------|----------|--------|--------|-------------|
| **Load** |
| `LD R1, 0xDEBA`    | `0x00`   | `0..7` | `0xDE` | `0xBA` | Load register with constant value         |
| `LD R1, R2`        | `0x01`   | `0..7` | `0x00` | `0..7` | Load register from another register       |
| `LD R1, (0xBEAD)`  | `0x02`   | `0..7` | `0xBE` | `0xAD` | Load register direct from a memory address       |
| **Store** |
| `ST  R1, (0xDEBA)` | `0x10`   | `0..7` | `0xDE` | `0xBA` | store register direct to address      |
| `STL R1, (0xDEBA)` | `0x20`   | `0..7` | `0xDE` | `0xBA` | store low byte of register direct to address     |
| `STH R1, (0xDEBA)` | `0x30`   | `0..7` | `0xDE` | `0xBA` | store high byte of register direct to address   |
| `ST  R1, (R2)`     | `0x40`   | `0..7` | `0x00` | `0..7` | store register to indirect address held in second register |
| `STL R1, (R2)`     | `0x50`   | `0..7` | `0x00` | `0..7` | store low byte of register to indirect address held in second register |
| `STH R1, (R2)`     | `0x60`   | `0..7` | `0x00` | `0..7` | store high byte of register to indirect address held in second register       |
| **Comparison** |
| `CMP R1, R2`       | `0x30`   | `0..7` | `0x00` | `0..7` | Compare R1 with R2       |
| `CMP R1, 0xDEBA`   | `0x31`   | `0..7` | `0xDE` | `0xBA` | compare register with constant value         |
| **Branching ** |
| **Arithmetic** |
| **Miscelanious** |
| `HALT`             | `0xFE`   | `0x00` | `0x00` | `0x00` | Stops the processor from executing        |
| `NOOP`             | `0xFF`   | `0x00` | `0x00` | `0xoo` | Does nothing, with no side effects        |




[1]: https://www.youtube.com/c/GaryExplains
[2]: https://github.com/garyexplains/examples
[3]: https://www.youtube.com/watch?v=wjHlvQfo5uI&list=PLxLxbi4e2mYGvzNw2RzIsM_rxnNC8m2Kz&index=4
[4]: https://www.youtube.com/watch?v=5ImTvOyvH2w&list=PLxLxbi4e2mYGvzNw2RzIsM_rxnNC8m2Kz&index=1

[5]: https://www.google.com
