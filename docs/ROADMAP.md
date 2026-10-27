# Roadmapa projektu Mini-ERP

Legenda: `[ ]` do zrobienia · `[~]` w trakcie · `[x]` zrobione

## Rytm pracy
- **Sprint 1 (26.10 – 13.11):** dużo wolnego czasu, ok. 6–8 godzin dziennie. Cel: Etapy 0–2 gotowe,
  Etap 3 rozpoczęty.
- **Po 13.11:** tempo do ustalenia (rewizja planu w dniu 13.11, patrz sekcja „Punkt kontrolny”).
- **Limit całego MVP:** ok. 3–4 miesiące.

### Sugerowany rytm dnia (w Sprincie 1)
| Blok | Czas | Co |
|---|---|---|
| Nauka | 1–2 h | Dokumentacja, ćwiczenia, zrozumienie zagadnienia przed kodowaniem |
| Kodowanie | 3–4 h | Zadanie z roadmapy, samodzielnie |
| Review i porządki | 1 h | Code review z Claude Code, testy, commit, aktualizacja roadmapy |
| Powtórka | 30 min | Co dziś zrozumiałem? Wyjaśnij na głos lub zapisz w dzienniku nauki |

Przerwy są częścią planu. Efektywne 5–6 godzin skupienia daje więcej niż 10 godzin „na wpół”.

---

