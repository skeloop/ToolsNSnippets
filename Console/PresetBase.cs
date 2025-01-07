namespace Tools;

/// <summary>
/// Basisklasse für Presets zum Schreiben in die Konsole.
/// </summary>
public abstract class PresetBase
{
    /// <summary> Die Farbe des Suffixes. </summary>
    public virtual ConsoleColor SuffixColor { get; set; } = ConsoleColor.Gray;
        
    /// <summary> Die Farbe des Präfixes. </summary>
    public virtual ConsoleColor PrefixColor { get; set; } = ConsoleColor.Gray;
        
    /// <summary> Die Farbe des Haupttextes. </summary>
    public virtual ConsoleColor Color { get; set; } = ConsoleColor.Gray;
        
    /// <summary> Die Taste, auf die gewartet wird.
    /// Wenn 'ConsoleKey.None' wird nicht gewartet. </summary>
    public virtual ConsoleKey ReturnKey { get; set; } = ConsoleKey.None;
        
    /// <summary> Wenn 'true', wird die Cursorposition nach dem Schreiben der Nachricht auf die ursprüngliche Position zurückgesetzt. </summary>
    public virtual bool ResetPosition { get; set; } = false;
        
    /// <summary> Wenn 'true' und der Text würde über die horizontale Puffergrenze hinausgehen, wird der Text in die nächste Zeile geschrieben. </summary>
    public virtual bool AllowPush { get; set; } = true;
        
    /// <summary> Die Position im Fenster wo der Text geschrieben wird. </summary>
    public virtual (int x, int y) CursorPosition { get; set; } = (-1,-1);
}