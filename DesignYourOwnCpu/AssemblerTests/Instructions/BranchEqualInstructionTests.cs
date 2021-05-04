using Assembler.Instructions;
using Castle.DynamicProxy.Generators;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    public class BranchEqualInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode() => CreateSut().OpCode.Should().Be(OpCodes.BranchEqual);

        private BranchEqualInstruction CreateSut() => new BranchEqualInstruction();
    }
}