using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace SharedTests
{
    public class MockAsciiMapperTests
    {
        [Test]
        [TestCase(32, ' ')] // legal to legal
        [TestCase(27, '.')] // unknown to period
        public void ConvertByteToChar_whenInvoked_MapsAsExpected(byte from, char expected)
        {
            var res = MockAsciiMapper.ConvertByteToChar(from);
            res.Should().Be(expected);
        }
        
        
        [Test]
        [TestCase(' ', 32)] // legal to legal
        [TestCase('\b', 255)] // unknown to period
        public void ConvertCharToByte_whenInvoked_MapsAsExpected(char from, byte expected)
        {
            var res = MockAsciiMapper.ConvertCharToByte(from);
            res.Should().Be(expected);
        }
    }
}