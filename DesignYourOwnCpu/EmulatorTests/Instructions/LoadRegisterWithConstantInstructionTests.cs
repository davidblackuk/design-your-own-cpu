using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    public class LoadRegisterWithConstantInstructionTests: EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInTheRegister()
        {
            var sut = CreateSut(4, 0xFFEE);

            sut.Execute(CpuMock.Object);

            RegistersMock.VerifySet(r => r[4] = 0xFFEE);
        }

        private LoadRegisterWithConstantInstruction CreateSut(byte register, ushort value)
        {
            return new LoadRegisterWithConstantInstruction(register, HighByte(value), LowByte(value));
        }
    }
}