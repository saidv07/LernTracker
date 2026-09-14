# LernTracker

Ein Programm zum speichern von Lerneinheiten.

## Was das Programm kann

- Lerneinheiten je nach Fach, Startzeit, Endzeit speichern
- Gespeicherte Lerneinheiten anzeigen
- Einträge bearbeiten und löschen
- Gesamte Lernzeit für jedes Fach anzeigen
- Fehler (falsche Eingaben) erkennen und wiedergeben
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
