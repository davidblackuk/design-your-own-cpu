using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assembler.LineSources;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.LineSources
{
    [ExcludeFromCodeCoverage]
    public class CommentStrippingLineSourceTests
    {
        private readonly string TestText = @"
;
; this is a comment
  ;
  #
.label1
Line 1
Line 2       ; so is this
# this is also a comment
Line 3 # and this too
Line 4 ; this is not comment #4 (nasty use of both comment chars
Line 5 # Really this is also; erm... possible
";

        [Test]
        public void Line_WhenIterated_ShouldRemoveEmptyLines()
        {
            var sut = CreateSut(TestText);
            var lines = new List<string>();
            foreach (var line in sut.Lines()) lines.Add(line);

            lines.Count.Should().Be(6);
            CollectionAssert.Contains(lines, ".label1");
            CollectionAssert.Contains(lines, "Line 1");
            CollectionAssert.Contains(lines, "Line 2");
            CollectionAssert.Contains(lines, "Line 3");
        }

        private CommentStrippingLineSource CreateSut(string text)
        {
            var res = new CommentStrippingLineSource();
            res.ChainTo(new WhitespaceRemovalLineSource()
               .ChainTo(new MemoryLineSource(text)));
            return res;
        }
    }
}