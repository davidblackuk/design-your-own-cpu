namespace Shared
{
    public class OpCodes
    {
        public const byte LoadRegisterWithConstant = 0x00;
        public const byte LoadRegisterFromRegister = 0x01;
        public const byte LoadRegisterFromMemory = 0x02;

        public const byte Nop = 0xFF;
        public const byte Halt = 0xFE;
        
    }
}