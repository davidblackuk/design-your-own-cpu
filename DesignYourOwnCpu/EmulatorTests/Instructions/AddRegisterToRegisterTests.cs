using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class AddRegisterToRegisterTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldAddTheConstantToTheRigister()
        {
            // ADD R7, R4
            byte firstRegister = 7;
            byte secondRegister = 4;

            ushort firstRegisterValue = 0x1234;
            ushort secondRegisterValue = 0x3456;

            RegistersMock.SetupGet(r => r[firstRegister]).Returns(firstRegisterValue);
            RegistersMock.SetupGet(r => r[secondRegister]).Returns(secondRegisterValue);

            var sut = CreateSut(firstRegister, secondRegister);

            sut.Execute(CpuMock.Object);

            RegistersMock.VerifySet(reg => reg[firstRegister] = (ushort) (secondRegisterValue + firstRegisterValue));
        }

        private AddRegisterToRegisterInstruction CreateSut(byte register, byte otherRegister)
        {
            return new(register, 0, otherRegister);
        }
    }
}