using System.Globalization;

namespace LernTracker.UI;

public static class ConsoleInput
{
    private const string Datumsformat = "dd.MM.yyyy HH:mm";
    public static string PflichtTextLesen(string frage)
    {
        while (true)
        {    Console.Write(frage);
            var eingabe = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(eingabe))
                return eingabe;
            Console.WriteLine("Bitte gib einen Wert ein."); }
    }
    public static string OptionalenTextLesen(string frage)
    {   Console.Write(frage);
        return Console.ReadLine()?.Trim() ?? string.Empty; }

    public static DateTime DatumLesen(string frage)
    {
        while (true)
        {
            Console.Write($"{frage} ({Datumsformat}): ");
            var eingabe = Console.ReadLine();
            if (DateTime.TryParseExact(eingabe, Datumsformat, CultureInfo.GetCultureInfo("de-AT"), DateTimeStyles.None, out var datum))
                return datum;
            Console.WriteLine("Ungültiges Format. Beispiel: 02.09.2026 18:30");
        }
    }
    public static int ZahlLesen(string frage)
    {
        while (true)
        {   Console.Write(frage);
                if (int.TryParse(Console.ReadLine(), out var zahl))
                return zahl;
            Console.WriteLine("Bitte gib eine ganze Zahl ein.");}
    }
}

