using System.Collections.Generic;
using Assembler.LineSources;
using NUnit.Framework;

namespace AssemblerTests.LineSources
{
    public class MemoryLineSourceTests
    {

        [Test]
        public void Line_WhenIterated_ShouldReturnAllLines()
        {
            var sut = CreateSut(TestText);
            List<string> lines = new List<string>();
            foreach (var line in sut.Lines())
            {
                lines.Add(line);
            }
            CollectionAssert.Contains(lines, "Line 1");
            CollectionAssert.Contains(lines, "Line 2");
            CollectionAssert.Contains(lines, "Line 3");
        }

        private MemoryLineSource CreateSut(string text)
        {
            return new MemoryLineSource(text);
        }

        private string TestText = @"
Line 1
Line 2
Line 3
";

    }
}