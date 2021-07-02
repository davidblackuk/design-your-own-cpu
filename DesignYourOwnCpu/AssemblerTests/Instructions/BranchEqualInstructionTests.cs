using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class BranchEqualInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            CreateSut().OpCode.Should().Be(OpCodes.BranchEqual);
        }

        private BranchEqualInstruction CreateSut()
        {
            return new();
        }
    }
}