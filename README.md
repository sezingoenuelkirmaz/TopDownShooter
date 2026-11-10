# Top-Down-Shooter – Miniprojekt

Fachprojekt Digital Entertainment Technologies, WiSe 2026/27, TU Dortmund

## Team

- Sezin Gönülkirmaz (GitHub: sezingoenuelkirmaz)
- Zeren Isik (GitHub: zerrennn)
- Ataberk Yigit

## Über das Spiel

2D-Top-Down-Shooter im Herbst-Setting. Die Spielfigur bewegt sich frei, zielt mit der Maus und schießt auf Gegner, die aus allen Richtungen auf sie zulaufen.

- Alle 3 Sekunden erscheint ein Gegner in 7 Einheiten Abstand zum Spieler, zufällig einer von drei Typen: Normal (50 %), Fast (30 %), Tank (20 %, hält 3 Treffer aus)
- Gegner verursachen Schaden, sobald sie nah genug am Spieler sind
- Der Spieler hat 5 Leben, angezeigt als Pixel-Herzen
- Alle 10 Sekunden erscheint ein Health-Pickup an einer zufälligen Position (+1 Leben)
- Jeder besiegte Gegner gibt +1 Score
- Bei 0 Leben erscheint „GAME OVER“, mit R startet die Runde neu

## Technische Angaben

- **Unity-Version:** 6000.3.25f1 (Unity 6.3 LTS)
- **Render Pipeline:** Universal Render Pipeline (2D)
- **Startszene:** `Assets/Scenes/SampleScene.unity`
- **Benötigte Pakete** (werden über `Packages/manifest.json` automatisch installiert):
  - Input System (`com.unity.inputsystem` 1.20.0)
  - Universal RP (`com.unity.render-pipelines.universal` 17.3.0)
  - uGUI inkl. TextMesh Pro (`com.unity.ugui` 2.0.0), die TMP Essential Resources liegen unter `Assets/TextMesh Pro`
  - 2D-Pakete (Sprite, Animation, Tilemap)

## Startanleitung

1. ZIP entpacken
2. Unity Hub → **Add** → **Add project from disk** → entpackten Ordner wählen
3. Mit Unity **6000.3.25f1** öffnen (beim ersten Öffnen werden die Pakete installiert, das kann einige Minuten dauern)
4. Im Project-Fenster `Assets/Scenes/SampleScene` per Doppelklick öffnen
5. **Play** drücken und einmal ins Game-Fenster klicken

## Steuerung

| Taste | Aktion |
|---|---|
| W A S D | Bewegen |
| Maus | Zielen |
| Linksklick oder Leertaste | Schießen |
| R | Neustart nach Game Over |

## Projektstruktur

| Ordner | Inhalt |
|---|---|
| `Assets/Scripts` | Alle C#-Skripte |
| `Assets/Scenes` | `SampleScene` (einzige Szene) |
| `Assets/Sprites` | Herz-Sprites für die Lebensanzeige |
| `Assets/Character` | Spritesheet der Spielfigur |
| `Assets/Backgrounds` | Hintergrundbild |
| `Assets/*.prefab` | Prefabs: `Bullet`, `Enemy`, `FastEnemy`, `TankEnemy` (`HealthPickup.prefab` liegt in `Assets/Scripts`) |

## Bekannte Probleme

- Nach Game Over kann sich die Spielfigur weiterhin bewegen und schießen, da `PlayerMovement` und `PlayerShoot` den Zustand `IsDead` nicht abfragen
- Das Spielfeld ist nicht begrenzt: Spieler und Gegner können den Bereich des Hintergrundbilds verlassen
- Das Hintergrundbild füllt je nach Bildschirmgröße nicht den ganzen Bildschirm aus

## Offene Funktionen / mögliche Erweiterungen

- Spielfeldbegrenzung und an die Bildschirmgröße angepasster Hintergrund
- Bewegung und Schießen nach Game Over deaktivieren
- Dauerfeuer mit Cooldown beim Gedrückthalten
- Steigender Schwierigkeitsgrad
- Startmenü und Pause

## Beitragsübersicht

Die Zuständigkeiten haben wir nach Bereichen aufgeteilt (Spieler, Gegner, Kugel/Score/Pickups). Wo jemand ein Skript übernommen hat, das ursprünglich eine andere Person umgesetzt hat, ist beides angegeben.

