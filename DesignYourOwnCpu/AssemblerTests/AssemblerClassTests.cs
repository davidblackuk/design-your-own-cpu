using System;
using System.Collections.Generic;
using Assembler;
using Assembler.Instructions;
using Assembler.LineSources;
using Moq;
using NUnit.Framework;

namespace AssemblerTests
{
    /// <summary>
    /// Not the best class name but avoid conflicts with the name space / project
    /// </summary>
    public class AssemblerClassTests
    {
        private Mock<IParser> parserMock;
        private Mock<ICodeGenerator> codeGeneratorMock;
        private Mock<ILineSource> lineSourceMock;

        [SetUp]
        public void SetUp()
        {
            parserMock = new Mock<IParser>();
            codeGeneratorMock = new Mock<ICodeGenerator>();
            lineSourceMock = new Mock<ILineSource>();
        }

        [Test]
        public void Ctor_whenInvokeWithMissingParserDependency_ShouldThrowArgumentNullException()
        {
            parserMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }

        [Test]
        public void Ctor_whenInvokeWithMissingCodeGeneratorDependency_ShouldThrowArgumentNullException()
        {
            codeGeneratorMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }

        [Test]
        public void Assemble_WhenInvoked_ShouldPArseAndGenerateCode()
        {
            var instructions = new List<IAssemblerInstruction>();
            parserMock.SetupGet(p => p.Instructions).Returns(instructions);

            var sut = CreateSut();
            sut.Assemble(lineSourceMock.Object);
            
            parserMock.Verify(pm => pm.ParseAllLines(lineSourceMock.Object), Times.Once);
            codeGeneratorMock.Verify(cg => cg.GenerateCode(instructions), Times.Once);
        }
        
        private Assembler.Assembler CreateSut()
        {
            return new Assembler.Assembler(parserMock?.Object, codeGeneratorMock?.Object);
        }
    }
}