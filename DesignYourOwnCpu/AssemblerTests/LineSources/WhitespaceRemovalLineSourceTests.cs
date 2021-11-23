using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assembler.LineSources;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.LineSources
{
    [ExcludeFromCodeCoverage]
    public class WhitespaceRemovalLineSourceTests
    {
        private readonly string TestText = @"
Line 1

Line 2
Line 3

";

        [Test]
        public void Line_WhenIterated_ShouldRemoveEmptyLines()
        {
            var sut = CreateSut(TestText);
            var lines = new List<string>();
            foreach (var line in sut.Lines()) lines.Add(line);

            lines.Count.Should().Be(3);
            CollectionAssert.Contains(lines, "Line 1");
            CollectionAssert.Contains(lines, "Line 2");
            CollectionAssert.Contains(lines, "Line 3");
        }

        private WhitespaceRemovalLineSource CreateSut(string text)
        {
            var res = new WhitespaceRemovalLineSource();
            res.ChainTo(new MemoryLineSource(text));
            return res;
        }
    }
}