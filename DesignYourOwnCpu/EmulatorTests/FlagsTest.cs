using System.Diagnostics.CodeAnalysis;
using Emulator;
using FluentAssertions;
using NUnit.Framework;

namespace EmulatorTests
{
    [ExcludeFromCodeCoverage]
    public class FlagsTest
    {
        [Test]
        public void Equal_WhenGetOrSet_ShouldGetOrSetTheValue()
        {
            var sut = CreateSut();
            sut.Equal.Should().BeFalse();
            sut.Equal = true;
            sut.Equal.Should().BeTrue();
            sut.Equal = false;
            sut.Equal.Should().BeFalse();
        }

        [Test]
        public void GreaterThan_WhenGetOrSet_ShouldGetOrSetTheValue()
        {
            var sut = CreateSut();
            sut.GreaterThan.Should().BeFalse();
            sut.GreaterThan = true;
            sut.GreaterThan.Should().BeTrue();
            sut.GreaterThan = false;
            sut.GreaterThan.Should().BeFalse();
        }

        [Test]
        public void LessThan_WhenGetOrSet_ShouldGetOrSetTheValue()
        {
            var sut = CreateSut();
            sut.LessThan.Should().BeFalse();
            sut.LessThan = true;
            sut.LessThan.Should().BeTrue();
            sut.LessThan = false;
            sut.LessThan.Should().BeFalse();
        }

        [Test]
        public void Halted_WhenGetOrSet_ShouldGetOrSetTheValue()
        {
            var sut = CreateSut();
            sut.Halted.Should().BeFalse();
            sut.Halted = true;
            sut.Halted.Should().BeTrue();
            sut.Halted = false;
            sut.Halted.Should().BeFalse();
        }

        private Flags CreateSut()
        {
            return new();
        }
    }
}