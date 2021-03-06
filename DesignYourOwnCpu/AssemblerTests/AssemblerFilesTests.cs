using System.Diagnostics.CodeAnalysis;
using Assembler;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace AssemblerTests;

[ExcludeFromCodeCoverage]
public class AssemblerFilesTests
{
    private Mock<IConfigurationRoot> configurationMock;

    [SetUp]
    public void SetUp()
    {
        configurationMock = new Mock<IConfigurationRoot>();
    }
        
    [Test]
    public void QuietOutput_WhenValueTrue_ShouldBeTrue()
    {
        configurationMock.SetupGet(c => c["quiet"]).Returns("true");
        var sut = CreateSut();
        sut.QuietOutput.Should().BeTrue();
    }

    [Test]
    public void QuietOutput_WhenValueNull_ShouldBeFalse()
    {
        var sut = CreateSut();
        sut.QuietOutput.Should().BeFalse();
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

    private AssemblerConfig CreateSut()
    {
        return new AssemblerConfig(configurationMock?.Object);
    }
}