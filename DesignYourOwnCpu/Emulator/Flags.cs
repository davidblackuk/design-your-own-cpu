namespace Emulator
{
    public class Flags : IFlags
    {
        public bool Equal { get; set; }
        public bool LessThan { get; set; }
        public bool GreaterThan { get; set; }
        
        public bool Halted { get; set; }
    }
}