| Skript | Umgesetzt von | Zuständig |
|---|---|---|
| `PlayerMovement` | Sezin Gönülkirmaz | Sezin Gönülkirmaz |
| `PlayerAim` | Sezin Gönülkirmaz | Sezin Gönülkirmaz |
| `PlayerShoot` | Sezin Gönülkirmaz | Sezin Gönülkirmaz |
| `PlayerHealth` | Sezin Gönülkirmaz | Sezin Gönülkirmaz |
| `PlayerSpriteAnimator` | Sezin Gönülkirmaz | Sezin Gönülkirmaz |
| `EnemyFollow`, `EnemyHealth`, `EnemyDeath`, `EnemySpawner` | Zeren Isik | Zeren Isik |
| `BulletHit` | Ataberk Yigit | Ataberk Yigit |
| `ScoreManager` | Ataberk Yigit| Ataberk Yigit |
| `HealthPickup`, `HealthSpawner` | Ataberk Yigit | Ataberk Yigit |

Weitere Projektbestandteile:

Zeren Isik: Gegner-Prefabs und Konfiguration der Gegnertypen
Sezin Gönülkirmaz: Player-Objekt mit Aim/Gun/FirePoint, Spielfigur und Laufanimation sowie spielerbezogene UI (Herz-Anzeige und Game-Over-Panel)
Ataberk Yigit: Projekt- und GitHub-Setup, Kugel- und Health-Pickup-Prefabs, Score-Anzeige, übriger Aufbau der Canvas-UI, Sounds und Hintergrund

## Quellen

| Asset | Quelle | Lizenz |
|---|---|---|
| `voicebosch-laser-gun-174976.mp3` (Schuss) | Pixabay, „Laser Gun“ von voicebosch (ID 174976) | Pixabay Content License |
| `universfield-game-over-deep-male-voice-clip-352695.mp3` (Treffer) | Pixabay, „Game Over Deep Male Voice Clip“ von Universfield (ID 352695) | Pixabay Content License |
| Schriftart LiberationSans SDF | mit TextMesh Pro ausgeliefert | SIL Open Font License |
| `Blue-Hour Autumn Forest Arena.png` | KI-generiert mit ChatGPT | – |
| `Hilal.png` (Spielfigur) | KI-generiert mit ChatGPT | – |
| `HeartFull.png`, `HeartEmpty.png` | KI-generiert mit Claude | – |

Übernommener Tutorial- oder Fremdcode: keiner.

## Einsatz von KI

| Werkzeug | Betroffene Dateien / Funktionen | Art der Unterstützung | Eigene Prüfung und Anpassung |
|---|---|---|---|
| Claude (Anthropic) | `PlayerMovement.cs`, `PlayerAim.cs` | Code-Vorschläge und Erklärung der Unity-Konzepte (Komponenten, Rigidbody2D, Update/FixedUpdate, `ScreenToWorldPoint`, `Atan2`) | Schrittweise in Unity eingebaut, im Play-Modus getestet, Code Zeile für Zeile nachvollzogen |
| Claude (Anthropic) | `PlayerHealth.cs`, `ScoreManager.cs` (UI-Teil), Canvas in `SampleScene` | Umstellung der Anzeige von `OnGUI` auf Canvas mit TextMesh Pro und Herz-Images, Anleitung zum Aufbau der UI im Editor | UI selbst im Editor aufgebaut und verknüpft, Leben, Score, Game Over und Neustart im Spiel getestet |
| Claude (Anthropic) | `HeartFull.png`, `HeartEmpty.png` | Generierung der 9×8-Pixel-Herzen | Import-Einstellungen angepasst (Point-Filter, keine Kompression) |
| Claude (Anthropic) | Git-Workflow | Hilfe bei der Einrichtung von GitHub und beim Lösen eines Merge-Konflikts in `SampleScene.unity` | Konflikt geprüft, Szene danach in Unity getestet |
| Claude, ChatGPT | Skripte für Schießen, Kugel, Gegner, Spawner, Score, Leben und Health-Pickups, `PlayerSpriteAnimator` | Code-Vorschläge | Im Spiel getestet, Werte im Inspector angepasst |
| ChatGPT | `Blue-Hour Autumn Forest Arena.png`, `Hilal.png` | Bildgenerierung | Als Sprites importiert, Spritesheet der Spielfigur für die Laufanimation eingerichtet |
| Claude (Anthropic) | Gesamtes Projekt | Erklärung des Codes zur Vorbereitung auf die Präsentation | – |
