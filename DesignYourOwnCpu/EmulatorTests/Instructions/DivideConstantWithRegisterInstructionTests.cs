using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class DivideConstantWithRegisterInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldDivideTheRegisterByTHe()
        {
            byte expectedRegister = 7;
            ushort constValue = 0x3456;
            ushort initialRegisterValue = 0x1234;

            var sut = CreateSut(expectedRegister, constValue);
            RegistersMock.SetupGet(r => r[expectedRegister]).Returns(initialRegisterValue);
            sut.Execute(CpuMock.Object);

            RegistersMock.VerifySet(reg => reg[expectedRegister] = (ushort)(initialRegisterValue/constValue));
        }

        private DivideRegisterByConstantInstruction CreateSut(byte register, ushort value)
        {
            return new DivideRegisterByConstantInstruction(register, HighByte(value), LowByte(value));
        }
    }
}