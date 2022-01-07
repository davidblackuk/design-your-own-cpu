# Emulator

The emulator emulates our CPU's Instruction Set Architecture (ISA) in a literal manner. There are classes that represent the Registers, Flags 
and the RAM (memory). Each ISA instruction is represented by a distinct class that models the instruction's behavior and any side effects it has on memory, Registers or flags.


![program definition](Images/block-diagram.png)

When the emulator bootstraps the main program gets a CPU object from the Dependency Injection (DI) container. This creates a CPU object and wires up it's dependencies, these are a set of registers, a block of Memory and a set of flags.

All flags are initialized to false. registers R0..R7 are initialized to zero. The Program Counter (`PC`) is also initialized to zero which is the address of the first instruction to execute.

Finally we load the RAM with the application image to execute and call the run method of the CPU. The CPU will execute fetch and execute instructions until the Halt flag is set to true, at that point execution terminates and the emulator exits to the command line.

```C#
    // Get the CPU
    ICpu cpu = serviceProvider.GetService<ICpu>();

    // Load the app image at address zero
    cpu.Memory.Load(binaryToExecute);
                
    // Run the CPU
    cpu.Run();
```

The emulator's main loop in the Run() method:

+ loops until the Halt flag in the Flags register is set to true:
  + Fetches the current instruction from the address held in the `PC` (this is always 4 bytes in size in our ISA)
  + Increments the `PC` by 4 to point to the next instruction
  + Executes the current instruction

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
before executing the current instruction. If the current 
instruction modifies `PC` (for example `BRA address`) then the instruction 
can set the `PC` to the address to branch to,  subsequent execution 
takes place from there. 

For instructions that do not alter the program counter the next 
instruction in memory will be the next one to be fetched and 
executed. So in the absence of instructions that alter `PC`, instructions execute in sequential manner.

## InstructionFactory

We saw that the get the next instruction to execute from the `InstructionFactory` this is a simple factory class that creates instructions based 
on the opcode of the instruction to create.

Here is a highly elided version of it

```C#

    public class EmulatorInstructionFactory : IEmulatorInstructionFactory
    {

        public IEmulatorInstruction Create(byte opcode, byte register, byte high, byte low)
        {
            switch (opcode)
            {
                case OpCodes.Halt:
                    return new HaltInstruction(register, high, low);
                case OpCodes.Nop:
                    return new NopInstruction(register, high, low);
                // many, many lines elided
                default:
                    return new UnknownInstruction();
            }
        }
    }

```

It functions by switching on the opcode and newing up an instruction class based on that opcode. For unknown 
opcodes, it returns an `UnknownInstruction` which will cause the emulator to quit after reporting the error.

There are two possible sources for unknown instructions: 

+ If executtion goes past the end of executable code and hits areas used for strings or storage.
+ We add an instruction to the assembler, but do not update the emulator

The latter shouldn't happen, but it has in the past!


## Instructions

In the preceding source code fragment, the emulator gets the next instruction to execute from the instruction factory, passing the 4 bytes that describe the instruction. 

The instruction is then evaluated via the `Execute()` method, `Execute()` is passed the CPU object so it can effect the state of it.

All instructions in the emulator derive from the `EmulatorInstruction` class, which in turn derives from the `Instruction` class. The `Instruction` class is a shared class used in both the assembler and the emulator.

![IClass diagram for a no-op instruction](Images/nop.png)

An instruction in this inheritance hierarchy has access to the 4 bytes of data that define it (`OpCode`, `ByteHigh`, `ByteLow` and 
`Register`, defined in the `Instruction` base class), instructions also have access to the 16 bit 
value associated with it from the `Value` property from the `EmulatorInstruction` parent class.

### No argument operations (`NOP`,`HALT`)

The NoOp instruction in the emulator is defined as an `EmulatorInstruction` that has an `Execute()` method that does nothing (especially it has no side effects on the CPU state)

```C#
    public class NopInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Nop;

        public NopInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            // Do nothing, effortlessly
        }
    }
```

As an example of an instruction that has side effects, here is the halt instruction, which stops the processor executing any more instructions and quits the 
emulator. Halt works by setting the `Halted` flag in the CPU's `Flags` register to true. 

