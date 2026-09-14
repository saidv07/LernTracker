using LernTracker.Modelle;

namespace LernTracker.Services;

public static class LernService
{
    public static void EingabePruefen(Lerneinheit lerneinheit)
    {
        if (string.IsNullOrWhiteSpace(lerneinheit.Fach))
            throw new ArgumentException("Das lernfach darf nicht leer sein.");

        if (lerneinheit.Endzeit <= lerneinheit.Startzeit)
            throw new ArgumentException("Die Endzeit muss nach der Startzeit liegen.");
    }

    public static Dictionary<string, TimeSpan> LernzeitProFachBerechnen(IEnumerable<Lerneinheit> lerneinheiten)
    {
        return lerneinheiten
            .GroupBy(einheit => einheit.Fach, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                gruppe => gruppe.Key,
                gruppe => TimeSpan.FromMinutes(gruppe.Sum(einheit => einheit.Dauer.TotalMinutes)),
                StringComparer.OrdinalIgnoreCase);
    }
}

