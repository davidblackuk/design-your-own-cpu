namespace Shared
{
    public class OpCodes
    {
        public const byte LoadRegisterWithConstant = 0x00;
        public const byte LoadRegisterFromRegister = 0x01;
        public const byte LoadRegisterFromMemory = 0x02;
        
        
        public const byte StoreRegisterDirect = 0x10;
        public const byte StoreRegisterLowDirect = 0x11;
        public const byte StoreRegisterHiDirect = 0x12;
        public const byte StoreRegisterIndirect = 0x13;
        public const byte StoreRegisterLowIndirect = 0x14;
        public const byte StoreRegisterHiIndirect = 0x15;
        
        
        public const byte CompareWithRegister = 0x20;
        public const byte CompareWithConstant = 0x21;


        public const byte BranchEqual = 0x30;
        public const byte BranchGreaterThan = 0x31;
        public const byte BranchLessThan = 0x32;
        public const byte Branch = 0x33;

        public const byte AddConstantToRegister = 0x40;
        public const byte SubtractConstantFromRegister = 0x41;
        public const byte AddRegisterToRegister = 0x42;
        public const byte SubtractRegisterFromRegister = 0x43;
        
        public const byte Nop = 0xFF;
        public const byte Halt = 0xFE;
        
    }
    
}