using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using Moq;
using NUnit.Framework;

namespace EmulatorTests.Instructions;

[ExcludeFromCodeCoverage]
public class CallInstructionTests : EmulatorUnitTest
{
    [Test]
    public void Execute_WhenInvoked_ShouldSetProgramCounterToValueAndPushReturnAddress()
    {
        ushort stackPointer = 0xFFFC;
        ushort callAddress = 0x3141;
        ushort currentProgramCounter = 0x00F0;
        var decrementedStackPointer = (ushort)(stackPointer - 4);

        RegistersMock.SetupGet(r => r.StackPointer).Returns(stackPointer);
        RegistersMock.SetupGet(r => r.ProgramCounter).Returns(currentProgramCounter);
        var sut = CreateSut(callAddress);
        sut.Execute(CpuMock.Object);

        RegistersMock.VerifySet(r => r.StackPointer = decrementedStackPointer);

        // since its a mock the pre-decrement isn't real, so ignore the address
        MemoryMock.Verify(m => m.SetWord(It.IsAny<ushort>(), currentProgramCounter));
        RegistersMock.VerifySet(r => r.ProgramCounter = callAddress);
    }

    private CallInstruction CreateSut(ushort expectedProgramCounter)
    {
        return new CallInstruction(0, HighByte(expectedProgramCounter), LowByte(expectedProgramCounter));
    }
}