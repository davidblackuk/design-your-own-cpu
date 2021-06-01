using System;
using Assembler.Instructions;
using Assembler.LineSources;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Assembler;
using Assembler.Symbols;
using FluentAssertions;

namespace AssemblerTests.Instructions
{
    public class ParserTests
    {
        private Mock<IInstructionNameParser> nameParserMock;
        private Mock<IAssemblerInstructionFactory> instructionFactoryMock;
        private Mock<ISymbolTable> symbolTableMock;

        [SetUp]
        public void SetUp()
        {
            nameParserMock = new Mock<IInstructionNameParser>();
            instructionFactoryMock = new Mock<IAssemblerInstructionFactory>();
            symbolTableMock = new Mock<ISymbolTable>();
        }

        [Test]
        public void Ctor_WhenInvokedWithMissingIInstructionNameParserDependency_ShouldThrowArgumentNullException()
        {
            nameParserMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }
        [Test]
        public void Ctor_WhenInvokedWithMissingIInstructionFactoryDependency_ShouldThrowArgumentNullException()
        {
            instructionFactoryMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }
        [Test]
        public void Ctor_WhenInvokedWithMissingISymbolTableDependency_ShouldThrowArgumentNullException()
        {
            symbolTableMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }

        [Test]
        public void Parse_whenInvokedWithALabelOnLineOne_DefinedTHeLabelAtAddressZero()
        {
            var sut = CreateSut();
            
            AddLoopSymbolDefinition(sut);

            symbolTableMock.Verify(st => st.DefineSymbol("loop", 0));
        }

        [Test]
        public void Parse_whenInvokedWithAnInstruction_ShouldParseAndAddIt()
        {
            var sut = CreateSut();

            var mockLineSource = new Mock<ILineSource>();
            mockLineSource.Setup(ls => ls.Lines())
                .Returns(new List<string>() {"blah"});
            nameParserMock.Setup(np => np.Parse("blah"))
                .Returns(("blah", ""));

            var mockInstruction = new Mock<IAssemblerInstruction>();
            instructionFactoryMock.Setup(i => i.Create("blah"))
                .Returns(mockInstruction.Object);
            
            sut.ParseAllLines(mockLineSource.Object);
            sut.Instructions[0].Should().Be(mockInstruction.Object);
        }

        
        private void AddLoopSymbolDefinition(Parser sut)
        {
            var mockLineSource = new Mock<ILineSource>();
            mockLineSource.Setup(ls => ls.Lines())
                .Returns(new List<string>() {".loop"});
            nameParserMock.Setup(np => np.Parse(".loop"))
                .Returns((".loop", ""));
            sut.ParseAllLines(mockLineSource.Object);
        }


        private Parser CreateSut()
        {
            return new Parser(nameParserMock?.Object, instructionFactoryMock?.Object, symbolTableMock?.Object);
        }
    }
}