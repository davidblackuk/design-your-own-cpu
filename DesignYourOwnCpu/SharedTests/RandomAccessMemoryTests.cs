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

        private RandomAccessMemory CreateSut()
        {
            return new RandomAccessMemory();
        }
    }
}
