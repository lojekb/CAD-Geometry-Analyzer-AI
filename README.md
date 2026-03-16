# CAD_Analyzer 🛠️🤖

**CAD_Analyzer** to profesjonalne narzędzie do analizy geometrii 3D (pliki STL), stworzone jako projekt rekrutacyjny na staż w firmie Bosch. Aplikacja łączy niskopoziomowe przetwarzanie danych z nowoczesnymi technologiami AI (LLM), umożliwiając inteligentną interakcję z modelami CAD.

---

## 📋 Spis treści
1. [O Projekcie](#-o-projekcie)
2. [Kluczowe Funkcjonalności](#-kluczowe-funkcjonalności)
3. [Architektura Techniczna](#-architektura-techniczna)
4. [Silnik Matematyczny](#-silnik-matematyczny)
5. [Integracja AI (LLM)](#-integracja-ai-llm)
6. [Instalacja i Uruchomienie](#-instalacja-i-uruchomienie)
7. [Plany Rozwoju](#-plany-rozwoju)

---

## 📖 O Projekcie
Projekt demonstruje podejście "AI-Augmented Engineering". Narzędzie nie tylko parsuje surowe dane o trójkątach, ale dzięki integracji z lokalnym modelem językowym, pozwala inżynierowi zadawać pytania dotyczące fizycznych właściwości modelu w języku naturalnym.

## ✨ Kluczowe Funkcjonalności
- **Wydajny Parser STL (ASCII):** Przetwarzanie potokowe (line-by-line), minimalizujące zużycie pamięci RAM przy dużych plikach.
- **Analiza Geometryczna:** Precyzyjne obliczanie pola powierzchni całkowitej oraz wyznaczanie skrajni modelu (Bounding Box).
- **Lokalny Konsultant AI:** Wykorzystanie modelu Llama 3 do interpretacji wyników technicznych.
- **Bezpieczeństwo Danych:** Dzięki użyciu Ollamy, dane CAD nie opuszczają lokalnej maszyny (krytyczny wymóg w branży R&D).

## 🏗️ Architektura Techniczna
Aplikacja została napisana w języku **C# (.NET 8)** z zachowaniem zasad **SOLID**:
- `StlParser`: Odpowiada za I/O i ekstrakcję danych tekstowych.
- `GeometryAnalyzer`: Czysta logika matematyczna wykorzystująca `System.Numerics`.
- `AiConsultant`: Warstwa abstrakcji łącząca się z API Ollama.
- `Models`: Wykorzystanie nowoczesnych rekordów (`records`) do reprezentacji danych.



## 📐 Silnik Matematyczny
Obliczanie pola powierzchni opiera się na algorytmie iloczynu wektorowego. Dla każdego trójkąta o wierzchołkach $V_1, V_2, V_3$ pole obliczane jest wzorem:

$$Area = \frac{1}{2} |(V_2 - V_1) \times (V_3 - V_1)|$$

Dzięki zastosowaniu biblioteki `System.Numerics.Vector3`, obliczenia są zoptymalizowane pod kątem instrukcji SIMD procesora.

## 🤖 Integracja AI (LLM)
Program wykorzystuje technikę **Prompt Engineeringu**. Wyniki analizy matematycznej są mapowane na techniczny kontekst, który trafia do modelu LLM.

**Przykład zapytania:**
> *Użytkownik:* "Czy ten detal zmieści się w pudełku o wymiarach 100x100x100 mm?"
> *AI:* (na podstawie Bounding Box) "Tak, model ma wymiary 10x10x10 mm, więc bez problemu zmieści się w podanym opakowaniu, pozostawiając duży margines."



## 🚀 Instalacja i Uruchomienie

### Wymagania:
- .NET 8 SDK
- [Ollama](https://ollama.com/) (z pobranym modelem `llama3`)

### Kroki:
1. Upewnij się, że Ollama działa:
   ```bash
   ollama run llama3