```C#
    public class HaltInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Halt;

        public HaltInstruction(byte register, byte high, byte low) : base(Opcode, register, high, low)
        {
        }

        public void Execute(ICpu cpu)
        {
            cpu.Flags.Halted = true;
        }
    }
```

There is no point detailing the implementation of every instruction in the ISA, there are significant  similarities between associate instructions. For example, 
if you have seen the implementation of the add register to register instruction; the Subtract, Divide and Multiply varieties will not surprise you.

### Mathematical operations (`ADD`,`SUB`,`MUL`,`DIV`)

This group of operations have two variants: one operates on two registers, the other applies the operation to a register and a 
constant value. For the `ADD` operator we have the classes: `AddRegisterToRegisterInstruction` and `AddConstantToRegisterInstruction`, these 
two operators differ only in where the instruction gets the value to apply to the source register.

I'm going to start to drop the constructors and OPCode declaration in the source from now on, they only add noise, just close your eyes and imagine 
them if you feel the need.

```C#
    public class AddConstantToRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] += Value;
        }
    }

    public class AddRegisterToRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            cpu.Registers[Register] += cpu.Registers[ByteLow];
        }
    }
```

These instructions take a 16 bit value and store them in the target register. In the case of the constant operation, the value comes from the 
opcode data (via the Value property of the `EmulatorInstruction` parent class), for the add register to register variety the register number 
comes from the ByteLow property of the `Instruction` base class, the value comes from the Register at that index. These implementations mirror the definition of the [Instruction Set Architecture][1].

### Compare operations (`CMP`)

Comparisons, like mathematical operations come in two variants, one comparing a register to another register: `CompareWithRegisterInstruction` 
and the other comparing a register to a constant value: `CompareWithConstantInstruction`. Comparions are slightly different to other instructions 
as we interpose a new base class `CompareInstruction` between the implementation class and the `EmulatorInstruction`. 

![Compare](Images/compare.png)

We introduced `CompareInstruction` because the the code to set the Flags is independant of the source of the value (register or constant).

```C#
    public class CompareInstruction : EmulatorInstruction
    {
        public void Compare(ushort left, ushort right, IFlags flags)
        {
            flags.Equal = left == right;
            flags.GreaterThan = left > right;
            flags.LessThan = left < right;
        }
    }
```
The compare function sets the actual Flags. All flags are set in one action. THe ISA has no notion of branch if greater than or equal,
in a RISC instruction set like the original Gary Explains ISA, you would compare and then `BGE Address`, `BEQ Address`. 

The implementations of the two compare instructions as previously mentioned, differ only by the source of the second value to compare with

```C#
    public class CompareWithRegisterInstruction : CompareInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            var left = cpu.Registers[Register];
            var right = cpu.Registers[ByteLow];
            Compare(left, right, cpu.Flags);
        }
    }

    public class CompareWithConstantInstruction : CompareInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            var left = cpu.Registers[Register];
            var right = Value;
            Compare(left, right, cpu.Flags);
        }
    }
```

### Branch operations (`BRA`, `BEQ`, `BGT`, `BLT`)

Branch operations work by (optionally) setting the `PC` to the address from which execution should continue on the 
next CPU cycle. the `BRA` instruction does this regardless of any conditions, the address is sored in the `ByteHigh` 
and `ByteLow` of the instructions data and acessed via the `Value` property:

```C#
    public class BranchAlwaysInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            cpu.Registers.ProgramCounter = Value;
        }
    }
```

the `BEQ`, `BGT` and `BLT` instructions only differ by the flag they test to determine if the branch should be taken. Here 
is the implementation of `BEQ`. 

```C#
    public class BranchEqualInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            if (cpu.Flags.Equal) { 
                cpu.Registers.ProgramCounter = Value;
            }
        }
    }
```

### Stack operations (`PUSH`, `POP`, `CALL`, `RET`)

Once again for stack opperations we have a shared base class `StackInstruction` that implements the Push and Pop operations as these 
are used by the `CALL`, `RET`, `PUSH` and `POP` Instructions.

![Stack](Images/stack.png)

