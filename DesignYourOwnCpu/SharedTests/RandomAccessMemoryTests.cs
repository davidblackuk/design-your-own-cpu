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


        private RandomAccessMemory CreateSut()
        {
            return new RandomAccessMemory();
        }
    }
}
