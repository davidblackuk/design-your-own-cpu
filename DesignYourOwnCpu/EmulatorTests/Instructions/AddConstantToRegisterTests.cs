using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions;

[ExcludeFromCodeCoverage]
public class AddConstantToRegisterTests : EmulatorUnitTest
{
    [Test]
    public void Execute_WhenInvoked_ShouldAddTheConstantToTheRigister()
    {
        byte expectedRegister = 7;
        ushort constValue = 0x3456;
        ushort initialRegisterValue = 0x1234;

        var sut = CreateSut(expectedRegister, constValue);
        RegistersMock.SetupGet(r => r[expectedRegister]).Returns(initialRegisterValue);
        sut.Execute(CpuMock.Object);

        RegistersMock.VerifySet(reg => reg[expectedRegister] = (ushort)(constValue + initialRegisterValue));
    }

    private AddConstantToRegisterInstruction CreateSut(byte register, ushort value)
    {
        return new AddConstantToRegisterInstruction(register, HighByte(value), LowByte(value));
    }
}