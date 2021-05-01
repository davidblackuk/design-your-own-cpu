namespace Shared
{
    public class OpCodes
    {
        public const byte LoadRegisterWithConstant = 0x00;
        public const byte LoadRegisterFromRegister = 0x01;
        public const byte LoadRegisterFromMemory = 0x02;
        
        
        public const byte StoreRegisterDirect = 0x10;
        public const byte StoreRegisterIndirect = 0x40;

        public const byte StoreRegisterHiDirect = 0x30;
        public const byte StoreRegisterHiIndirect = 0x60;
        
        public const byte StoreRegisterLowDirect = 0x20;
        public const byte StoreRegisterLowIndirect = 0x50;

        public const byte Nop = 0xFF;
        public const byte Halt = 0xFE;
        
    }
}