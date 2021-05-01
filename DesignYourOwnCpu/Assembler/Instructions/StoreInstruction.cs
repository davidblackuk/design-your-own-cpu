using System.Diagnostics.CodeAnalysis;
using Shared;

namespace Assembler.Instructions
{
    public class StoreInstructionBase : Instruction, IInstruction
    {
        private readonly string instructionName;
        private readonly byte indirectOpCode;
        private readonly byte directOpCode;

        public StoreInstructionBase(string instructionName, byte indirectOpCode, byte directOpCode)
        {
            this.instructionName = instructionName;
            this.indirectOpCode = indirectOpCode;
            this.directOpCode = directOpCode;
        }
        public void Parse(string source)
        {
            var operands = GetOperands(instructionName, source);
            Register = ParseRegister(operands.left);
            if (IsIndirectAddress(operands.right))
            {
                ByteLow = ParseIndirectAddress(operands.right);

                OpCode = indirectOpCode;
            }
            else // its load a constant
            {
                ParseAddress(operands.right);
                OpCode = directOpCode;
            }
        }
        
        
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            string bytes = base.ToString();
            if (OpCode == directOpCode)
            {
                return $"{bytes}    # {instructionName} r{Register}, (0x{ByteHigh:X2}{ByteLow:X2})";
            }
            else if (OpCode == indirectOpCode)
            {
                return $"{bytes}    # {instructionName}  r{Register}, (r{ByteLow})";
            }
            else
            {
                return "!!ERROR!!";
            }
        }
    }

    public class StoreInstruction : StoreInstructionBase
    {
        public const string InstructionName = "st";

        public StoreInstruction() : base(InstructionName, OpCodes.StoreRegisterIndirect, OpCodes.StoreRegisterDirect)
        {
            
        }
    }
    
    public class StoreHiInstruction : StoreInstructionBase
    {
        public const string InstructionName = "sth";

        public StoreHiInstruction(): base(InstructionName, OpCodes.StoreRegisterHiIndirect, OpCodes.StoreRegisterHiDirect)
        {
        }
    }

    public class StoreLowInstruction : StoreInstructionBase
    {
        public const string InstructionName = "stl";

        public StoreLowInstruction(): base(InstructionName, OpCodes.StoreRegisterLowIndirect, OpCodes.StoreRegisterLowDirect)
        {
        }
    }
}