using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using Pastel;
using Shared;

namespace Emulator.Instructions.Interrupts
{
    /// <summary>
    /// Reads a word from the console. Acceptable formats
    /// <para>01234 (octal)</para>
    /// <para>0x1234 (hexadecimal)</para>
    /// <para>1234 (decimal)</para>
    /// </summary>
    public class ReadWordInterrupt : IInterrupt
    {
        /// <summary>
        /// TODO: We should inject this
        /// </summary>
        private readonly INumberParser numberParser;

        public ReadWordInterrupt(INumberParser numberParser)
        {
            this.numberParser = numberParser;
        }
        
        public void Execute(ICPU cpu)
        {
            string text = EditNumber();
            cpu.Registers[0] = numberParser.Parse(text);
        }

        private string EditNumber()
        {
            bool finished = false;
            StringBuilder text = new StringBuilder();
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                string key = keyInfo.KeyChar.ToString();
                if (keyInfo.Key == ConsoleKey.Enter && text.Length > 0)
                {
                    finished = true;
                    Console.WriteLine();
                }
                else if (Char.IsDigit(key[0]) || (key.ToLowerInvariant() == "x" && text.Length == 1 && text[0] == '0'))
                {
                    text.Append(key);
                    Console.Out.Write(key.Pastel(Color.DodgerBlue));
                }
            } while (!finished);

            return text.ToString();
        }
    }
}


