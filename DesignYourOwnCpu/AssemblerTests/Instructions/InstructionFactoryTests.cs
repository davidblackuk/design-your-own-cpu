using System;
using System.Data;
using Assembler.Exceptions;
using Assembler.Instructions;
using FluentAssertions;
using NUnit.Framework;

namespace AssemblerTests.Instructions
{
    public class InstructionFactoryTests
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
        public void Create_WhenCalledWithAKnownInstructionName_ShouldReturnCorrectType(
            string instructionName, Type expectedType)
        {
            var sut = CreateSut();
            var instruction = sut.Create(instructionName);
            instruction.GetType().Name.Should().Be(expectedType.Name);
        }

        private InstructionFactory CreateSut()
        {
            return new InstructionFactory();
        }
    }
}