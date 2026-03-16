using CAD_Analyzer.Models;
using OllamaSharp;
using CAD_Analyzer.Models;

namespace CAD_Analyzer
{
    public class AiConsultant
    {
        private readonly OllamaApiClient _ollama;

        public AiConsultant(string uri = "http://localhost:11434")
        {
            _ollama = new OllamaApiClient(uri);
            _ollama.SelectedModel = "llama3:latest";
        }

        public async Task<string> GetAiAdviceAsync(GeometryStats stats, string userQuestion)
        {
            string prompt = $@"
                Jesteś ekspertem CAD. Analizujemy model 3D ze statystykami:
                - Liczba trójkątów: {stats.TriangleCount}
                - Pole powierzchni: {stats.TotalSurfaceArea:F2} mm2
                - Wymiary (Bounding Box): od {stats.MinBounds} do {stats.MaxBounds}

                Użytkownik pyta: {userQuestion}
                Odpowiedz krótko i konkretnie jako inżynier.";

            try
            {
                string responseText = "";
                await foreach (var stream in _ollama.GenerateAsync(prompt))
                {
                    responseText += stream.Response;
                }
                return responseText;
            }
            catch (Exception ex)
            {
                return $"Błąd połączenia z AI: {ex.Message}.";
            }
        }
    }
}