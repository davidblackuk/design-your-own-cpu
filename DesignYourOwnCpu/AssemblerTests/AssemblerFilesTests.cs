using Assembler;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace AssemblerTests
{
    public class AssemblerFilesTests
    {
        private Mock<IConfigurationRoot> configurationMock;

        [SetUp]
        public void SetUp()
        {
            configurationMock = new Mock<IConfigurationRoot>();
        }

        [Test]
        [TestCase("foo.txt", "foo.txt")]
        [TestCase("foo", "foo")]
        public void SourceFilename_WhenRead_ComesFromConfig(string path, string resolvedPath)
        {
            configurationMock.SetupGet(c => c["input"]).Returns(path);
            var sut = CreateSut();
            sut.SourceFilename.Should().Be(resolvedPath);
        }

        [Test]
        [TestCase("foo.txt", "foo.sym")]
        [TestCase("foo", "foo.sym")]
        public void SymbolFilename_WhenRead_HasExtensionSymEvenIfSourceFileNameDidNot(string path, string resolvedPath)
        {
            configurationMock.SetupGet(c => c["input"]).Returns(path);
            var sut = CreateSut();
            sut.SymbolFilename.Should().Be(resolvedPath);
        }

        [Test]
        [TestCase("foo.txt", "foo.bin")]
        [TestCase("foo", "foo.bin")]
        public void BinaryFilename_WhenRead_HasExtensionSymEvenIfSourceFileNameDidNot(string path, string resolvedPath)
        {
            configurationMock.SetupGet(c => c["input"]).Returns(path);
            var sut = CreateSut();
            sut.BinaryFilename.Should().Be(resolvedPath);
        }

        private AssemblerFiles CreateSut()
        {
            return new AssemblerFiles(configurationMock?.Object);
        }
    }
}