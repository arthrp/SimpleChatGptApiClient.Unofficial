namespace SimpleChatGPTApiClient.Unofficial;

public record ChatGptRequestDto()
{
    /// <summary>
    /// Max amount of response tokens
    /// </summary>
    public int MaxTokens { get; init; }
    /// <summary>
    /// Model identifier, e.g. text-davinci-003
    /// </summary>
    public string Model { get; init; }
    /// <summary>
    /// Stop token to be used
    /// </summary>
    public List<string> Stop { get; init; }
    public double Temperature { get; set; } = 0.7;
    public double PresencePenalty { get; init; } = 0.6;
    /// <summary>
    /// Prompt to be sent, includes your input and possibly other text to e.g. limit response size
    /// </summary>
    public string Prompt { get; init; }
}