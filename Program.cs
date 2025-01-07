
namespace Tools
{
    class Preset : PresetBase {}
    public class Program
    {
        static ConsoleWriter consoleWriter = new(new Preset());
        
        /// <summary>
        /// Einstiegsspunkt für Tools.
        /// Wird nur verwendet zum Testen, innerhalb dieser Bibliothek (Tools). 
        /// </summary>
        static void Main(string[] args)
        {
            consoleWriter.Write(new string('.', Console.BufferWidth));
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write(new string('.', Console.BufferWidth));
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
            consoleWriter.Write("Hallo 2");
        }
    }
}
