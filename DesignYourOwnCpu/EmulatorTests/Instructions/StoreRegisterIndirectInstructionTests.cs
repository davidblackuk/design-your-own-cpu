using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class StoreRegisterIndirectInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInMemory()
        {
            // LD R3, 0x2234
            // LD R6, 0x3456
            // ST R6, (R3)
            byte targetRegister = 6;
            byte expectedAddressRegister = 3;
            ushort expectedValue = 0x3456;
            ushort expectedAddress = 0x2234;

            RegistersMock.SetupGet(r => r[targetRegister]).Returns(expectedValue);
            RegistersMock.SetupGet(r => r[expectedAddressRegister]).Returns(expectedAddress);


            var sut = CreateSut(targetRegister, expectedAddressRegister);
            sut.Execute(CpuMock.Object);
            MemoryMock.Verify(m => m.SetWord(expectedAddress, expectedValue));
        }

        private StoreRegisterIndirectInstruction CreateSut(byte register, byte indirectRegister)
        {
            return new(register, 0, indirectRegister);
        }
    }
}