## Etap 0: Fundamenty i odświeżenie C# (26–28.10)
Szczegóły: `docs/STAGE_0_TASKS.md`
- [ ] Środowisko: .NET SDK, IDE (Visual Studio / Rider / VS Code + C# Dev Kit), Git, Docker
- [ ] Repo na GitHubie, `CLAUDE.md` i `docs/` wrzucone do repozytorium
- [ ] Odświeżenie C#: seria ćwiczeń w aplikacji konsolowej
- [ ] Pierwsze rozwiązanie z trzema projektami (`Erp.Api`, `Erp.Core`, `Erp.Data`) i projektami testowymi
- [ ] Endpoint `GET /health` działa, test go sprawdza
- [ ] Baza PostgreSQL w Dockerze, połączenie z aplikacji (na obu maszynach)

**Gotowe, gdy:** `dotnet build` i `dotnet test` przechodzą, a API odpowiada na `/health`.

## Etap 1: Kontrahenci i Towary (29.10 – 2.11)
- [ ] Encje: `UnitOfMeasure`, `VatRate`, `Contractor`, `Product`
- [ ] `DbContext`, konfiguracje encji (precyzja decimali, indeksy, unikalność)
- [ ] Pierwsza migracja i aktualizacja bazy
- [ ] Seed danych słownikowych (jednostki, stawki VAT)
- [ ] CRUD kontrahentów (API) z walidacją (w tym NIP z sumą kontrolną)
- [ ] CRUD towarów (API) z walidacją (unikalny SKU, poprawne ceny)
- [ ] Soft delete / dezaktywacja zamiast usuwania
- [ ] Paginacja i filtrowanie list
- [ ] Testy jednostkowe: walidacja NIP, reguły towaru
- [ ] Testy integracyjne: tworzenie i pobieranie kontrahenta
- [ ] Seed danych demo (sklep wędkarski): ok. 30 towarów, 10 kontrahentów

**Gotowe, gdy:** w Swaggerze można dodać, edytować, wyszukać i dezaktywować kontrahenta oraz towar.

## Etap 2: Magazyn (3 – 8.11)
- [ ] Encje: `Warehouse`, `StockMovement`, `WarehouseDocument`, `WarehouseDocumentLine`
- [ ] Dokument PZ (przyjęcie): szkic → zatwierdzenie → ruchy magazynowe
- [ ] Dokument WZ (wydanie) z kontrolą dostępności
- [ ] Zatwierdzenie dokumentu w **jednej transakcji**
- [ ] Zapytanie o aktualny stan (towar × magazyn) i historię ruchów
- [ ] Anulowanie dokumentu przez dokument korygujący (bez edycji historii)
- [ ] Numeracja dokumentów (`NumberSequence`), numery bez luk
- [ ] Testy reguł: brak towaru, zero w ilości, zatwierdzony dokument niezmienny
- [ ] Test współbieżności: dwa równoległe wydania ostatniej sztuki
- [ ] Ćwiczenie SQL: te same raporty napisane ręcznie w SQL i w LINQ

**Gotowe, gdy:** po PZ stan rośnie, po WZ maleje, a wydanie ponad stan jest odrzucone z czytelnym błędem.

## Etap 3: Sprzedaż (9.11 – po 13.11)
- [ ] Encje: `SalesOrder`, `SalesOrderLine`, `StockReservation`
- [ ] Zamówienie: szkic → potwierdzone (rezerwacja) → zrealizowane (WZ) / anulowane
- [ ] Kopiowanie ceny i VAT do linii zamówienia
- [ ] Wyliczanie wartości zamówienia (netto, VAT, brutto) w `Erp.Core`
- [ ] Dostępność = stan − rezerwacje
- [ ] Testy reguł sprzedaży i rezerwacji

## Etap 4: Faktury
- [ ] Encje: `Invoice`, `InvoiceLine`
- [ ] Wystawienie faktury z zamówienia, numeracja (typ + rok)
- [ ] Wyliczenia VAT i zaokrągleń według jednej, jawnej reguły (udokumentowanej)
- [ ] Niezmienność wystawionej faktury
- [ ] Wydruk PDF (biblioteka do wyboru, np. QuestPDF, po konsultacji)
- [ ] Testy: wyliczenia VAT dla różnych stawek i zaokrągleń

## Etap 5: Użytkownicy, role i audyt
- [ ] ASP.NET Core Identity, logowanie, tokeny JWT
- [ ] Role: Administrator, Magazynier, Handlowiec, i ograniczenie endpointów
- [ ] `AuditLog` wypełniany automatycznie (nadpisanie `SaveChanges` lub interceptor)
- [ ] Obsługa błędów: spójny format odpowiedzi (ProblemDetails)
- [ ] Logowanie (Serilog lub wbudowane `ILogger`)
- [ ] Testy autoryzacji

## Etap 6: Interfejs użytkownika (Blazor)
- [ ] Decyzja: Blazor Server vs WebAssembly (omów z Claude Code)
- [ ] Logowanie i nawigacja
- [ ] Ekrany: kontrahenci, towary, stany magazynowe, dokumenty, zamówienia
- [ ] Prosty dashboard: stany poniżej minimum, sprzedaż z ostatnich 30 dni

## Etap 7: Jakość i wdrożenie
- [ ] `docker-compose.yml`: aplikacja + baza, uruchomienie jedną komendą
- [ ] GitHub Actions: build + testy na każdym pushu
- [ ] README: opis, zrzuty ekranu, diagram architektury, instrukcja uruchomienia
- [ ] `docs/DECISIONS.md` z najważniejszymi decyzjami i uzasadnieniami
- [ ] Wdrożenie wersji demo online (np. darmowy lub tani hosting)
- [ ] Przegląd bezpieczeństwa: sekrety poza repo, walidacja, CORS

## Etap 8: Stretch (po MVP)
- [ ] Warianty produktów (kolor, rozmiar)
- [ ] Partie i terminy ważności
- [ ] Import towarów z Excela
- [ ] Obsługa konfliktów współbieżności w UI
- [ ] Raporty z wykresami
- [ ] Wsparcie SQL Server jako drugiego dostawcy (testy integracyjne w CI na Linuksie x64 lub lokalnie na Windows), żeby znać T-SQL i różnice między dialektami

---

## Punkt kontrolny: 13.11
W dniu zakończenia wolnego czasu przejrzyj razem z Claude Code:
1. Które etapy faktycznie są zrobione, a które tylko „prawie”?
2. Ile godzin tygodniowo możesz poświęcać po 13.11?
3. Czy tniemy zakres Etapu 3–5, żeby zmieścić się w 3–4 miesiącach?
4. Co sprawiało najwięcej trudności i czego trzeba się jeszcze nauczyć?

## Dziennik nauki (uzupełniaj codziennie, 3 zdania)
| Data | Czego się nauczyłem | Czego nie rozumiem | Plan na jutro |
|---|---|---|---|
| 26.10 | | | |
