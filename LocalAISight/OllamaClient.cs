using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
namespace LocalAISight;
public class OllamaClient
{
    private readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(120) };
    private const string OllamaUrl = "http://localhost:11434/api/generate";
    private const string DefaultQuestion = "Beskriv det du ser här";

    public async Task<string> GetDescriptionAsync(string img, string question = null)
    {
        var myPrompt = !string.IsNullOrEmpty(question) ? question : DefaultQuestion;
        var payload = new
        {
            model = "ministral-3",
            prompt = myPrompt +
                     "Extrahera all text ordagrant och beskriv visuella element kortfattat. Du talar alltid svenska, såvida det inte handlar om text som OCR behandlats, då ska den visas som den är.",
            images = new[] { img },
            stream = false,
            options = new { temperature = 0 },
            keep_alive = -1
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync(OllamaUrl, payload);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("response").GetString();
        }
        catch (Exception ex)
        {
            return $"Fel vid kontakt med AI-modellen: {ex.ToString()}";
        }
    }
}