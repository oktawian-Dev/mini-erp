# Wizja projektu: Mini-ERP

## Cel
Zbudować działający mini-ERP dla małej firmy handlowej, który:
1. uczy fundamentów wymaganych na stanowiskach Junior .NET Developer,
2. jest dobrze wykonanym projektem portfolio (testy, wdrożenie, dokumentacja),
3. stanowi bazę pod późniejszy, bardziej wyróżniający się projekt.

## Firma demonstracyjna
Fikcyjny **sklep wędkarski „Szczupak & Spółka”**. To tylko scenariusz do danych testowych i opowieści
w README. **Kod pozostaje generyczny** (`Product`, `Warehouse`, `SalesOrder`), żeby system był
w pełni „ERP-em”, a nie jednorazową aplikacją sklepową.

Przykładowe dane demo: kołowrotki, wędziska, żyłki, przynęty, zanęty; dostawcy hurtowi;
klienci detaliczni i firmowi (łowiska, kluby).

## Zakres MVP (wersja 1.0)
| Moduł | Funkcje |
|---|---|
| **Użytkownicy** | Logowanie, role: Administrator, Magazynier, Handlowiec |
| **Kontrahenci** | Klienci i dostawcy, NIP, adresy, status aktywny/nieaktywny |
| **Towary** | Karta towaru, jednostki miary, stawki VAT, ceny, kod/EAN |
| **Magazyn** | Magazyny, przyjęcia (PZ), wydania (WZ), stany, historia ruchów, korekty |
| **Sprzedaż** | Zamówienia sprzedaży z rezerwacją towaru, realizacja zamówienia |
| **Faktury** | Faktura sprzedaży z numeracją, wyliczeniem VAT, wydruk PDF |
| **Audyt** | Ślad zmian: kto, co, kiedy |
| **Raporty** | Stany magazynowe, sprzedaż w okresie |

## Poza zakresem MVP (świadomie odkładamy)
- Zakupy i zamówienia do dostawców
- Produkcja i receptury (BOM)
- Wielowalutowość, korekty faktur, faktury zaliczkowe
- Integracje zewnętrzne (KSeF, NBP)
- Konfigurowalny „uniwersalny ERP” dla wielu branż
- Mikroserwisy, kolejki, chmura

## Stretch goals (jeśli zostanie czas)
- Warianty produktów (kolor, rozmiar) i partie z terminem ważności
- Import towarów z Excela
- Wielu użytkowników edytujących jednocześnie z obsługą konfliktów (concurrency)
- Raport sprzedaży z wykresami

## Cele nauki (co musi być przećwiczone)
- C#: klasy, interfejsy, kolekcje, LINQ, async/await, obsługa wyjątków, nullable
- SQL: SELECT, JOIN, GROUP BY, indeksy, transakcje, normalizacja
- EF Core: encje, relacje, migracje, zapytania, transakcje, współbieżność
- ASP.NET Core: kontrolery/endpointy, DI, walidacja, middleware, autoryzacja
- Testowanie: jednostkowe i integracyjne
- Git/GitHub, Docker, CI/CD
- Dokumentacja i uzasadnianie decyzji projektowych

## Kryteria sukcesu projektu
- Aplikacja uruchamia się jedną komendą (`docker compose up`)
- Pokrycie testami reguł biznesowych magazynu i sprzedaży
- README z opisem, diagramem architektury i listą decyzji projektowych
- Mogę w 10 minut opowiedzieć o każdym module i bronić każdego wyboru

## Limit czasu
**Cel: MVP w ok. 3–4 miesiące.** Projekt CORE ma zostać dokończony, a nie dopracowywany bez końca.
Jeśli zadanie rośnie ponad plan, tniemy zakres, a nie wydłużamy projekt.
