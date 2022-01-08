using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions;

[ExcludeFromCodeCoverage]
public class MultiplyRegisterWithConstantInstructionTests : EmulatorUnitTest
{
    [Test]
    public void Execute_WhenInvoked_ShouldSubtractTheConstantFromTheRigister()
    {
        byte expectedRegister = 7;
        ushort constValue = 0x1234;
        ushort initialRegisterValue = 0x3456;

        var sut = CreateSut(expectedRegister, constValue);
        RegistersMock.SetupGet(r => r[expectedRegister]).Returns(initialRegisterValue);
        sut.Execute(CpuMock.Object);

        RegistersMock.VerifySet(reg => reg[expectedRegister] = (ushort)(initialRegisterValue * constValue));
    }

    private MultiplyRegisterWithConstantInstruction CreateSut(byte register, ushort value)
    {
        return new MultiplyRegisterWithConstantInstruction(register, HighByte(value), LowByte(value));
    }
}