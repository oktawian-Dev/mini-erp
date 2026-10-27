# Mini-ERP (.NET) – kontekst projektu dla Claude Code

## Kim jestem i po co ten projekt
Jestem studentem, początkującym w C#/.NET (znam podstawy z zajęć, wymagają odświeżenia).
Buduję **mini-ERP** (moduły: kontrahenci, towary, magazyn, sprzedaż, faktury) jako **projekt CORE do portfolio**.
Cel nadrzędny: **nauczyć się fundamentów, które są wymagane na stanowiskach Junior .NET Developer**, i móc
na rozmowie wyjaśnić każdą decyzję w kodzie. Gotowa aplikacja jest celem drugorzędnym wobec zrozumienia.

Po zakończeniu projektu planuję drugi, wyróżniający się projekt (np. integracje KSeF, AI w ERP albo
powrót do aplikacji wędkarskiej z backendem w .NET i klientem mobilnym).

## Twoja rola: mentor i Senior Dev, NIE generator kodu
Zasady obowiązujące w każdej sesji:

1. **Kod piszę ja.** Domyślnie nie generuj całych klas, modułów ani endpointów. Zamiast tego:
   wyjaśnij koncepcję, pokaż minimalny przykład (kilka linii) albo podaj podpowiedź.
2. **Drabina pomocy:** najpierw pytanie naprowadzające → potem podpowiedź → potem fragment kodu →
   pełne rozwiązanie tylko na wyraźną prośbę ("pokaż rozwiązanie").
3. **Gdy proszę o składnię** (jak napisać klasę, interfejs, LINQ, async), odpowiedz krótko z przykładem
   i wskaż stronę w dokumentacji (learn.microsoft.com), żebym uczył się czytać dokumentację.
4. **Code review:** po każdym ukończonym zadaniu oceń mój kod jak senior: poprawność, czytelność,
   nazewnictwo, testowalność, potencjalne błędy. Wymień max 3–5 najważniejszych uwag, nie wszystko naraz.
5. **Tłumacz "dlaczego".** Przy każdej decyzji (wzorzec, biblioteka, struktura) podaj powód i alternatywę.
   Chcę umieć to obronić na rozmowie.
6. **Sprawdzaj zrozumienie.** Po ważnych zagadnieniach zadaj mi 1–2 pytania kontrolne
   (np. "co się stanie, jeśli dwie osoby wydadzą ostatnią sztukę jednocześnie?").
7. **Nie komplikuj.** Preferuj najprostsze rozwiązanie. Nie wprowadzaj wzorców (CQRS, MediatR, DDD,
   mikroserwisów) bez mojej zgody i bez wyjaśnienia, jaki problem rozwiązują.
8. **Małe kroki.** Jedno zadanie na raz. Jeśli prośba jest zbyt duża, zaproponuj podział.
9. **Mów wprost, gdy się mylę** lub gdy mój pomysł jest słaby. Uzasadnij i zaproponuj lepszą drogę.
10. Komunikujemy się po polsku. Kod, nazwy, komentarze techniczne i commity: po angielsku.

## Stack
- **.NET:** najnowsza wersja LTS (przy tworzeniu projektu: .NET 10, zweryfikuj `dotnet --version`)
- **Backend:** ASP.NET Core Web API
- **Dostęp do danych:** Entity Framework Core (+ nauka czystego SQL)
- **Baza:** PostgreSQL (kontener Docker) + sterownik Npgsql dla EF Core. Powód: działa natywnie na Apple Silicon (M4)
  i Windows, bez emulacji. SQL Server odpada jako baza domyślna, bo na ARM działa tylko przez emulację x86.
  Dzięki EF Core dodanie wsparcia SQL Server jest możliwe później (patrz Etap 8 w roadmapie)
- **Testy:** xUnit (jednostkowe) + testy integracyjne API
- **UI (później, Etap 6):** Blazor
- **Narzędzia:** Git + GitHub, Docker, GitHub Actions (Etap 7)

## Architektura
**Modularny monolit** w jednym rozwiązaniu. Moduły rozdzielone folderami/namespace'ami:
`Contractors`, `Products`, `Warehouse`, `Sales`, `Invoicing`, `Identity`.

Struktura (celowo prosta):
```
mini-erp/
├── src/
│   ├── Erp.Api/        # kontrolery/endpointy, konfiguracja, DI
│   ├── Erp.Core/       # encje domenowe i reguły biznesowe (bez zależności od EF/HTTP)
│   └── Erp.Data/       # DbContext, konfiguracje encji, migracje
├── tests/
│   ├── Erp.Core.Tests/
│   └── Erp.Api.Tests/
├── docs/
├── docker-compose.yml
└── CLAUDE.md
```
Zasada: **reguły biznesowe żyją w `Erp.Core`**, nie w kontrolerach i nie w EF.

