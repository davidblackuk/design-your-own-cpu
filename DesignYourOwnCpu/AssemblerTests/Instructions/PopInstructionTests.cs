using System.Diagnostics.CodeAnalysis;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class PopInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            var sut = CreateSut();
            sut.OpCode.Should().Be(OpCodes.Pop);
        }

        private PopInstruction CreateSut()
        {
            return new();
        }
    }
}