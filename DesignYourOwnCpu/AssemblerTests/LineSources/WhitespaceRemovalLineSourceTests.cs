using System.Collections.Generic;
using Assembler.LineSources;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.LineSources
{
    public class WhitespaceRemovalLineSourceTests
    {

        [Test]
        public void Line_WhenIterated_ShouldRemoveEmptyLines()
        {
            var sut = CreateSut(TestText);
            List<string> lines = new List<string>();
            foreach (var line in sut.Lines())
            {
                lines.Add(line);
            }

            lines.Count.Should().Be(3);
            CollectionAssert.Contains(lines, "Line 1");
            CollectionAssert.Contains(lines, "Line 2");
            CollectionAssert.Contains(lines, "Line 3");
        }

        private WhitespaceRemovalLineSource CreateSut(string text)
        {
            return new WhitespaceRemovalLineSource(new  MemoryLineSource(text));
        }

        private string TestText = @"
Line 1

Line 2
Line 3

";

    }
}