using System;
using System.Collections.Generic;
using Assembler;
using Assembler.Instructions;
using Assembler.Symbols;
using Moq;
using NUnit.Framework;
using Shared;

namespace AssemblerTests
{
    public class CodeGeneratorTests
    {
        private const byte ExpectedOpCode = 0x23;
        private const byte ExpectedRegister = 0x03;
        private const byte ExpectedByteHigh = 0xEE;
        private const byte ExpectedByteLow = 0xF4;
        
        private Mock<ISymbolTable> symbolTableMock;
        private Mock<IRandomAccessMemory> memoryMock;
        private Mock<IAssemblerInstruction> mockInstruction;

        [SetUp]
        public void SetUp()
        {
            memoryMock = new Mock<IRandomAccessMemory>();
            symbolTableMock = new Mock<ISymbolTable>();
            
            mockInstruction = new Mock<IAssemblerInstruction>();
            mockInstruction.SetupGet(instr => instr.OpCode).Returns(ExpectedOpCode);
            mockInstruction.SetupGet(instr => instr.Register).Returns(ExpectedRegister);
            mockInstruction.SetupGet(instr => instr.ByteHigh).Returns(ExpectedByteHigh);
            mockInstruction.SetupGet(instr => instr.ByteLow).Returns(ExpectedByteLow);
        }

        [Test]
        public void Ctor_WhenInvokedWithMissingSymbolTableDependancy_ShouldThorwArgumentNullException()
        {
            symbolTableMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }

        [Test]
        public void Ctor_WhenInvokedWithMissingRamDependancy_ShouldThorwArgumentNullException()
        {
            memoryMock = null;
            Assert.Throws<ArgumentNullException>(() => CreateSut());
        }

        [Test]
        public void GenerateCode_WhenCalled_ShouldStoreTheCorrectBytes()
        {
            var instructions = new List<IAssemblerInstruction>() {mockInstruction.Object};
            var sut = CreateSut();
            sut.GenerateCode(instructions);
            mockInstruction.Verify(i => i.WriteBytes(memoryMock.Object, 0));
        }

        [Test]
        public void GenerateCode_WhenCalledWithAnInstructionRequiringResolution_ShouldResolveTheAddress()
        {
            var expectedSymbol = "sym";
            ushort expectedAddress = 0xFEAA;
            mockInstruction.SetupGet(i => i.RequresSymbolResolution).Returns(true);
            mockInstruction.SetupGet(i => i.Symbol).Returns(expectedSymbol);

            Assembler.Symbols.Symbol symbol = new Assembler.Symbols.Symbol(expectedSymbol, expectedAddress);
            symbolTableMock.Setup(st => st.GetSymbol(expectedSymbol)).Returns(symbol);
            
            var instructions = new List<IAssemblerInstruction>() {mockInstruction.Object};
            var sut = CreateSut();
            sut.GenerateCode(instructions);
            
            mockInstruction.Verify(i => i.StoreData(expectedAddress));
            
        }

        
        private CodeGenerator CreateSut()
        {
            return new CodeGenerator(symbolTableMock?.Object, memoryMock?.Object);
        }
    }
}