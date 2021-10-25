using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class LoadRegisterFromRegisterInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInTheRegister()
        {
            // LD R4, R2
            byte targetRegister = 4;
            byte sourceRegister = 2;
            ushort expectedValue = 0x3456;

            RegistersMock.SetupGet(r => r[sourceRegister]).Returns(expectedValue);
            var sut = CreateSut(targetRegister, sourceRegister);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(r => r[targetRegister] = expectedValue);
        }

        private LoadRegisterFromRegisterInstruction CreateSut(byte register, byte sourceRegister)
        {
            return new LoadRegisterFromRegisterInstruction(register, 0, sourceRegister);
        }
    }
}