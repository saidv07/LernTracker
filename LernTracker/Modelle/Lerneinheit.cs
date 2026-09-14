namespace LernTracker.Modelle;

public class Lerneinheit
{
    public int Id { get; set; }
    public string Fach { get; set; } = string.Empty;
    public DateTime Startzeit { get; set; }
    public DateTime Endzeit { get; set; }
    public string Notiz { get; set; } = string.Empty;

    public TimeSpan Dauer => Endzeit - Startzeit;
}

