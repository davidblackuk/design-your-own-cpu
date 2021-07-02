using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class BranchGreaterThanInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            CreateSut().OpCode.Should().Be(OpCodes.BranchGreaterThan);
        }

        private BrachGreaterThanInstruction CreateSut()
        {
            return new();
        }
    }
}