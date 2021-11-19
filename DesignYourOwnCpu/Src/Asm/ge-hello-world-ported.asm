#
# Original Gary explains example to write the string hello into memory.
# Adjusted  to use a lower address and our syntax
#

LD R1, 0x80         # start address to store our string at

LD R2, 72           # load an 'H' character in to R2
STL R2, (R1)        # store the letter in the address held in R1 (80)

ADD R1, 1           # increment R1 to next memory location 
LD R2, 101          # load an 'e' character in to R2
STL R2, (R1)        # store the letter in the address held in R1 (81)

ADD R1, 1           # increment R1 to next memory location 
LD R2, 108          # load an 'l' character in to R2
STL R2, (R1)        # store the letter in the address held in R1 (82)

ADD R1, 1           # increment R1 to next memory location
LD R2, 108          # load an 'l' character in to R2
STL R2, (R1)        # store the letter in the address held in R1 (83)

ADD R1, 1           # increment R1 to next memory location
LD R2, 111          # load an 'o' character in to R2
STL R2, (R1)        # store the letter in the address held in R1 (84)

HALT