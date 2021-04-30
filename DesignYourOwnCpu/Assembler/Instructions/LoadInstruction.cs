using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Shared;

namespace Assembler.Instructions
{
    public class Instruction
    {
        public byte OpCode { get; set; }
        public byte Register { get; set; }
        public byte ByteHigh { get; set; }
        public byte ByteLow { get; set; }

        /// <summary>
        /// Splits the remaining line into two parts on a comma, returns the left operand and right
        /// operand as trimmed strings
        /// </summary>
        /// <param name="???"></param>
        /// <returns></returns>
        protected (string left, string right) GetOperands(string instructionName, string line)
        {
            var parts = line.Split(",".ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                throw new AssemblerException(
                    $"Expected two operands for instruction: {instructionName} , but found {parts.Length}");
            }

            return (parts[0], parts[1]);
        }

        protected bool IsRegister(string source)
        {
            return source.ToLowerInvariant().StartsWith("r");
        }

        protected bool IsAddress(string source)
        {
            return source.StartsWith("(") && source.EndsWith(")");
        }

        protected byte ParseRegister(string source)
        {
            if (IsRegister(source))
            {
                var registerNumber = source.Substring(1);
                if (byte.TryParse(registerNumber, out var register))
                {
                    if (register > Constants.MaxRegisterNumber)
                    {
                        throw new AssemblerException($"{source} is an illegal register number, values registers are r0..r7");
                    }
                    return register;
                }
            }

            throw new AssemblerException("Expected a register but got: " + source);
        }

        protected void ParseAddress(string text)
        {
            ParseValue(text.Substring(1).Substring(0, text.Length - 2));
        }

        protected void ParseValue(string text)
        {
            text = text.Trim();
            int value = 0;
            if (text.ToLowerInvariant().StartsWith("0x"))
            {
                value = Convert.ToUInt16(text.Substring(2), 16);
            }
            else if (text.ToLowerInvariant().StartsWith("0"))
            {
                value = Convert.ToUInt16(text.Substring(1), 8);
            }
            else
            {
                value = Convert.ToUInt16(text, 10);
            }

            ByteLow = (byte) (value & 0xff);
            ByteHigh = (byte) (value >> 8 & 0xff);
        }
    }


    public class LoadInstruction : Instruction, IInstruction
    {
        public const string InstructionName = "ld";


        public void Parse(string source)
        {
            var operands = GetOperands(InstructionName, source);
            Register = ParseRegister(operands.left);
            if (IsRegister(operands.right))
            {
                ByteLow = ParseRegister(operands.right);
                OpCode = OpCodes.LoadRegisterFromRegister;
            }
            else if (IsAddress(operands.right))
            {
                ParseAddress(operands.right);
                OpCode = OpCodes.LoadRegisterFromMemory;
            }
            else // its load a constant
            {
                ParseValue(operands.right);
                OpCode = OpCodes.LoadRegisterWithConstant;
            }
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            switch (OpCode)
            {
                case OpCodes.LoadRegisterWithConstant:
                    return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}    # {InstructionName} r{Register}, 0x{ByteHigh:X2}{ByteLow:X2}";
                case OpCodes.LoadRegisterFromRegister:
                    return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}    # {InstructionName}  r{Register}, r{ByteLow}";
                case OpCodes.LoadRegisterFromMemory:
                    return $"{OpCode:X2} {Register:X2} {ByteHigh:X2} {ByteLow:X2}    # {InstructionName} r{Register}, (0x{ByteHigh:X2}{ByteLow:X2})";
                    
            }

            return "ERROR!!";
        }
    }
}