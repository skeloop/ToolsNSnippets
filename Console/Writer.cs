using System;
using static System.ConsoleColor;

namespace Tools;

/// <summary>
/// Klasse zum Schreiben von formatierten Nachrichten in die Konsole.
/// </summary>
public class ConsoleWriter
{
    #region Constructor
    // Dieses Preset wird Standardsgemäß verwendet
    private PresetBase _presetBase;
    
    public ConsoleWriter(PresetBase presetBase)
    {
        _presetBase = presetBase;
    }
    #endregion
    /// <summary>
    /// Schreibt eine formatierte Nachricht in die Konsole.
    /// </summary>
    /// <param name="text">         Der Text, der in die Konsole geschrieben wird.                                                                       </param>
    /// <param name="prefix">       Optionaler Text, der vor dem Haupttext angezeigt wird.                                                               </param>
    /// <param name="suffix">       Optionaler Text, der nach dem Haupttext angezeigt wird.                                                              </param>
    public void Write(
        string text,
        string prefix = "",
        string suffix = ""
    )
    {
        #region Positionierung
        // Speichern der aktuellen Cursorposition, um sie später bei Bedarf wiederherzustellen
        var originalCursorPosition = Console.GetCursorPosition();

        // Überprüfen, ob die Positionswerte gültig sind und den Cursor an die gewünschte Position setzen
        if (_presetBase.CursorPosition.x >= 0 && _presetBase.CursorPosition.x < Console.BufferWidth && _presetBase.CursorPosition.y >= 0 && _presetBase.CursorPosition.y < Console.BufferHeight)
        {
            Console.SetCursorPosition(_presetBase.CursorPosition.x, _presetBase.CursorPosition.y);
        }
        #endregion

        #region Nachrichtenverarbeitung
        // Erstellen der gesamten Nachricht mit Präfix und Suffix
        string parsedMessage = $"{prefix}{text}{suffix}";
        char[] messageChars = parsedMessage.ToCharArray();

        // Speichern der Längen der Textteile für die Farbgebung
        int prefixLength = prefix.Length;
        int textLength = text.Length;
        int suffixLength = suffix.Length;
        #endregion

        #region Ausgabe
        // Iterieren durch die Zeichen der Nachricht und Ausgeben

        for (int i = 0; i < messageChars.Length; i++)
        {
            #region Color
            // Farbgebung basierend auf dem Textteil
            if (i < prefixLength)
            {
                Console.ForegroundColor = _presetBase.PrefixColor; // Farbe für den Präfix
            }
            else if (i < prefixLength + textLength)
            {
                Console.ForegroundColor = _presetBase.Color; // Farbe für den Haupttext
            }
            else
            {
                Console.ForegroundColor = _presetBase.SuffixColor; // Farbe für den Suffix
            }
            #endregion

            int bufferRange = Console.BufferWidth - 1;
            bool inBuffer = Console.CursorLeft <= bufferRange;
            
            // Abbruch wenn Cursor nicht im Puffers ist
            if (!inBuffer && !_presetBase.AllowPush) break;
            // nur in neue Zeile gehen wenn Push erlaubt ist und Cursor nicht im Puffer 
            else if (!inBuffer && _presetBase.AllowPush) Console.Write('\n');
            
            // danach Zeichen aus Nachricht schreiben
            Console.Write(messageChars[i]);
        }

        #endregion

        #region Benutzerinteraktion
        if (_presetBase.ReturnKey != ConsoleKey.None)
            WaitForUserInput(_presetBase.ReturnKey);
        #endregion

        #region Rücksetzung
        // Zurücksetzen der Cursorposition, wenn aktiviert
        if (_presetBase.ResetPosition)
        {
            Console.SetCursorPosition(originalCursorPosition.Left, originalCursorPosition.Top);
        }

        // Zurücksetzen der Konsolenfarbe
        Console.ResetColor();
        // in neue Zeile gehen
        Console.WriteLine(" ");
        #endregion
    }

    /// <summary>
    /// Wartet auf eine Benutzereingabe.
    /// </summary>
    /// <param name="returnKey">Die Taste, auf die gewartet wird. Wenn 'ConsoleKey.None' wird auf eine beliebige Taste gewartet.</param>
    public static void WaitForUserInput(ConsoleKey returnKey)
    {
        while (true)
        {
            var key = Console.ReadKey(true).Key;

            // Reaktionstaste abfragen
            if (returnKey == ConsoleKey.None)
            {
                break;
            }
        }
    }
}
    
//[Obsolete("Veraltete Funktionen")]
public static class ConsoleHelper
{
    #region Cursor
    /// <summary>
    /// Lässt den Cursor blinken.
    /// </summary>
    /// <param name="repeat">Die Anzahl der Wiederholungen. </param>
    /// <param name="durationPerIterationMS">Die Dauer einer Wiederholung in Millisekunden. </param>
    [Obsolete("Muss neu gemacht werden")] public static void CursorBlink(int repeat = 10, int durationPerIterationMS = 500)
    {
        var pos = Console.GetCursorPosition();
        for (int i = 0; i < repeat; i++)
        {

            Console.SetCursorPosition(pos.Left, pos.Top);
            //"■".Write(newLine: false, color: Cyan);
            Thread.Sleep(durationPerIterationMS);
            Console.SetCursorPosition(pos.Left, pos.Top);
            //"■".Write(newLine: false, color: Black);
            Thread.Sleep(durationPerIterationMS);

        }
        Console.SetCursorPosition(pos.Left, pos.Top);
        //" ".Write(newLine: false);
    }
    #endregion

    /// <summary>
    /// Schreibt einen Text als einzelne Ziffern Rückwärts von Rechts nach Links.
    /// Text kann umgekehrt werden
    /// </summary>
    /// <param name="text">   </param>
    /// <param name="reverse">Gibt an ob der Text gespiegelt sein soll </param>
    /// <param name="position">Die Position im Fenster wo der Text geschrieben wird </param>
    [Obsolete("Muss neu gemacht werden")] public static void WriteBackwards(this string text, (int x, int y) position, bool reverse = false)
    {
        var @chars = text.ToCharArray();
        if (reverse) @chars.Reverse();

        var startPosition = Console.GetCursorPosition();
        Console.SetCursorPosition(position.x, position.y);

        if (position.x - chars.Length < 0)
        {
            //"Nicht genug Platz um Text zu schreiben!: ".Write(color: Red, wait: true, suffix: text);
            return;
        }

        for (int i = chars.Length - 1; i > 0; i--)
        {
            Console.SetCursorPosition(Console.GetCursorPosition().Left - 2, position.y);
            Console.Write(@chars[i]);
        }
    }
        
        
    /// <summary>
    /// Schreibt einen Text , in der Fenstermitte
    /// </summary>
    /// <param name="text">        </param>
    /// <param name="widthOffset">+1 nach rechts | -1 nach links </param>
    /// <param name="heightOffset">+1 nach unten | -1 nach oben </param>
    /* [Obsolete("Muss neu gemacht werden")] */ public static void WriteMiddle(string text, int widthOffset = 0, int heightOffset = 0)
    {
        var lenght = text.Length;
        var middle = Console.BufferWidth / 2;
        var start = middle - lenght / 2;

        Console.SetCursorPosition(start + widthOffset, Console.BufferHeight / 2 + heightOffset);
    }
}