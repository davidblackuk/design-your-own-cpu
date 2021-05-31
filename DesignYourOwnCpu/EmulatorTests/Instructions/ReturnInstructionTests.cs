using Emulator.Instructions;
using NUnit.Framework;

namespace EmulatorTests.Instructions
{
    public class ReturnInstructionTests: EmulatorUnitTest
    {

        [Test]
        public void Execute_WhenCalled_ShouldSetProgramCounterToValueOnStack()
        {
            ushort expectedStackPointer = 0xFFFC;
            ushort extepedReturnAddress = 0xA020;
            
            RegistersMock.SetupGet(r => r.StackPointer).Returns(expectedStackPointer);
            MemoryMock.Setup(mm => mm.GetWord(expectedStackPointer)).Returns(extepedReturnAddress);
            var sut = CreateSut(expectedStackPointer);
            sut.Execute(CpuMock.Object);
            RegistersMock.VerifySet(reg => reg.ProgramCounter = extepedReturnAddress);
            RegistersMock.VerifySet(r => r.StackPointer = (ushort)(expectedStackPointer + 4));
        }
        
        private ReturnInstruction CreateSut( ushort expectedProgramCounter)
        {
            return new ReturnInstruction(0, HighByte(expectedProgramCounter), LowByte(expectedProgramCounter));
        }
        
    }
}