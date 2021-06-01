#
# Simple assembly test app for the assembler.
#

    LD R1, 0x30
.loop
    STL R1, (0x4000)
    CMP R1, 0x39
    CALL increment_r1
    BLT loop
    LD R1, 0x39
    LD R2, 3
.loop2
    STL R1, (0x4100)
    CMP R1, 0x30
    SUB R1, R2
BGT loop2
    HALT                # terminate
    
#
# completely unnecessary sub routine to increment R1 by 1
#
.increment_r1
    ADD R1, 1
    CALL other_routine
    RET

#
# storage for our app
#

.buffer
defs 32

.other_routine
    LD R7, 0x65F1
    RET

.upcounter
# defw 0         # upward counter in part 1

.downcounter
# defw 0         # downward counter in part 2
 
.byte_primes 
#defb 2,	3,	5,	7,	11,	13,	17,	19,	23
 
.message1    
defm "messages add zero terminator"

.primes
# defw 2,	3,	5,	7,	11,	13,	17,	19,	23,	29,	31,	37,	41,	43,	47,	53,	59,	61,	67,	71

 