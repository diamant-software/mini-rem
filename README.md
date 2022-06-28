# Mini REM

Für die Präsentation an der Uni Paderborn.

## Ausführen
### Voraussetzungen
- Docker-Deamon (z. B. Docker-Desktop)
- Postman

### Starten
Docker-Desktop starten

In einer Console im root-verzeichnis des Repos
- 'docker-compose up --build'

Im Chrome-Browser
- nach 'http://localhost:4200/' navigieren
- Refresh-Button verwenden, um neuen Früchte anzuzeigen

In Postman
- Post Request erstellen mit 'http://localhost:5177/api/fruit'
- Als Body 'from-data' auswählen
- Ein Key-Value Paar hinzufügen
  - Key: document 
  - frucht.jpg hochlanden 
  - -> dazu über dem Key hovern, dann erscheint ein dropdown, dort 'File' auswählen, dann kann man in Value eine Datei hochladen

### Benenden / Neustarten
- 'Ctrl+C' in der Console
- 'docker-compose down'

## Programmieren
### Voraussetzungen
- Angular 13
- dotNet 6

### Debuggen
- Service zum Debuggen in der IDE starten
- Service im docker-compose auskommentieren
- Frontend lässt sich im Chrome-Browser in den Entwicklertools debuggen
