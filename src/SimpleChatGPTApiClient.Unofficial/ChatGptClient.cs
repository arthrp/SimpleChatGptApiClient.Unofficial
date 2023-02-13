using System.Text;
using System.Text.Json;
using Yoh.Text.Json.NamingPolicies;

namespace SimpleChatGPTApiClient.Unofficial;

using System;
public class ChatGptClient
{
    private readonly string _baseUrl;
    private readonly string _apiKey;
    private readonly int _maxResponseTokens;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly string _promptPrefix;
    public const string STOP_TOKEN = "x|im_end|x";

    /// <summary>
    /// Client for ChatGPT API
    /// </summary>
    /// <param name="apiKey">Your OpenAI API key</param>
    /// <param name="baseUrl">Base url for ChatGPT API</param>
    /// <param name="maxResponseTokens">Max amount of response tokens</param>
    public ChatGptClient(string apiKey, string baseUrl = "https://api.openai.com", int maxResponseTokens = 1000)
    {
        _apiKey = apiKey;
        _baseUrl = baseUrl;
        _maxResponseTokens = maxResponseTokens;

        _serializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicies.SnakeCaseLower };
        _promptPrefix = "You are ChatGPT, a large language model trained by OpenAI. You answer as concisely as possible for each response (e.g. don’t be verbose).User:\n\n";
    }

    public async Task<string> GetResponse(string text, int timeoutMs = 5000)
    {
        var message = BuildPrompt(text);
        
        var dto = new ChatGptRequestDto()
        {
            MaxTokens = _maxResponseTokens, 
            Model = "text-davinci-003",
            Stop = new List<string>() { STOP_TOKEN },
            Prompt = message
        };

        var body = JsonSerializer.Serialize(dto, _serializerOptions);
        //Dirty hack
        body = body.Replace("x|", "<|").Replace("|x", "|>");
        
        var request = new HttpRequestMessage() {
            RequestUri = new Uri($"{_baseUrl}/v1/completions"),
            Method = HttpMethod.Post,
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        };

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromMilliseconds(timeoutMs);
        var response = await httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ChatGptResponseDto>(json, _serializerOptions);
        return result.Choices.ElementAt(0).Text;
    }

    private string BuildPrompt(string text)
    {
        return $"{_promptPrefix}{text}{STOP_TOKEN}\n\nChatGPT:\n";
    }
}