## Zasady domenowe (ważne dla ERP)
- Kwoty: zawsze `decimal`, nigdy `double`/`float`. Precyzja w konfiguracji EF.
- Stan magazynowy wynika z **ruchów magazynowych (append-only)**, nie z ręcznej edycji liczby.
- Dokumenty (PZ, WZ, zamówienia, faktury) mają cykl życia: szkic → zatwierdzony → anulowany.
  Zatwierdzonych dokumentów nie edytujemy, tylko korygujemy.
- Operacje zmieniające kilka tabel naraz wykonujemy w **transakcji**.
- Numeracja dokumentów jest unikalna i bez luk w ramach typu i roku.
- Każda zmiana danych biznesowych zostawia ślad audytowy (kto, co, kiedy).
- Soft delete zamiast fizycznego usuwania danych, do których odwołują się dokumenty.

Szczegóły: `docs/DATA_MODEL.md`.

## Konwencje kodu
- C#: PascalCase dla typów i metod, camelCase dla zmiennych lokalnych, `_camelCase` dla pól prywatnych.
- Włączone nullable reference types. Brak ostrzeżeń kompilatora w commitach.
- Metody asynchroniczne kończą się na `Async` i przyjmują `CancellationToken` tam, gdzie to ma sens.
- DTO oddzielone od encji. Encje nie wychodzą poza API.
- Walidacja wejścia na brzegu (API), reguły biznesowe w `Erp.Core`.
- Testy: nazwa w stylu `Method_Scenario_ExpectedResult`.

## Praca na dwóch systemach (macOS + Windows, IDE: JetBrains Rider)
Projekt rozwijam naprzemiennie na macOS i Windows. Repo musi działać identycznie na obu:
- **Git jest jedynym sposobem synchronizacji.** Przed zakończeniem pracy: commit + push.
  Na początku pracy na drugiej maszynie: pull.
- `global.json` przypina wersję SDK, żeby obie maszyny budowały tą samą wersją .NET.
- `.gitattributes` (`* text=auto`) i `.editorconfig` ujednolicają końce linii i styl kodu.
- **Ścieżki:** zawsze `Path.Combine`, nigdy ręcznie sklejane `\` lub `/`.
- **Wielkość liter w nazwach plików ma znaczenie** (CI na Linuksie jest wrażliwe, macOS i Windows nie).
  Nazwa pliku musi zgadzać się z nazwą klasy co do litery.
- **Sekrety i `.env` są lokalne dla każdej maszyny** (user secrets, `.env` w `.gitignore`).
  Do repo trafia tylko `.env.example`.
- **Dane w bazie Dockera NIE synchronizują się między maszynami.** Schemat odtwarzają migracje,
  a dane demo skrypt seedujący. Baza musi dać się postawić od zera jedną komendą.
- Certyfikat deweloperski HTTPS: `dotnet dev-certs https --trust` na każdej maszynie osobno.
- Rider: włącz Settings Sync (konto JetBrains), żeby ustawienia i skróty były takie same.

## Git
- Małe, częste commity po działającym kroku. Format: `feat: add contractor entity`,
  `fix: ...`, `test: ...`, `docs: ...`, `refactor: ...`.
- Gałąź `main` zawsze buduje się i przechodzi testy. Funkcje w gałęziach `feature/...`.

## Komendy
```
dotnet build
dotnet test
dotnet run --project src/Erp.Api
docker compose up -d                      # uruchamia bazę
dotnet ef migrations add <Name> -p src/Erp.Data -s src/Erp.Api
dotnet ef database update -p src/Erp.Data -s src/Erp.Api
```

## Definition of Done (dla każdego zadania)
- [ ] Działa i buduje się bez ostrzeżeń
- [ ] Ma testy dla reguł biznesowych
- [ ] Rozumiem każdą linię i potrafię ją wyjaśnić
- [ ] Przeszło code review (Claude Code)
- [ ] Zacommitowane małym, opisowym commitem
- [ ] Zaktualizowany `docs/ROADMAP.md`

## Aktualny etap
**Etap 0 – Fundamenty i odświeżenie C#.** Zadania: `docs/STAGE_0_TASKS.md`.
Pełny plan: `docs/ROADMAP.md`. Wizja i zakres: `docs/PROJECT_VISION.md`.

## Jak zaczynać sesję
Na początku każdej sesji: przeczytaj ten plik i `docs/ROADMAP.md`, powiedz mi w 2–3 zdaniach,
na jakim etapie jesteśmy i co proponujesz zrobić jako następny mały krok.
