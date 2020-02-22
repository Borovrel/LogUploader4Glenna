# LogUploader4Glenna
Ein kleines Programm um unsere Wöchentlichen PvE Raids in Guild Wars 2 auf dps.report hochzuladen und anschließend diese an einen Discord Bot zu verschicken (Glenna).

Die Logs werden wie folgt ausgewählt:
- PvE oder WvW (abhängig von ausgewählter Einstellung)
- Alle Dateien, welche **größer als 100 KB** sind. (PvE)
- Alle Dateien, deren Erstelldatum zwischen "Heute" und "Heute"-Textfeld liegt. Bsp: Heute: 16.02.2020; Textfeld = 1 => Alle Logs vom 15.02.2020 bis 16.02.2020 werden berücksichtigt.

**Dabei wird immer nur der letzte Log hochgeladen!** Dies liegt daran, dass Glenna nur den letzten Versuch möchte und die Anzahl der benötigten Versuche. Alle Logs die im oben genannten ausgewählt wurden und im entsprechenden Ordner liegen (Unterordner zählen auch) (z.B. Talwächter) zählen dann jeweils für den jeweiligen Boss als Versuch.

Es kann aktuell zu folgenden Fehlern kommen:
- Beim Versuch mehrmals Logs hintereinander hochladen zu wollen!
- Der Log liegt in zu vielen Unterordner => Der falsche Boss wird erkannt => Logs werden falsch hochgeladen ebenso die Anzahl der benötigten Versuche

Anmerkungen:
- Weniger der oben genannten Fehler
