# Etap 0: Fundamenty i odświeżenie C# (26–28.10)

Cel: mieć gotowe środowisko, puste rozwiązanie z działającym API i pewność, że znasz podstawy C#
na tyle, by nie blokować się na składni w kolejnych etapach.

## Zasada pracy w tym etapie

Piszesz sam. Gdy zapomnisz składni, pytasz Claude Code o **krótki przykład**, a potem sprawdzasz temat
w dokumentacji (learn.microsoft.com). Cel to nauczyć się szukać, a nie zapamiętać wszystko.

---

## Dzień 1: Środowisko i repozytorium

### Zadanie 0.1: Instalacja narzędzi

- [x] .NET SDK (najnowsza LTS). Sprawdź: `dotnet --version`
      **Powtórz całość na obu maszynach (macOS i Windows):**
- [x] IDE: JetBrains Rider (sprawdź aktualne warunki licencji: darmowa opcja do użytku niekomercyjnego
      oraz licencje studenckie). Włącz Settings Sync
- [x] Git (`git --version`), konto GitHub
- [x] Docker Desktop (`docker --version`)
- [x] Narzędzie EF: `dotnet tool install --global dotnet-ef`
- [x] Ta sama wersja SDK na obu maszynach (porównaj `dotnet --version`)
- [x] Claude Code zainstalowany na obu maszynach

> Decyzja o bazie: **PostgreSQL** na obu maszynach. Oficjalny obraz SQL Servera nie działa natywnie
> na Apple Silicon (M4) i wymaga emulacji x86, która bywa niestabilna. PostgreSQL działa natywnie
> na macOS (ARM) i Windows, a SQL, którego się uczysz, jest w zdecydowanej większości przenośny.

### Zadanie 0.2: Repozytorium

- [x] Utwórz repo `mini-erp` na GitHubie i sklonuj lokalnie
- [x] Skopiuj `CLAUDE.md` i folder `docs/` do repo
- [x] Dodaj `.gitignore` dla .NET (`dotnet new gitignore`)
- [x] Dodaj `global.json` (`dotnet new globaljson`), przypina wersję SDK na obu maszynach
- [x] Dodaj `.gitattributes` z linią `* text=auto` oraz `.editorconfig` (`dotnet new editorconfig`)
- [x] Dodaj `.env.example` (bez prawdziwych haseł) i wpis `.env` w `.gitignore`
- [x] Pierwszy commit: `docs: add project vision, roadmap and CLAUDE.md`
- [x] **Test dwóch maszyn:** sklonuj repo na drugiej maszynie, wykonaj `dotnet build`. Jeśli działa
      bez dodatkowych kroków, repo jest poprawnie przenośne

### Zadanie 0.3: Uruchom Claude Code w repo

- [x] Uruchom Claude Code w katalogu repo
- [x] Poproś o przeczytanie `CLAUDE.md` i podsumowanie, jak rozumie swoją rolę
- [x] Zapisz w dzienniku nauki, jak wygląda Wasza współpraca (czy trzyma się zasad mentora?)

---

## Dzień 1–2: Odświeżenie C# (aplikacja konsolowa)

Utwórz osobny projekt do ćwiczeń: `dotnet new console -n CSharpPlayground`.
Do każdego bloku napisz małe programy, **bez kopiowania gotowców**.

### Blok A: Podstawy języka

- [x] Typy, zmienne, `var`, `string`, `decimal` vs `double` (policz `0.1 + 0.2` w obu)
- [x] Instrukcje warunkowe, pętle, `switch` (także wyrażenie `switch`)
- [x] Metody: parametry, zwracanie wartości, parametry opcjonalne, `out`
- [x] **Ćwiczenie:** kalkulator VAT: dla ceny netto i stawki policz brutto, wypisz kwoty z dwoma miejscami

### Blok B: Klasy i obiektowość

- [x] Klasa, właściwości, konstruktor, modyfikatory dostępu
- [x] Enkapsulacja: prywatne pole + metoda zmieniająca stan z walidacją
- [ ] Dziedziczenie, `abstract`, `virtual`/`override`
- [ ] Interfejsy i polimorfizm
- [ ] `record` vs `class`, `enum`
- [x] **Ćwiczenie:** klasa `Product` (nazwa, cena, ilość) z metodami `AddStock(int)` i `RemoveStock(int)`, która rzuca wyjątek, gdy brakuje towaru

### Blok C: Kolekcje i LINQ

- [ ] `List<T>`, `Dictionary<K,V>`, `HashSet<T>`
- [ ] LINQ: `Where`, `Select`, `OrderBy`, `GroupBy`, `Sum`, `Any`, `FirstOrDefault`
- [ ] **Ćwiczenie:** lista 20 towarów w pamięci. Wypisz: najdroższe 3, sumę wartości magazynu, towary pogrupowane według kategorii

