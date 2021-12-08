using System.Diagnostics.CodeAnalysis;
using Emulator;
using Moq;
using NUnit.Framework;
using Shared;

namespace EmulatorTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class EmulatorUnitTest
    {
        protected Mock<ICpu> CpuMock;
        protected Mock<IFlags> FlagsMock;
        protected Mock<IRandomAccessMemory> MemoryMock;
        protected Mock<IRegisters> RegistersMock;

        [SetUp]
        public virtual void SetUp()
        {
            CpuMock = new Mock<ICpu>();
            RegistersMock = new Mock<IRegisters>();
            MemoryMock = new Mock<IRandomAccessMemory>();
            FlagsMock = new Mock<IFlags>();
            CpuMock.SetupGet(cpu => cpu.Registers).Returns(RegistersMock.Object);
            CpuMock.SetupGet(cpu => cpu.Memory).Returns(MemoryMock.Object);
            CpuMock.SetupGet(cpu => cpu.Flags).Returns(FlagsMock.Object);
        }

        protected byte HighByte(ushort value)
        {
            return (byte)(value >> 8);
        }

        protected byte LowByte(ushort value)
        {
            return (byte)(value & 0xff);
        }
    }
}