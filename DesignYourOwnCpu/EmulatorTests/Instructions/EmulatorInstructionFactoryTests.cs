using System;
using System.Diagnostics.CodeAnalysis;
using Emulator;
using Emulator.Instructions;
using Emulator.Instructions.Interrupts;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shared;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class EmulatorInstructionFactoryTests
    {
        private Mock<IInterruptFactory> interruptFactoryMock;

        [SetUp]
        public void SetUp()
        {
            interruptFactoryMock = new Mock<IInterruptFactory>();
        }

        [Test]
        [TestCase(OpCodes.Nop, typeof(NopInstruction))]
        [TestCase(OpCodes.Halt, typeof(HaltInstruction))]
        [TestCase(OpCodes.LoadRegisterWithConstant, typeof(LoadRegisterWithConstantInstruction))]
        [TestCase(OpCodes.LoadRegisterFromMemory, typeof(LoadRegisterFromMemoryInstruction))]
        [TestCase(OpCodes.LoadRegisterFromRegister, typeof(LoadRegisterFromRegisterInstruction))]
        [TestCase(OpCodes.StoreRegisterDirect, typeof(StoreRegisterDirectInstruction))]
        [TestCase(OpCodes.StoreRegisterHiDirect, typeof(StoreRegisterHighDirectInstruction))]
        [TestCase(OpCodes.StoreRegisterLowDirect, typeof(StoreRegisterLowDirectInstruction))]
        [TestCase(OpCodes.StoreRegisterIndirect, typeof(StoreRegisterIndirectInstruction))]
        [TestCase(OpCodes.StoreRegisterLowIndirect, typeof(StoreRegisterLowIndirectInstruction))]
        [TestCase(OpCodes.StoreRegisterHiIndirect, typeof(StoreRegisterHighIndirectInstruction))]
        [TestCase(OpCodes.CompareWithConstant, typeof(CompareWithConstantInstruction))]
        [TestCase(OpCodes.CompareWithRegister, typeof(CompareWithRegisterInstruction))]
        [TestCase(OpCodes.Branch, typeof(BranchAlwaysInstruction))]
        [TestCase(OpCodes.BranchEqual, typeof(BranchEqualInstruction))]
        [TestCase(OpCodes.BranchGreaterThan, typeof(BranchGreaterThanInstruction))]
        [TestCase(OpCodes.BranchLessThan, typeof(BranchLessThanInstruction))]
        [TestCase(OpCodes.AddConstantToRegister, typeof(AddConstantToRegisterInstruction))]
        [TestCase(OpCodes.AddRegisterToRegister, typeof(AddRegisterToRegisterInstruction))]
        [TestCase(OpCodes.SubtractConstantFromRegister, typeof(SubtractConstantFromRegisterInstruction))]
        [TestCase(OpCodes.SubtractRegisterFromRegister, typeof(SubtractRegisterFromRegisterInstruction))]
        [TestCase(OpCodes.Push, typeof(PushInstruction))]
        [TestCase(OpCodes.Pop, typeof(PopInstruction))]
        [TestCase(OpCodes.Call, typeof(CallInstruction))]
        [TestCase(OpCodes.Ret, typeof(ReturnInstruction))]
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
            return new EmulatorInstructionFactory(interruptFactoryMock?.Object);
        }
    }
}