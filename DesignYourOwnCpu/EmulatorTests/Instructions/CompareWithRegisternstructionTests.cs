using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions;

[ExcludeFromCodeCoverage]
public class CompareWithRegisternstructionTests : EmulatorUnitTest
{
    [Test]
    [TestCase(0x1234, 0x1234, true, false, false)]
    [TestCase(0x1234, 0x123F, false, true, false)]
    [TestCase(0xD234, 0x1234, false, false, true)]
    public void Execute_WhenInvoked_ShouldSetFlagsCorrectly(int lvalue, int rvalue, bool eq, bool lt, bool gt)
    {
        // CMP r1, r4 
        byte leftRegister = 1;
        byte rightRegister = 4;

        RegistersMock.SetupGet(r => r[leftRegister]).Returns((ushort)lvalue);
        RegistersMock.SetupGet(r => r[rightRegister]).Returns((ushort)rvalue);

        var sut = CreateSut(leftRegister, rightRegister);
        sut.Execute(CpuMock.Object);

        FlagsMock.VerifySet(f => f.Equal = eq);
        FlagsMock.VerifySet(f => f.GreaterThan = gt);
        FlagsMock.VerifySet(f => f.LessThan = lt);
    }


    private CompareWithRegisterInstruction CreateSut(byte register, byte rightRegister)
    {
        return new CompareWithRegisterInstruction(register, 0, rightRegister);
    }
}