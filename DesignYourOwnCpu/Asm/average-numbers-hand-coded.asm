# Average out a series of numbers 
# enter one number per line and terminate with a zero 

    LD R1, 0                # sum of all non-zero values read so far
    LD R2, 0                # number of values read

    SWI SYS-READ-WORD       # current = read

.NEXT_NUMBER
    ADD R1, R0              # sum += read
    ADD R2, 1               # number += 1

    SWI SYS-READ-WORD       # current = read

    CMP  R0, 0              # loop terminated?
    BEQ LOOP_ENDS           # if so goto next part
    BRA NEXT_NUMBER         # other wise loop for the next number

.LOOP_ENDS
    LD R0, R1               # R0 = sum
    CALL OUTPUT_NUMBER      # write out the sum

    LD R0, R2               # R0 = number
    CALL OUTPUT_NUMBER      # write out the number

    CMP R2, 0               # did we not get any numbers input?
    BEQ SKIP_AVERAGE        # if so skip the average calculation to avoid a divide by zero

    LD R0, R1               # sum into R0
    DIV R0, R2              # average (R0) = sum / number 
    CALL OUTPUT_NUMBER      # write out the average

.SKIP_AVERAGE
    HALT                    # program complete

.OUTPUT_NUMBER
    SWI SYS-WRITE-WORD      # write out the number in R0
    LD R0, eol              # get the address of the newline string
    SWI SYS-WRITE-STRING    # got to the next line
    RET

.eol
    DEFM "\n"