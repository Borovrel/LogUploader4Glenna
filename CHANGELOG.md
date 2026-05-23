# Changelog

Alle nennenswerten Änderungen an LogUploader4Glenna. Format orientiert sich grob an [Keep a Changelog](https://keepachangelog.com/), Versionierung folgt `Major.Minor.Patch.Build`.

## [1.3.0.0] — 2026-05-23

### Hinzugefügt
- **Echte Erfolgs-Erkennung pro Boss**: Statt zu raten welcher Log der Kill war, wird die dps.report-JSON-Antwort ausgewertet (`encounter.success`). Pro Boss wird die URL des ersten erfolgreichen Kills ausgegeben.
- **Wipe-Markierung**: Bosse ohne erfolgreichen Kill im Zeitraum erscheinen als `WIPE <Bossname> <Anzahl Versuche>` — sichtbar für den Discord-Bot.
- **Default-Zeitraum „seit letztem Reset"**: Ist das Zeitraum-Feld leer, wird automatisch ab letztem Montag 14:00 (MEZ/MESZ, DST-aware) gefiltert. Manuelle Tageseingabe weiterhin möglich.
- **Neue Datei `ReportAusgabe.cs`** mit POCO-Klassen für die dps.report-API-Antwort.
- **`CHANGELOG.md`** (diese Datei).

### Geändert
- **UI komplett aufgeräumt**: Klares dreiteiliges Layout (Konfiguration / Optionen / Ergebnis), Segoe-UI-Schrift, primärer blauer „Logs hochladen"-Button, monospaced Consolas-Schrift im Ergebnisfeld, Tooltips an wichtigen Feldern.
- **Upload-Pipeline modernisiert**: Sechs `BackgroundWorker`-Instanzen durch eine `async/await`-Pipeline mit `SemaphoreSlim(4)` ersetzt. Behebt vermutlich auch die in der README erwähnte „sequentielle Uploads schlagen fehl"-Problematik.
- **Pro Boss wird nur noch maximal ein Log hochgeladen**: Logs werden absteigend nach Datum sortiert und beim ersten Erfolg abgebrochen. Spart Bandbreite und Wartezeit gegenüber „alles uploaden, danach filtern".
- **`CLAUDE.md`** an die neue Architektur angepasst.

### Entfernt
- Tab 2 „SelectBosses" inklusive 30 manueller Boss-Checkboxen und der dazugehörigen Filter-Routinen.
- Versteckte/tote Buttons: „Thread", „Lade Logs hoch mehrere Threads", „Starte Überwachung" (FileSystemWatcher).
- Sechs `BackgroundWorker`-Komponenten, mehrere ungenutzte Thread-Splits, doppelte Datei-Sammel-Routinen.

### Behoben
- **Projekt baut wieder**. Die `.csproj` referenzierte `Settings.cs` (existierte nicht) und `ReportAusgabe.cs` (existierte nicht). Erstere wurde aus dem Projekt entfernt, letztere neu angelegt.
- `.gitignore` ignorierte versehentlich `*.cs` und `*.resx` — diese Regeln entfernt, damit künftige Quellcode-Änderungen ohne `-f` committed werden können.

## [1.2.0.0] — vor 2026-05-23

### Hinzugefügt
- Button „Lade letzten Log hoch" — lädt nur den allerletzten PvE-Log unabhängig von den anderen Optionen hoch. Wird vom Zeitraum und Mindestgröße beeinflusst.

### Geändert
- Problematische bzw. unstabile Buttons (Threading-Varianten, Watch-Directory) auf `Visible = false` gesetzt.

## [1.1.0.0]

### Geändert
- Andere Variante für die Auswahl der Logs (manueller Boss-Filter-Tab).

## [1.0.x] — Initiale Versionen

- Grundlegender Upload-Workflow: Logs aus arcdps-Ordner einsammeln, nach Größe und Erstelldatum filtern, zu dps.report hochladen, im Output-Feld als URL + Versuchszahl ausgeben.
- WvW/Fraktale/Übungsbereich als alternative Inhaltstypen.
- Optionale Vermeidung doppelter Uploads via `Upgeloaded/logs.txt`.
- Update-Check-Funktion gegen das GitHub-Repository.
