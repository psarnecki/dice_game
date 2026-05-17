# DiceGame

Implementacja gry „Kości" dla 2–4 graczy jako zadanie rekrutacyjne.

## Stack

- .NET 10 + C# 13
- Blazor WebAssembly
- Brak zewnętrznych zależności NuGet poza standardowym SDK
## Jak uruchomić

**Wymagania:** .NET 10 SDK + dowolna nowoczesna przeglądarka

```bash
git clone https://github.com/psarnecki/dice_game.git
cd dice_game
dotnet run --project DiceGame.Client
```

Następnie otwórz w przeglądarce adres wyświetlony w konsoli (domyślnie `http://localhost:5000`).  
Pierwsze załadowanie może chwilę potrwać — przeglądarka pobiera środowisko .NET WebAssembly.

## Struktura projektu

```
DiceGame.sln
├── DiceGame.Core/      # logika domeny — zero zależności od UI
└── DiceGame.Client/    # Blazor WASM — wyłącznie warstwa prezentacji
```

`Core` nie wie nic o Blazorze ani przeglądarce. Gdyby ktoś chciał przenieść grę do konsoli albo wystawić ją przez API — domena zostaje bez zmian.

## Decyzje projektowe

**Strategy pattern dla punktacji**  
Każda z 13 kategorii to osobna klasa implementująca `IScoringStrategy`. `ScoringService` dostaje je wszystkie przez DI i dispatchuje po `Category` — bez żadnych switch-ów. Dodanie nowej kategorii to jedna klasa i jedna linia w `Program.cs`.

Żeby nie powtarzać kodu, wydzieliłem 3 abstrakcyjne bazy:
- `FaceValueStrategy` — logika wspólna dla Jedynek–Szóstek
- `OfAKindStrategy` — wspólna dla 3 i 4 jednakowych
- `StraightStrategy` — wspólna dla małego i dużego strita
  **Result objects zamiast wyjątków**  
  `GameEngine` zwraca `RollResult`, `ScoreResult` itd. — rekordy z flagą `Success`. UI robi `if (result.Success)` zamiast łapać wyjątki na normalnym przepływie gry. Wyjątki zostają jako safety net wewnątrz domeny, na wypadek buga w silniku.

**Niemutowalne snapshoty stanu**  
`Game` (class) to mutowalny agregat zmieniany przez `GameEngine`. Na zewnątrz eksponuję tylko `GameState` (record) — niemutowalną migawkę. Komponenty Blazor zawsze pracują na snapshocie, nie mają referencji do agregatu.

**Hot-seat multiplayer**  
Gracze siedzą przy jednym ekranie i biorą tury po kolei. Specyfikacja wymaga 2–4 graczy — ten model to w pełni spełnia i nie wymaga żadnej infrastruktury sieciowej. Gdyby trzeba było dodać tryb online, architektura jest gotowa: `GameEngine` jest agnostyczny wobec transportu, a `GameState` to gotowy DTO. Naturalnym krokiem byłby projekt `DiceGame.Server` z hubem SignalR.

**Etykiety UI po polsku, kod po angielsku**  
Nazwy klas i metod angielskie (standard C#), wyświetlane etykiety kategorii po polsku przez `ScoreCategoryLabels`. Wymiana języka UI nie dotyka domeny.

**Brak magic numbers — `GameConstants`**  
Wszystkie reguły liczbowe (5 kości, 3 rzuty, próg bonusu, wartości punktowe Full/Strit/Król itd.) zebrane w jednym miejscu. Zmiana dowolnej reguły to jedna linia, propaguje się wszędzie — w silniku i w podpowiedziach UI.

**`ScoringService` bez interfejsu**  
`IGameEngine` i `IDiceRoller` mają interfejsy bo mają sens do mockowania i podmiany. `ScoringService` nie — jedna implementacja, delegacja do strategii. Dodanie `IScoringService` byłoby YAGNI.

**Fail-fast przy starcie**  
`ScoringService` sprawdza w konstruktorze czy wstrzyknięto wszystkie 13 strategii. Jeśli ktoś zapomni zarejestrować jedną, aplikacja nie wstaje zamiast paść z `KeyNotFoundException` w połowie gry.

## Użyte wzorce

| Wzorzec | Gdzie |
|---|---|
| Strategy | `IScoringStrategy` + 13 implementacji w `Services/Strategies/` |
| Facade | `GameStateService` jako jeden punkt wejścia dla komponentów Blazor |
| Observer | `event Action? StateChanged` — komponenty subskrybują, silnik emituje |
| Aggregate Root | `Game` kontroluje dostęp do `Player` i `ScoreCard`, mutacja tylko przez metody agregatu |
| Snapshot | `GameState` jako niemutowalna kopia stanu agregatu eksponowana do UI |
| Template Method | Abstrakcyjne bazy strategii trzymają algorytm, subklasy tylko parametryzują |

## Założenia funkcjonalne

- **Pierwszy rzut automatyczny** — po starcie gry i po każdej zmianie gracza silnik sam rzuca wszystkimi pięcioma. Zgodnie ze specyfikacją: *„W pierwszej próbie zawsze rzuca wszystkimi"*
- **Scoring dostępny po każdym rzucie** — gracz nie musi wykorzystywać wszystkich 3 prób. Po pierwszym rzucie może od razu wybrać kategorię
- **Po 3 rzutach gracz musi wybrać kategorię** — przycisk rzutu blokuje się, `RollDice()` w tej fazie zwraca `Fail`
- **Walidacja nazw graczy** — niepuste, unikalne case-insensitive. Dwóch „Adam" / „adam" zostanie odrzuconych — na scoreboard byłoby nieczytelnie
- **Remis** — przy równym wyniku wygrywa gracz który zajął wyższe miejsce w kolejności zgłoszenia. Specyfikacja nie precyzuje tie-breakera
- **Highlight dostępnych kategorii** — wiersze dające > 0 punktów dla aktualnych kości są podświetlone. Czysto UX, nie zmienia mechaniki
- **Etykiety kategorii** — `Yahtzee → Król` zamiast potocznego „Generał", `ThreeOfAKind → 3 Jednakowe` zamiast „Trójka" (mylącej z górną sekcją). Etykiety opisują regułę, nie slang
## Uwaga o progu bonusu

W mailu z zadaniem próg bonusu za górną sekcję podano jako **62 punkty**, natomiast oficjalny PDF i Wikipedia podają **63 punkty**.

Implementacja trzyma się PDF-u (63 pkt) — ta wartość wynika wprost z matematyki: 3 kości każdej wartości to 3+6+9+12+15+18 = 63. Jeśli wymagana jest wartość 62, wystarczy zmienić jedną stałą:

```csharp
// DiceGame.Core/GameConstants.cs
public const int UpperBonusThreshold = 62;
```
 