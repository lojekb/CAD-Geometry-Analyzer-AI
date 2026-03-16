using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CAD_Analyzer;
using CAD_Analyzer.Models;

namespace CAD_Analyzer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var parser = new StlParser();
            var analyzer = new GeometryAnalyzer();
            var ai = new AiConsultant();

            Console.WriteLine("===============================================");
            Console.WriteLine("   CAD_Analyzer - System Analizy Geometrii 3D");
            Console.WriteLine("===============================================");

            // 1. Pobranie ścieżki do pliku
            Console.Write("\nPodaj ścieżkę do pliku .stl (lub przeciągnij plik tutaj): ");
            string filePath = Console.ReadLine()?.Replace("\"", "") ?? "";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("BŁĄD: Plik nie istnieje. Upewnij się, że ścieżka jest poprawna.");
                return;
            }

            try
            {
                Console.WriteLine("\n[1/3] Parsowanie pliku STL...");
                List<Triangle> triangles = parser.ParseAscii(filePath);
                Console.WriteLine($"      Sukces!");

                Console.WriteLine("[2/3] Przeprowadzanie analizy geometrycznej...");
                GeometryStats stats = analyzer.CalculateStats(triangles);

                Console.WriteLine("\n--- WYNIKI ANALIZY ---");
                Console.WriteLine($"Liczba ścian:      {stats.TriangleCount}");
                Console.WriteLine($"Pole powierzchni:  {stats.TotalSurfaceArea:F2} jednostek kw.");
                Console.WriteLine($"Bounding Box Min:  {stats.MinBounds}");
                Console.WriteLine($"Bounding Box Max:  {stats.MaxBounds}");
                Console.WriteLine("----------------------");

                Console.WriteLine("\n[3/3] Konsultant AI gotowy.");
                Console.WriteLine("Możesz zadawać pytania dotyczące modelu (np. 'Czy to wejdzie do pudełka 10x10x10?')");
                Console.WriteLine("Wpisz 'koniec', aby wyjść.");

                while (true)
                {
                    Console.Write("\nTwoje pytanie: ");
                    string userQuestion = Console.ReadLine() ?? "";

                    if (userQuestion.ToLower() == "koniec")
                        break;

                    if (string.IsNullOrWhiteSpace(userQuestion)) continue;

                    Console.WriteLine("AI analizuje dane...");
                    string aiResponse = await ai.GetAiAdviceAsync(stats, userQuestion);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nODPOWIEDŹ AI:");
                    Console.ResetColor();
                    Console.WriteLine(aiResponse);
                    Console.WriteLine(new string('-', 20));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nWYSTĄPIŁ BŁĄD:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("\nDziękujemy za skorzystanie z CAD_Analyzer. Naciśnij dowolny klawisz, aby zamknąć...");
            Console.ReadKey();
        }
    }
}