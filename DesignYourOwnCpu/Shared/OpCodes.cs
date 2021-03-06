namespace Shared;

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

    public const byte MultiplyRegisterWithConstant = 0x44;
    public const byte DivideRegisterByConstant = 0x45;
    public const byte MultiplyRegisterWithRegister = 0x46;
    public const byte DivideRegisterByRegister = 0x47;

    public const byte Push = 0x50;
    public const byte Pop = 0x51;
    public const byte Call = 0x52;
    public const byte Ret = 0x53;
    public const byte Swi = 0x54;


    public const byte Nop = 0xFF;
    public const byte Halt = 0xFE;


    /// <summary>
    ///     The unused opcode is useful for unit tests as it is guaranteed never to be implemented
    /// </summary>
    public const byte Unused = 0xFD;
        
    /// <summary>
    /// Indicates the emulator read an unknown opcode
    /// </summary>
    public const byte Unknown = 0xFC;

}