using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    public class StoreRegisterLowIndirectInstructionTests: EmulatorUnitTest
    {

        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInMemory()
        {
            // LD R3, 0x2234
            // LD R6, 0x3456
            // STH R6, (R3)
            byte targetRegister = 6;
            byte expectedAddressRegister = 3;
            ushort expectedValue = 0x3456;
            ushort expectedAddress = 0x2234;

            RegistersMock.SetupGet(r => r[targetRegister]).Returns(expectedValue);
            RegistersMock.SetupGet(r => r[expectedAddressRegister]).Returns(expectedAddress);
            
            
            var sut = CreateSut(targetRegister, expectedAddressRegister);
            sut.Execute(CpuMock.Object);
            MemoryMock.VerifySet(m => m[expectedAddress] = LowByte(expectedValue));
        }

        private StoreRegisterLowIndirectInstruction CreateSut(byte register, byte indirectRegister)
        {
            return new StoreRegisterLowIndirectInstruction(register, 0, indirectRegister);
        }
    }
}