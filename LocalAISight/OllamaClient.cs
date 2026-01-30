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
        var myPrompt = !string.IsNullOrEmpty(question) ? question : Properties.Settings.Default.DefaultPrompt;
        var mySystemPrompt = Properties.Settings.Default.SystemPrompt;
        var payload = new
        {
            model = Properties.Settings.Default.Model,
            system = mySystemPrompt,
            prompt = myPrompt,
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