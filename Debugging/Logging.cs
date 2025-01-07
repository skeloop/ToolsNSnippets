using Tools;



public enum LogLevel
{
    // Keine weiteren Aktionen
    None,

    // Log-Nachricht der Sitzung speichern
    Log,

    // Warnmeldung in der Konsole ausgeben
    Warning,

    // Fehlermeldung ausgeben
    Error,

    // Fatale Fehlermeldung in der Konsole ausgeben
    Fatal

}

public static class Log
{
    /// <summary>
    /// Beinhaltet alle Log-Nachrichten der aktuellen Sitzung
    /// </summary>
    public static List<Message> SessionLogs = new();

    // Datenstruktur für Log und Debug-Nachrichten
    public class Message
    {
        /// <summary>
        /// Die Nachricht selbst
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Fehlergrad der Log-Nachricht
        /// </summary>
        public LogLevel Level { get; set; } = LogLevel.None;

        /// <summary>
        /// Die Textfarbe der Nachricht
        /// </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.Cyan;


        /// <summary>
        /// Schreibt die Log-Nachricht in die Konsole
        /// </summary>
        public void Print()
        {
            Console.ForegroundColor = Color;
            string prefix = "Log";
            // Log Level Prefix wenn Level nicht Standard
            if(Level != LogLevel.None) prefix = Level.ToString();
            Console.WriteLine($"{prefix} » {Text}");
            Console.ResetColor();
        }
    }
}
