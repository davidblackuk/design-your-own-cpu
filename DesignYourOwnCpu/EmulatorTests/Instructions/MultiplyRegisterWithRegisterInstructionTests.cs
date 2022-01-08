using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions;

[ExcludeFromCodeCoverage]
public class MultiplyRegisterWithRegisterInstructionTests : EmulatorUnitTest
{
    [Test]
    public void Execute_WhenInvoked_ShouldAddTheConstantToTheRigister()
    {
        // SUB R7, R4
        byte firstRegister = 7;
        byte secondRegister = 4;

        ushort firstRegisterValue = 0x3456;
        ushort secondRegisterValue = 0x1234;

        RegistersMock.SetupGet(r => r[firstRegister]).Returns(firstRegisterValue);
        RegistersMock.SetupGet(r => r[secondRegister]).Returns(secondRegisterValue);

        var sut = CreateSut(firstRegister, secondRegister);

        sut.Execute(CpuMock.Object);

        RegistersMock.VerifySet(reg => reg[firstRegister] = (ushort)(firstRegisterValue * secondRegisterValue));
    }

    private MultiplyRegisterWithRegisterInstruction CreateSut(byte register, byte otherRegister)
    {
        return new MultiplyRegisterWithRegisterInstruction(register, 0, otherRegister);
    }
}