# Kontakt Splitter

## Produktbeschreibung
Dieses Programm ist im Rahmen der Vorlesung Softwarequalität an der DHBW Stuttgart Campus Horb entstanden. Die Software teilt eine Anrede in die einzelnen Kontaktdaten auf und erstellt daraus eine Briefanrede. Die Software schließt an ein bestehendes CRM-System an.

## Design
[Design.md](documentation.md)

## Funktionalitäten
* Ein Sachbearbeiter kann die Anrede einer Person angeben. Diese wird in einzelne Kontaktdaten aufgeteilt.
* Aus den Kontaktdaten wird eine standardisierte Briefanrede generiert.
* Die Kontaktdaten lassen sich von dem Sachbearbeiter bearbeiten, wodurch die Briefanrede individualisiert werden kann.
* Der Sachbearbeiter kann neue Titel hinzufügen, welche bei folgender Bearbeitung erkannt werden.
* Der Sachbearbeiter wird benachrichtigt, wenn Angeben fehlen oder nicht interpretiert werden können.

## Technische Funktionsweise
Hier ist die Oberfläche der Software zu sehen. Die beschriebenen Funktionen sind IMMER erst verfügbar, sobald ein Kontakt aufgeteilt wurde.

![Oberfäche](Documentation/kontakt-splitter.jpg)

1. Hier wird die Anrede eingegeben. Nach Bestätigen durch den "Splitten"-Knopf (2) wird die Anrede aufgeteilt und in den Feldern 3,4,6,7,8 angezeigt.
2. Der "Splitten"-Knopf löst das Aufteilen der Anrede aus (siehe 1.).
3. Hier wird die Anrede der Person angezeigt. Aus der Anrede wird automatisch das Geschlecht ermittelt.
4. Hier wird eine Liste der Titel der Person angezeigt. In die Briefanrede wird lediglich der erste Titel übernommen. Durch Drag´n Drop lässt sich dieser Titel anpassen. Durch anklicken eines Titels wird die Briefanrede anktualisiert. Mit einem Rechtsklick lässt sich ein Titel löschen. Dieser wird zukünftig nicht mehr erkannt.
5. Durch diesen Knopf öffnet sich ein neues Fenster in dem ein neuer Titel hinzugefügt werden kann (siehe extra Fenster).
6. Hier wird der Vorname der Person angezeigt. Bei Änderung wird die Briefanrede automatisch aktualisiert.
7. Hier wird der Nachname der Person angezeigt. Bei Änderung wird die Briefanrede automatisch aktualisiert.
8. Das Geschlecht wird in einer Dropdown-Liste angezeigt. Bei einer Änderung wird die Briefanrede neu generiert. Wenn kein Geschlecht erkannt werden kann, wird als Standardwert "UNKNOWN" gesetzt.
9. Für eine Person kann alternativ eine Funktion gewählt werden. Dadurch wird die Person in der Briefanrede in dieser Funktion angesprochen. Die Funktion wird automatisch anhand des Geschlechtes angepasst.
10. Hier wird die generierte Briefanrede angezeigt.
11. Hier wird die erkannte Sprache angezeigt. Eine Änderung hat noch keine Auswirkung.
12. Dieser Knopf ist an das CRM-System angeschlossen um den Kontakt zu speichern. Noch besitzt dieser keine Funktion.

![Kontakt hinzufügen](Documentation/kontakt-splitter-add.jpg)
1. Hier wird der neue Titel eingegeben.
2. Durch den "Hinzufügen"-Knopf wird der Titel zu der Person hinzugefügt und zukünftig auch automatisch erkannt.
3. Durch "Abbrechen" wird das Fenster ohne Änderung geschlossen.

## Einschränkungen
Adelstitel werden noch nicht in ein eigenes Feld aufgeteilt, sondern dem Nachnamen zugeordnet.

Das Erkennen von mehreren Vornamen ist noch nicht implementiert. Der erste Vorname wird als Vorname gesetzt, der Rest wird als Nachname genutzt.

Die Software erkennt bisher nur deutsche und englische Anreden. Weitere Sprachen können hinzugefügt werden. Dafür muss eine Konfiguration für die neue Sprache hinzugefügt werden und eine neuen Klasse hinzugefügt werden, welche von der abstrakte Klasse "Language" erbt um die Grußformel zu generieren.

## Erklärung

Die Generierung der Briefanrede folgt folgenden [Regeln](http://www.stil.de/uploads/media/Die_wichtigsten_Anreden_klipp_und_klar.pdf):

* Es werden Standardmäßig einige Master-, Diplom-, Doktor- und Professortitel erkannt.
* Bei mehreren akademischen Graden wird lediglich der erste im Namen aufgeführt.