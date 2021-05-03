using Assembler.Instructions;
using Castle.DynamicProxy.Generators;
using FluentAssertions;
using NUnit.Framework;
using Shared;

namespace AssemblerTests.Instructions
{
    
    public class BranchInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode()
        {
            var sut = CreateSut();
            sut.OpCode.Should().Be(OpCodes.BranchLessThan);
        }

        [Test]
        public void Parse_WhenItIsALabel_ShouldStoreTHeSymbolCorrectly()
        {
            var sut = CreateSut();
            var expected = "loop1";
            sut.Parse(expected);
            sut.Symbol.Should().Be(expected);
            sut.RequresSymbolResolution.Should().BeTrue();
        }

        [Test]
        public void Parse_WhenItIsAValue_ShouldNotMarkAsRequiresSymbolResolution()
        {
            var sut = CreateSut();
            sut.Parse("0xFFEE");
            sut.RequresSymbolResolution.Should().BeFalse();
        }

        [Test]
        public void Parse_WhenItIsAValue_ShouldStoreTheValueCorrectly()
        {
            var sut = CreateSut();
            sut.Parse("0xFFEE");
            sut.ByteHigh.Should().Be(0xFF);
            sut.ByteLow.Should().Be(0xEE);
        }

        
        private BranchInstruction CreateSut() => new BranchInstruction("blt", OpCodes.BranchLessThan);
    }
    
    
    public class BranchEqualInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode() => CreateSut().OpCode.Should().Be(OpCodes.BranchEqual);

        private BranchEqualInstruction CreateSut() => new BranchEqualInstruction();
    }
    
    public class BranchGreaterThanInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode() => CreateSut().OpCode.Should().Be(OpCodes.BranchGreaterThan);

        private BranchGreaterThanInstruction CreateSut() => new BranchGreaterThanInstruction();
    }
    
    public class BranchLessthanInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode() => CreateSut().OpCode.Should().Be(OpCodes.BranchLessThan);

        private BranchLessThanInstruction CreateSut() => new BranchLessThanInstruction();
    }
    
    public class BranchAlwaysInstructionTests
    {
        [Test]
        public void Ctor_WhenInvoked_ShouldCorrectlySetOpCode() => CreateSut().OpCode.Should().Be(OpCodes.Branch);

        private BranchAlwaysInstruction CreateSut() => new BranchAlwaysInstruction();
    }
}