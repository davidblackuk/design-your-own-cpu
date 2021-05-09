using Emulator.Instructions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace EmulatorTests.Instructions
{
    public class CompareWithConstantInstructionTests: EmulatorUnitTest
    {

        [Test]
        [TestCase(0x1234, 0x1234, true, false, false)]
        [TestCase(0x1234, 0x123F, false, true, false)]
        [TestCase(0xD234, 0x1234, false, false, true)]
        public void Execute_WhenInvoked_ShouldSetFlagsCorrectly(int lvalue, int rvalue, bool eq, bool lt, bool gt)
        {
            // CMP r1, 0xNNNN 
            byte leftRegister = 1;
            RegistersMock.SetupGet(r => r[leftRegister]).Returns((ushort)lvalue);
            
            var sut = CreateSut(leftRegister, (ushort)rvalue);
            sut.Execute(CpuMock.Object);
            
            FlagsMock.VerifySet(f => f.Equal = eq);
            FlagsMock.VerifySet(f => f.GreaterThan = gt);
            FlagsMock.VerifySet(f => f.LessThan = lt);
        }


        private CompareWithConstantInstruction CreateSut(byte register, ushort value)
        {
            return new CompareWithConstantInstruction(register, HighByte(value), LowByte(value));
        }
    }
}