```C#
    public class StackInstruction : EmulatorInstruction
    {
       
        public void Push(ushort value, ICpu cpu)
        {
            cpu.Registers.StackPointer -= 4;
            cpu.Memory.SetWord(cpu.Registers.StackPointer, value);
        }

        public ushort Pop(ICpu cpu)
        {
            var res = cpu.Memory.GetWord(cpu.Registers.StackPointer);
            cpu.Registers.StackPointer += 4;
            return res;
        }
    }
```

`Push()`, decrements the stack pointer (remember the stack grows down from the top of memory), and stores the value being 
pushed at the address now contained in that stack pointer.

`Pop()`, gets the word stored at the address in the current stack pointer and then increments the stack pointer to point to 
the preceding value on the stack.

The `PushInstruction` and `PopInstruction` both operate by delegating there functionality to the methods in the base class. Here 
is the `PushInstruction` as an example:

```C#
    public class PushInstruction : StackInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu) => Push(cpu.Registers[Register], cpu);
    }
```

this pushes the specified register's value onto the stack.

The call instruction pushes the current Program Counter value onto the stack and then jumps to the sub-routine
at the address specified in the `Value` property from the instruction bytes.


```C#
    public class CallInstruction : StackInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            Push(cpu.Registers.ProgramCounter, cpu);
            cpu.Registers.ProgramCounter = Value;
        }
    }
```

To return from a sub-routine to the address after the call instruction, we Pop the address off the stack and then
set the Program Counter to that address. 

```C#
    public class ReturnInstruction : StackInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu) => cpu.Registers.ProgramCounter = Pop(cpu);
    }
```

### Load and store operations (`LD`, `ST`, `STH`, `STL`)

The load and store instructions are not orthoganal, this is hold over from the original GE version of the ISA, for example there is a 
Store Register High instruction but no matching Load Register High. More imporantly you can store a registers value indirectly (`ST R0 , (R1)`), but 
not load it in the same way (`LD R0, (R1)`). This is a gap that should be fixed in a future version.

The load instruction has three variants: 

+ `LD R1, (0xBEAD)`: load a R1 directly from an address specified in the instruction (`LoadRegisterDirectInstruction`)
+ `LD R7, 0x1234`:  load R7 with a constant value specified in the instruction  (`LoadRegisterWithConstantInstruction`)
+ `LD R4, R6`: load R4 with the value of R6 (`LoadRegisterFromRegisterInstruction`) 

The implementation of these instructions are: 

```C#
    public class LoadRegisterDirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            // Value is the address in memory
            cpu.Registers[Register] = cpu.Memory.GetWord(Value);
        }
    }

    public class LoadRegisterFromRegisterInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            // ByteLow contains the register to load from
            cpu.Registers[Register] = cpu.Registers[ByteLow];
        }
    }

    public class LoadRegisterWithConstantInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            // value contains the constant ro load (16 buts stored in ByteHigh and ByteLow)
            cpu.Registers[Register] = Value;
        }
    }
```

The the are two store instructions that complement the above three load ones.

+ `ST R1, (0xBEAD)`: store the value in R1 to a direct address im memory specified as a constant (`StoreRegisterDirectInstruction`)
+ `ST R7, (R0)`:  store R7 indirectly via the address contained in R0 (`StoreRegisterDirectInstruction`)


```C#
    public class StoreRegisterDirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            // store two bytes to the const address in VAlue
            cpu.Memory.SetWord(Value, cpu.Registers[Register]);
        }
    }

    public class StoreRegisterIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            // get the destination address from the specified register
            var indirectAddress = cpu.Registers[ByteLow];

            // store two bytes
            cpu.Memory.SetWord(indirectAddress, cpu.Registers[Register]);
        }
    }

```

If you are wondering why 3 load instructions and two store?: There is no _store register to register_ instruction because 
there is already a load register from register, which does the same thing.

There are two store instructions that store just the high or low byte of the source register, as usual there are two 
variations for these instructions, for the direct and indirect addressing modes.

+ `STH R1, 0x4356`: store the high byte of R1 to the direct address specified as a constant (`StoreRegisterHighDirectInstruction`). 
+ `STH R1, (R3)`:store the high byte of R1 to the indirect address held in register R3 (`StoreRegisterHighIndirectInstruction`).
+ `STL R1, 0x4356`: store the low byte of R1 to the direct address specified as a constant (`StoreRegisterLowDirectInstruction`). 
+ `STL R1, (R3)`:store the low byte of R1 to the indirect address held in register R3 (`StoreRegisterLowIndirectInstruction`).