### Blok D: Nowoczesny C#

- [ ] Nullable reference types (`string?`), operatory `?.`, `??`
- [ ] Wyjątki: `try/catch/finally`, własny typ wyjątku
- [ ] `async` / `await` i `Task` (symulacja: `await Task.Delay(1000)`)
- [ ] Dependency Injection: ręczny przykład (klasa przyjmuje interfejs w konstruktorze)

### Blok E: Mini-zadanie spajające

- [ ] **Magazyn w pamięci:** klasa `Warehouse` z listą ruchów (`+10`, `-3`...), metodą `GetStock()` jako sumą ruchów oraz odrzucaniem ruchów prowadzących do ujemnego stanu. To mikro-wersja Etapu 2!
- [ ] Zapisz 3 testy jednostkowe (xUnit) dla tej klasy

**Punkt kontrolny:** wyjaśnij Claude Code własnymi słowami różnicę między klasą a interfejsem
i dlaczego używamy `decimal` do pieniędzy. Jeśli utkniesz, wróć do bloku.

---

## Dzień 2–3: Szkielet rozwiązania

### Zadanie 0.4: Rozwiązanie i projekty

```
dotnet new sln -n MiniErp
dotnet new webapi -n Erp.Api -o src/Erp.Api
dotnet new classlib -n Erp.Core -o src/Erp.Core
dotnet new classlib -n Erp.Data -o src/Erp.Data
dotnet new xunit -n Erp.Core.Tests -o tests/Erp.Core.Tests
dotnet new xunit -n Erp.Api.Tests -o tests/Erp.Api.Tests
```

- [ ] Dodaj wszystkie projekty do solution (`dotnet sln add ...`)
- [ ] Ustaw referencje: `Api → Core, Data`; `Data → Core`; testy → odpowiednie projekty
- [ ] Włącz `<Nullable>enable</Nullable>` i `<TreatWarningsAsErrors>` (omów z Claude Code, co to daje)
- [ ] Zapytaj Claude Code: **dlaczego `Core` nie powinno zależeć od `Data`?** Zapisz odpowiedź w dzienniku

### Zadanie 0.5: Pierwszy endpoint

- [ ] `GET /health` zwraca `{ "status": "ok" }`
- [ ] Uruchom API i otwórz je w przeglądarce/Swaggerze (sprawdź w dokumentacji, jak włączyć OpenAPI w aktualnym szablonie)
- [ ] Test integracyjny w `Erp.Api.Tests`, który woła `/health` (sprawdź `WebApplicationFactory`)

### Zadanie 0.6: Baza danych w Dockerze

- [ ] Plik `docker-compose.yml` z PostgreSQL (obraz `postgres`, wolumen na dane, hasło w `.env`, który jest w `.gitignore`)
- [ ] `docker compose up -d`, sprawdź, że kontener działa
- [ ] Połączenie przez narzędzie do baz (Rider ma wbudowane okno Database, możesz też użyć DBeaver)
- [ ] Dodaj do `Erp.Data` pakiet `Npgsql.EntityFrameworkCore.PostgreSQL` i pusty `ErpDbContext` i skonfiguruj connection string przez `appsettings` / user secrets
- [ ] Utwórz pustą migrację i uruchom `dotnet ef database update`

### Zadanie 0.7: Zamknięcie etapu

- [ ] Wszystko zacommitowane małymi commitami
- [ ] `dotnet build` i `dotnet test` przechodzą
- [ ] Zaktualizowane pola `[x]` w `docs/ROADMAP.md` i wpis w dzienniku nauki
- [ ] Poproś Claude Code o review struktury rozwiązania
- [ ] Zmień w `CLAUDE.md` sekcję „Aktualny etap” na **Etap 1**

---

## Przydatne materiały (dokumentacja oficjalna)

- Dokumentacja C#: learn.microsoft.com/dotnet/csharp
- Tour po C#: sekcja „Tour of C#” w dokumentacji
- ASP.NET Core: learn.microsoft.com/aspnet/core
- EF Core: learn.microsoft.com/ef/core
- SQL: ćwicz na lokalnej bazie. Poproś Claude Code o zestaw zadań z JOIN-ami i GROUP BY na danych demo

## Jeśli utkniesz

Opisz Claude Code: (1) co chciałeś osiągnąć, (2) co zrobiłeś, (3) co dokładnie się stało
(komunikat błędu w całości). Dobrze sformułowane pytanie to umiejętność, którą też ćwiczysz.
