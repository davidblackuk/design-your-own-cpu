using System;
using Emulator;
using Emulator.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace EmulatorTests.Instructions
{
    public class EmulatorInstructionFactoryTests
    {

        [Test]
        [TestCase(OpCodes.Nop, typeof(NopInstruction))]
        [TestCase(OpCodes.Halt, typeof(HaltInstruction))]
        [TestCase(OpCodes.LoadRegisterWithConstant, typeof(LoadRegisterWithConstantInstruction))]
        [TestCase(OpCodes.LoadRegisterFromMemory, typeof(LoadRegisterFromMemoryInstruction))]
        [TestCase(OpCodes.LoadRegisterFromRegister, typeof(LoadRegisterFromRegisterInstruction))]
        [TestCase(OpCodes.StoreRegisterDirect, typeof(StoreRegisterDirectInstruction))]
        [TestCase(OpCodes.StoreRegisterHiDirect, typeof(StoreRegisterHighDirectInstruction))]
        [TestCase(OpCodes.StoreRegisterLowDirect, typeof(StoreRegisterLowDirectInstruction))]
        public void Create_WhenCalledWithAKnownOpcode_ShouldReturnCorrectType(
            byte opcode, Type expectedType)
        {
            byte expectedRegister = 0xDE;
            byte expectedHigh = 0x12;
            byte expectedLow = 0x09;
            var sut = CreateSut();
            var instruction = sut.Create(opcode, expectedRegister, expectedHigh, expectedLow);
            instruction.GetType().Name.Should().Be(expectedType.Name);
            instruction.OpCode.Should().Be(opcode);
            instruction.Register.Should().Be(expectedRegister);
            instruction.ByteHigh.Should().Be(expectedHigh);
            instruction.ByteLow.Should().Be(expectedLow);
        }

        [Test]
        public void Create_WhenUnimplementedInstructionPassed_ShouldThrowEmulationException()
        {
            var sut = CreateSut();
            Assert.Throws<EmulatorException>(() => sut.Create(OpCodes.Unused, 0, 0, 0));
        } 
        
        
        private EmulatorInstructionFactory CreateSut()
        {
            return new EmulatorInstructionFactory();
        }
    }
}