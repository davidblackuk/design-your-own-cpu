#
# Original Gary explains example to write the string hello into memory.
# This will not assemble in our assembler!
#

#start address
LD R1, 0x80
# H
LD R2, 72
STL R2, R1
# e
ADD R1, 1
LD R2, 101
STL R2, R1
# l
ADD R1, 1
LD R2, 108
STL R2, R1
# l
ADD R1, 1
LD R2, 108
STL R2, R1
# o
ADD R1, 1
LD R2, 111
STL R2, R1
