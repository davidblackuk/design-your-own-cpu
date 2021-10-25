using System;
using System.Diagnostics.CodeAnalysis;
using Assembler.Exceptions;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Instructions
{
    [ExcludeFromCodeCoverage]
    public class AssemblerInstructionFactoryTests
    {
        [Test]
        public void Create_WhenInvokedWithAnUnknownInstruction_ShouldThrow()
        {
            var sut = CreateSut();
            Assert.Throws<AssemblerException>(() => sut.Create("blah"));
        }

        [Test]
        [TestCase("nop", typeof(NopInstruction))]
        [TestCase("halt", typeof(HaltInstruction))]
        [TestCase("ld", typeof(LoadInstruction))]
        [TestCase("st", typeof(StoreInstruction))]
        [TestCase("stl", typeof(StoreLowInstruction))]
        [TestCase("sth", typeof(StoreHiInstruction))]
        [TestCase("cmp", typeof(CompareInstruction))]
        [TestCase("blt", typeof(BranchLessThanInstruction))]
        [TestCase("bgt", typeof(BrachGreaterThanInstruction))]
        [TestCase("beq", typeof(BranchEqualInstruction))]
        [TestCase("bra", typeof(BranchAlwaysInstruction))]
        [TestCase("add", typeof(AddInstruction))]
        [TestCase("sub", typeof(SubtractInstruction))]
        [TestCase("call", typeof(CallInstruction))]
        [TestCase("ret", typeof(ReturnInstruction))]
        [TestCase("push", typeof(PushInstruction))]
        [TestCase("pop", typeof(PopInstruction))]
        public void Create_WhenCalledWithAKnownInstructionName_ShouldReturnCorrectType(
            string instructionName, Type expectedType)
        {
            var sut = CreateSut();
            var instruction = sut.Create(instructionName);
            instruction.GetType().Name.Should().Be(expectedType.Name);
        }

        private AssemblerInstructionFactory CreateSut()
        {
            return new AssemblerInstructionFactory();
        }
    }
}