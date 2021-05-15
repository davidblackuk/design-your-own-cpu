﻿#
# Simple assembly test app for the assembler.
#

    LD R1, 0x30
.loop
    STL R1, (0xFBFF)
    CMP R1, 0x39
    ADD R1, 1
    BLT loop
    LD R1, 0x39
    LD R2, 3
.loop2
    STL R1, (0xFC00)
    CMP R1, 0x30
    SUB R1, R2
BGT loop2
    HALT                # terminate