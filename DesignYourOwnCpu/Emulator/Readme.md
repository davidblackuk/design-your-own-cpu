# Emulator

The emulator is the least complex component in the system. In essence it:

+ Loads the applications binary image into RAM from address zero
+ Initializes the Program Counter (`PC`) to address zero
+ loops until the current instruction is halt:
  + Fetches the current instruction from the address held in the `PC`
  + Increments the `PC` by 4 bytes
  + Executes the current instruction

```C#

    // Get the CPU
    ICpu cpu = serviceProvider.GetService<ICpu>();

    // Load the app image at address zero
    cpu.Memory.Load(binaryToExecute);
                
    // Run the CPU
    cpu.Run();

```

The run method from the CPU class minus error checking is:

```C#

    do
    {
        // get the 4 bytes from the next instruction
        var bytes = Memory.Instruction(Registers.ProgramCounter);

        // get an object that emulates the effects of the instruction
        var instruction =
            instructionFactory.Create(bytes.opcode, bytes.register, bytes.byteHigh, bytes.byteLow);
                
        // increment the program counter
        Registers.ProgramCounter = (ushort)(Registers.ProgramCounter + instruction.Size);

        instruction.Execute(this);
    } while (!Flags.Halted);

```



The most important thing to note is we pre-increment the `PC` 
before executing the fetched, current instruction. If the current 
instruction modifies `PC` (for example `BRA address`) then the instruction 
can set the `PC` to the address to branch to, and subsequent execution 
takes place from that address. 

For instructions that do not alter the program counter the next 
instruction in memory will be the next one to be fetched and 
exectued.