```C#
      public class StoreRegisterHighDirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            cpu.Memory[Value] = (byte)((cpu.Registers[Register] >> 8) & 0xFF);
        }
    }

    public class StoreRegisterHighIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            var indirectAddress = cpu.Registers[ByteLow];
            var value = (byte)((cpu.Registers[Register] >> 8) & 0xFF);
            cpu.Memory[indirectAddress] = value;
        }
    }

    public class StoreRegisterLowDirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            cpu.Memory[Value] = (byte)(cpu.Registers[Register] & 0xFF);
        }
    }

    public class StoreRegisterLowIndirectInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public void Execute(ICpu cpu)
        {
            var indirectAddress = cpu.Registers[ByteLow];
            var value = (byte)(cpu.Registers[Register] & 0xFF);
            cpu.Memory[indirectAddress] = value;
        }
    }
```
The only note worth thing here for beginning developers is that we have a 16 bit value in the source register.  To get the registers low data byte we 'AND' the value with 0xFF (`cpu.Registers[Register] & 0xFF`). OxFF is binary `0000000011111111`, 
the effect of this is to set the upper 8 bits to zero and retain the lowest 8 bits. This would turn `10110110 10100011` (0xB6A3)  into '00000000 10100011' (0x00A3) , which we then cast to a byte and store the 8 bit value.

To get the upper 8 bits as a byte we use a similar trick but first we shift the 16 bit value right 8 binary places, then mask it
`((cpu.Registers[Register] >> 8) & 0xFF)`. This would turn `10110110 10100011`  (0xB6A3) into '00000000  10110110'  (0x00B6), which we then cast to a byte and store it as an 8 bit value in memory.

### Software Interups (`SWI`)

The software interrupt instruction takes as an argument the interrupt number and then executes the instruction associated with it. There are
software interrupts to read and write integer values and to write a string to the console. The `EmulatorInstructionFactory` 
returns a `SoftWareInterruptInstruction`, this uses a second factory (`InterruptFactory`) to create an instruction that implements the  functionality 
specified in the value of the `SWI` call.

```C#
    public class SoftwareInterruptInstruction : EmulatorInstruction, IEmulatorInstruction
    {
        public const byte Opcode = OpCodes.Swi;
        private readonly IInterruptFactory interruptFactory;

        public SoftwareInterruptInstruction(IInterruptFactory interruptFactory, byte register, byte high, byte low) :
            base(Opcode, register, high, low)
        {
            this.interruptFactory = interruptFactory;
        }

        public void Execute(ICpu cpu)
        {
            // Create the interupt from the vector number in the Value operand.
            var interrupt = interruptFactory.Create(Value);
            interrupt.Execute(cpu);
        }
    }  
```

The interrupt factory uses the instructions `Value` to determine the instruction to return 

```C#
    public class InterruptFactory : IInterruptFactory
    {
        public IInterrupt Create(ushort vector)
        {
            switch (vector)
            {
                // Other interrupts elided
                case InternalSymbols.WriteWordInterrupt:
                    return new WriteWordInterrupt();
                default:
                    throw new EmulatorException($"Unknown interrupt vector {vector}");
            }
        }
    }
```

Finally the returned instruction will be executed. Software Interrupts are faked calls to a ROM kernel, since we are emulating the ISA and 
not a hardware platform, there is no device to send data to, so in the example of the write word interrupt, we simply echo the value to 
the console from C#


```C#
    public class WriteWordInterrupt : IInterrupt
    {
        // outputs the value in register R0 to the console
        public void Execute(ICpu cpu)
        {
            Console.Write($"0x{cpu.Registers[0]:X4}");
        }
    }
```

## Wrap up

This concludes the description of the emulator, we've covered all of the key classes that makes the emulator tick. Some of the details of
the wire-up of the executable from a dependency injection point of view were skipped, but those can be covered in a separate document, 
these are cross cutting concerns and need to be detailed in one place and one place only.


In my opinion the emulator is far simpler than the assembler. The assembler has to deal with uncertain human written input, 
the emulator can slavishly follow the well formed input: a byte, is a byte, is a byte. 

Grab the source, build it and have a play.


[1]: ../../Readme.md
