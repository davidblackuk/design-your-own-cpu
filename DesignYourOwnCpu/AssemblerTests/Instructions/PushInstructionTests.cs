﻿using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    public class PushInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            var sut = CreateSut();
            sut.OpCode.Should().Be(OpCodes.Push);
        }

        private PushInstruction CreateSut() => new PushInstruction();
    }
}