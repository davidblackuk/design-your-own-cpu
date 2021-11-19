#
# Add two numbers and print the result to the console, using
# registers for temp storage
#

    ld r0, hello-message            # lets add two numbers message
    swi sys-write-string
    
    ld r0, enter-number-message     # Enter a number prompt
    swi sys-write-string

    swi sys-read-word               # read a number from the console
    ld r6, r0                       # store read number in r6
        
    ld r0, enter-number-message     # Enter a number prompt
    swi sys-write-string

    swi sys-read-word               # read a number from the console
    ld r7, r0                       # store read number in r7

    ld r0, results-message          # write out the results message
    swi sys-write-string
   
    add r6, r7                      # add r6 and r7, result in r6
    ld r0, r6                       # place in r0 to call the print software interupt
    swi sys-write-word              # write the value of r0
   
    ld r0, eol                      # add a pair of new lines to tidy up
    swi sys-write-string
    swi sys-write-string

    halt                            # halt the processor we are done
 
#
# Messages
#

.hello-message    
    defm "\nLets add two numbers\n\0"
.enter-number-message    
    defm "\nEnter a number please:\0"
.results-message    
    defm "\nThe sum of the two values is:\0"
.eol
    defm "\n\0"
