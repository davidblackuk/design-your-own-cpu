using System.Diagnostics.CodeAnalysis;
using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class StoreRegisterLowDirectInstructionTests : EmulatorUnitTest
    {
        [Test]
        public void Execute_WhenInvoked_ShouldStoreTheValueInMemory()
        {
            // LD R6, 0x3456
            // STL R6, (0x2234)
            byte targetRegister = 6;
            ushort expectedValue = 0x3456;
            ushort expectedAddress = 0x2234;

            RegistersMock.SetupGet(r => r[targetRegister]).Returns(expectedValue);

            var sut = CreateSut(targetRegister, expectedAddress);
            sut.Execute(CpuMock.Object);
            MemoryMock.VerifySet(m => m[expectedAddress] = LowByte(expectedValue));
        }

        private StoreRegisterLowDirectInstruction CreateSut(byte register, ushort expectedAddress)
        {
            return new StoreRegisterLowDirectInstruction(register, HighByte(expectedAddress), LowByte(expectedAddress));
        }
    }
}