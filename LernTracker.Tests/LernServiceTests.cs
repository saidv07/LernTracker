using LernTracker.Modelle;
using LernTracker.Services;
using Xunit;

namespace LernTracker.Tests;

public class LernServiceTests
{
    [Fact]
    public void FehlerWennEndzeitVorStartzeitLiegt()
    {
        var lerneinheit = new Lerneinheit
        {
            Fach = "C#",
            Startzeit = new DateTime(2026, 9, 2, 18, 0, 0),
            Endzeit = new DateTime(2026, 9, 2, 17, 0, 0)
        };

        Assert.Throws<ArgumentException>(() => LernService.EingabePruefen(lerneinheit));
    }

    [Fact]
    public void LernzeitenVomGleichenFachWerdenZusammengerechnet()
    {
        var lerneinheiten = new List<Lerneinheit>
        {
            BeispielErstellen("C#", 60),
            BeispielErstellen("C#", 30),
            BeispielErstellen("Mathematik", 45)
        };

        var lernzeiten = LernService.LernzeitProFachBerechnen(lerneinheiten);

        Assert.Equal(90, lernzeiten["C#"].TotalMinutes);
        Assert.Equal(45, lernzeiten["Mathematik"].TotalMinutes);
    }

    private static Lerneinheit BeispielErstellen(string fach, int minuten)
    {
        var startzeit = new DateTime(2026, 9, 2, 10, 0, 0);
        return new Lerneinheit
        {
            Fach = fach,
            Startzeit = startzeit,
            Endzeit = startzeit.AddMinutes(minuten)
        };
    }
}

