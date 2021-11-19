#
# Say hello world application.
#

    ld r0, hello-world      # load r0 with the address of the string
    swi sys-write-string    # invoke the software interupt
    halt                    # hal the processor we are done

.hello-world    
    defm "\nHello world!\n\n\0"

