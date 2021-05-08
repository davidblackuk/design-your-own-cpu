using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    public class StoreRegisterDirectInstructionTests: EmulatorUnitTest
    {

        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInMemory()
        {
            // LD R6, 0x3456
            // ST R6, (0x2234)
            byte targetRegister = 6;
            ushort expectedValue = 0x3456;
            ushort expectedAddress = 0x2234;

            RegistersMock.SetupGet(r => r[targetRegister]).Returns(expectedValue);
            
            var sut = CreateSut(targetRegister, expectedAddress);
            sut.Execute(CpuMock.Object);
            MemoryMock.Verify(m => m.SetWord(expectedAddress, expectedValue));
        }

        private StoreRegisterDirectInstruction CreateSut(byte register, ushort expectedAddress)
        {
            return new StoreRegisterDirectInstruction(register, HighByte(expectedAddress), LowByte(expectedAddress));
        }
    }
}