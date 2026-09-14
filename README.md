# LernTracker

Ein Programm zum speichern von Lerneinheiten.

## Was das Programm kann

- lerneinheiten je nach fach, startzeit, endzeit speichern
- gespeicherte Lerneinheiten anzeigen
- Einträge bearbeiten und löschen
- gesamte Lernzeit für jedes fach anzeigen
- fehler (falsche eingaben) erkennen und widergeben
- Daten in einer SQLite-Datenbank speichern

## Verwendet

- C#
- .NET 8
- SQLite
- xUnit für die Tests

## Programm starten

Die Datei `LernTracker.sln` in Visual Studio öffnen und das Projekt starten.

Oder im Terminal:

```bash
dotnet run --project LernTracker
```

Tests starten:

```bash
dotnet test
```