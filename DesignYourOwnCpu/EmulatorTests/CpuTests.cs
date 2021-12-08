using System.Diagnostics.CodeAnalysis;
using Emulator;
using Emulator.Instructions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shared;

namespace EmulatorTests
{
    [ExcludeFromCodeCoverage]
    public class CpuTests
    {
        private readonly ushort expectedAddress = 0x1234;
        private readonly (byte opcode, byte register, byte byteHigh, byte byteLow) expectedInstruction = (0, 1, 2, 3);
        private readonly ushort expectedSize = 2;
        private Mock<IFlags> flagsMock;
        private Mock<IEmulatorInstructionFactory> instructionFactoryMock;
        private Mock<IRandomAccessMemory> memoryMock;
        private Mock<IRegisters> registersMock;

        [SetUp]
        public void SetUp()

        {
            memoryMock = new Mock<IRandomAccessMemory>();
            registersMock = new Mock<IRegisters>();
            flagsMock = new Mock<IFlags>();
            instructionFactoryMock = new Mock<IEmulatorInstructionFactory>();
        }

        [Test]
        public void Ctor_whenInvoked_ShouldWireUpProperties()
        {
            var sut = CreateSut();
            sut.Flags.Should().BeSameAs(flagsMock.Object);
            sut.Registers.Should().BeSameAs(registersMock.Object);
            sut.Memory.Should().BeSameAs(memoryMock.Object);
        }

        [Test]
        public void Run_whenHaltedSet_ShouldExitTheRunLoopAndStop()
        {
            SetUpMockCpu();
            var sut = CreateSut();
            sut.Run();
        }

        [Test]
        public void Run_whenInstructionExecuted_ShouldIncrementTheProgramCounter()
        {
            SetUpMockCpu();
            var sut = CreateSut();
            sut.Run();
            registersMock.VerifySet(r => r.ProgramCounter = (ushort)(expectedAddress + expectedSize));
        }

        [Test]
        public void Run_whenInstructionExecuted_ShouldExecuteTHeCurrentInstruction()
        {
            var instructionMock = SetUpMockCpu();
            var sut = CreateSut();
            sut.Run();
            instructionMock.Verify(i => i.Execute(sut));
        }

        private Mock<IEmulatorInstruction> SetUpMockCpu()
        {
            var expectedInstructionMock = new Mock<IEmulatorInstruction>();
            expectedInstructionMock.SetupGet(i => i.Size).Returns(expectedSize);

            registersMock.SetupGet(r => r.ProgramCounter).Returns(expectedAddress);

            memoryMock.Setup(m => m.Instruction(expectedAddress)).Returns(expectedInstruction);
            instructionFactoryMock.Setup(m => m.Create(expectedInstruction.opcode, expectedInstruction.register,
                    expectedInstruction.byteHigh, expectedInstruction.byteLow))
                .Returns(expectedInstructionMock.Object);
            flagsMock.SetupGet(flags => flags.Halted).Returns(true);
            return expectedInstructionMock;
        }

        private Cpu CreateSut()
        {
            return new Cpu(memoryMock?.Object, registersMock?.Object, flagsMock?.Object,
                instructionFactoryMock?.Object);
        }
    }
}