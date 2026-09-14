using LernTracker.Daten;
using LernTracker.Modelle;
using LernTracker.Services;
using LernTracker.UI;

var lernSpeicher = new LernSpeicher("lerntracker.db");
lernSpeicher.DatenbankErstellen();
var programmLaeuft = true;
while (programmLaeuft)

{
    MenueAnzeigen();
    var auswahl = Console.ReadLine();
    try
    {
        switch (auswahl)
        {
            case "1": LerneinheitHinzufuegen(lernSpeicher); break;
            case "2": LerneinheitenAnzeigen(lernSpeicher.AlleLaden()); break;
            case "3": LerneinheitBearbeiten(lernSpeicher); break;
            case "4": LerneinheitLoeschen(lernSpeicher); break;
            case "5": AuswertungAnzeigen(lernSpeicher.AlleLaden()); break;
            case "0": programmLaeuft = false; break;
            default: Console.WriteLine("Bitte wähle einen gültigen Menüpunkt."); break;
        }
    }
    catch (Exception fehler)
    {
        Console.WriteLine($"Fehler: {fehler.Message}");
    }

    if (programmLaeuft)
    {
        Console.WriteLine("\nDrücke Enter, um fortzufahren.");
        Console.ReadLine();
        Console.Clear();
    }
}

static void MenueAnzeigen()
{
    Console.WriteLine("=== LERNTRACKER ===");
    Console.WriteLine("1 - Lernzeit hinzufügen");
    Console.WriteLine("2 - Alle Lernzeiten anzeigen");
    Console.WriteLine("3 - Lernzeit bearbeiten");
    Console.WriteLine("4 - Lernzeit löschen");
    Console.WriteLine("5 - Lernzeit pro Fach anzeigen");
    Console.WriteLine("0 - PRogramm beenden");
    Console.Write("Auswahl: ");
}

static void LerneinheitHinzufuegen(LernSpeicher lernSpeicher)
{   var lerneinheit = LerneinheitEinlesen();
    LernService.EingabePruefen(lerneinheit);
    lernSpeicher.Hinzufuegen(lerneinheit);
    Console.WriteLine("die lernzeit wurde gespeichert."); }

static void LerneinheitenAnzeigen(List<Lerneinheit> lerneinheiten)
{
    if (lerneinheiten.Count == 0)
    {   Console.WriteLine("es wurden keine lernzeiten gespeichert.");
        return; }

    Console.WriteLine("\nID | Fach | Start | Ende | Dauer | Notiz");
    Console.WriteLine(new string('-', 90));

    foreach (var lerneinheit in lerneinheiten)
    { Console.WriteLine($"{lerneinheit.Id} | {lerneinheit.Fach} | {lerneinheit.Startzeit:dd.MM.yyyy HH:mm} | " +
      $"{lerneinheit.Endzeit:dd.MM.yyyy HH:mm} | {DauerAnzeigen(lerneinheit.Dauer)} | {lerneinheit.Notiz}");
    }
}

static void LerneinheitBearbeiten(LernSpeicher lernSpeicher)
{   LerneinheitenAnzeigen(lernSpeicher.AlleLaden());
    var id = ConsoleInput.ZahlLesen("ID des Eintrags: ");
    var lerneinheit = LerneinheitEinlesen();
    lerneinheit.Id = id;
    LernService.EingabePruefen(lerneinheit);

    Console.WriteLine(lernSpeicher.Bearbeiten(lerneinheit)
        ? "Die Lernzeit wurde geändert."
        : "Es wurde kein Eintrag mit dieser ID gefunden."); }

static void LerneinheitLoeschen(LernSpeicher lernSpeicher)
{
 LerneinheitenAnzeigen(lernSpeicher.AlleLaden());
    var id = ConsoleInput.ZahlLesen("ID des Eintrags, den du löschen willst: ");
    Console.WriteLine(lernSpeicher.Loeschen(id)
        ? "Der Eintrag wurde gelöscht."
        : "Es wurde kein Eintrag mit dieser ID gefunden."); }

static void AuswertungAnzeigen(List<Lerneinheit> lerneinheiten)
{
    var lernzeiten = LernService.LernzeitProFachBerechnen(lerneinheiten);
    if (lernzeiten.Count == 0)
    {   Console.WriteLine("Für die Auswertung sind noch keine Daten vorhanden.");
        return;
}

    Console.WriteLine("\n=== LERNZEIT PRO FACH ===");
    foreach (var lernzeit in lernzeiten.OrderByDescending(eintrag => eintrag.Value))
    Console.WriteLine($"{lernzeit.Key}: {DauerAnzeigen(lernzeit.Value)}");
}

static Lerneinheit LerneinheitEinlesen()
{
    return new Lerneinheit
    {
        Fach = ConsoleInput.PflichtTextLesen("LErnfach: "),
        Startzeit = ConsoleInput.DatumLesen("Startzeit"),
        Endzeit = ConsoleInput.DatumLesen("Endzeit"),
        Notiz = ConsoleInput.OptionalenTextLesen("Notiz (optional); ")
    };
}

static string DauerAnzeigen(TimeSpan dauer)
{   
    var stunden = (int)dauer.TotalHours;
    return $"{stunden} h {dauer.Minutes} min";
}

