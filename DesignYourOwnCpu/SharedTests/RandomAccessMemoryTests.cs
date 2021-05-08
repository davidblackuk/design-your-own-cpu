using NUnit.Framework;
using System;
using FluentAssertions;
using Shared;

namespace SharedTests
{
    public class RandomAccessMemoryTests
    {
        [Test]
        public void RawMemory_WhenInitialized_ShouldHaveSixteenBitMaxSize()
        {
            var sut = CreateSut();
            sut.RawBytes.Length.Should().Be(65536);
        }

        [Test]
        public void Index_WhenDataIsStoredAtAddress_ShouldBeStoredAtThatAddress()
        {
            ushort expectedAddress = 0xDD00;
            byte expectedValue = 0x49;

            var sut = CreateSut();
            
            sut[expectedAddress] = expectedValue;
            sut[expectedAddress].Should().Be(expectedValue);
        }

        [Test]
        public void Instruction_WhenCalled_returnsTheInstructionDataForTheAddress()
        {
            var sut = CreateSut();
            byte expectedOpCode = 0xFF;
            byte expectedRegister = 0xEE;
            byte expectedByteHigh = 0xDD;
            byte expectedByteLow = 0xCC;
            
            sut[12] = expectedOpCode;
            sut[13] = expectedRegister;
            sut[14] = expectedByteHigh;
            sut[15] = expectedByteLow;
            
            var res = sut.Instruction(12);
            
            res.opcode.Should().Be(expectedOpCode);
            res.register.Should().Be(expectedRegister);
            res.byteHigh.Should().Be(expectedByteHigh);
            res.byteLow.Should().Be(expectedByteLow);
        }

        [Test]
        public void GetWord_WWhenCalled_returnsValueInABigEndianFormat()
        {
            var sut = CreateSut();
            
            sut[12] = 0xFF;
            sut[13] = 0x55;

            sut.GetWord(12).Should().Be(0xFF55);

        }
        
        private RandomAccessMemory CreateSut()
        {
            return new RandomAccessMemory();
        }
    }
}
