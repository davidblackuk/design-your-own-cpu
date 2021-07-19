using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace SharedTests
{
    [ExcludeFromCodeCoverage]
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
        public void GetWord_WhenCalled_returnsValueInABigEndianFormat()
        {
            var sut = CreateSut();

            sut[12] = 0xFF;
            sut[13] = 0x55;

            sut.GetWord(12).Should().Be(0xFF55);
        }   
        
        [Test]
        public void GetWord_WhenCalled_ShouldStoreTheValueInABigEndianFormat()
        {
            var sut = CreateSut();
            sut.SetWord(12, 0x1278);
            
            sut.GetWord(12).Should().Be(0x1278);

            sut[12].Should().Be(0x12);
            sut[13].Should().Be(0x78);
        }
       
        [Test]
        public void Ctor_WhenInvokedWithAByteArray_ShouldInitializeMemory()
        {
            var originalRam = CreateSut();
            originalRam.SetWord(12, 0x1278);

            var clonedRam = CreateSut(originalRam.RawBytes);
            
            clonedRam.GetWord(12).Should().Be(0x1278);
        }

      
        [Test]
        [TestCase(RandomAccessMemory.RamTop)]
        [TestCase(RandomAccessMemory.RamTop + 2)]
        public void Ctor_WhenInvokedWithAIllegallySizedByteArray_ShouldThrowArgumentException(int arraySize)
        {
            byte[] ram = new byte[arraySize];
            Assert.Throws<ArgumentException>(() => CreateSut(ram));
        }   
        
        private RandomAccessMemory CreateSut(byte [] bytes = null)
        {
            if (bytes == null)
            {
                return new RandomAccessMemory();
            }
            else
            {
                return new RandomAccessMemory(bytes);
            }
        }
    }
}