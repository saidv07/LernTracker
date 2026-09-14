using LernTracker.Modelle;
using Microsoft.Data.Sqlite;

namespace LernTracker.Daten;

public class LernSpeicher
{
    private readonly string _verbindung;
    public LernSpeicher(string datenbankDatei)
    {
        _verbindung = $"Data Source={datenbankDatei}";
    }

    public void DatenbankErstellen()
    {
        using var verbindung = new SqliteConnection(_verbindung);
        verbindung.Open();

        var befehl = verbindung.CreateCommand();
        befehl.CommandText = """
            CREATE TABLE IF NOT EXISTS Lerneinheiten (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Fach TEXT NOT NULL,
            Startzeit TEXT NOT NULL,
            Endzeit TEXT NOT NULL,
            Notiz TEXT NOT NULL
            );
            """;
        befehl.ExecuteNonQuery();
    }

    public void Hinzufuegen(Lerneinheit lerneinheit)
    {
        using var verbindung = new SqliteConnection(_verbindung);
        verbindung.Open();
        var befehl = verbindung.CreateCommand();
        befehl.CommandText = """
            INSERT INTO Lerneinheiten (Fach, Startzeit, Endzeit, Notiz)
            VALUES ($fach, $startzeit, $endzeit, $notiz);
            """;
        WerteEinsetzen(befehl, lerneinheit);
        befehl.ExecuteNonQuery();
    }

    public List<Lerneinheit> AlleLaden()
    {
        var lerneinheiten = new List<Lerneinheit>();
        using var verbindung = new SqliteConnection(_verbindung);
        verbindung.Open();
        var befehl = verbindung.CreateCommand();
        befehl.CommandText = "SELECT Id, Fach, Startzeit, Endzeit, Notiz FROM Lerneinheiten ORDER BY Startzeit DESC;";

        using var ergebnis = befehl.ExecuteReader();
        while (ergebnis.Read())
        {
            lerneinheiten.Add(new Lerneinheit
            {
                Id = ergebnis.GetInt32(0),
                Fach = ergebnis.GetString(1),
                Startzeit = DateTime.Parse(ergebnis.GetString(2)),
                Endzeit = DateTime.Parse(ergebnis.GetString(3)),
                Notiz = ergebnis.GetString(4)
            });
        }
        return lerneinheiten; }

    public bool Bearbeiten(Lerneinheit lerneinheit)
    {
        using var verbindung = new SqliteConnection(_verbindung);
        verbindung.Open();
        var befehl = verbindung.CreateCommand();
        befehl.CommandText = """
            UPDATE Lerneinheiten
            SET Fach = $fach, Startzeit = $startzeit, Endzeit = $endzeit, Notiz = $notiz
            WHERE Id = $id;
            """;
        WerteEinsetzen(befehl, lerneinheit);
        befehl.Parameters.AddWithValue("$id", lerneinheit.Id);
        return befehl.ExecuteNonQuery() == 1;
    }

    public bool Loeschen(int id)
    {
        using var verbindung = new SqliteConnection(_verbindung);
        verbindung.Open();

        var befehl = verbindung.CreateCommand();
        befehl.CommandText = "DELETE FROM Lerneinheiten WHERE Id = $id;";
        befehl.Parameters.AddWithValue("$id", id);
        return befehl.ExecuteNonQuery() == 1;
    }

    private static void WerteEinsetzen(SqliteCommand befehl, Lerneinheit lerneinheit)
    {
        befehl.Parameters.AddWithValue("$fach", lerneinheit.Fach);
        befehl.Parameters.AddWithValue("$startzeit", lerneinheit.Startzeit.ToString("O"));
        befehl.Parameters.AddWithValue("$endzeit", lerneinheit.Endzeit.ToString("O"));
        befehl.Parameters.AddWithValue("$notiz", lerneinheit.Notiz);
    }
}

