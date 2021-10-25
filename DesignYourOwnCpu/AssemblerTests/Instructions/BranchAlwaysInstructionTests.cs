using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class BranchAlwaysInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            CreateSut().OpCode.Should().Be(OpCodes.Branch);
        }

        private BranchAlwaysInstruction CreateSut()
        {
            return new BranchAlwaysInstruction();
        }
    }
}