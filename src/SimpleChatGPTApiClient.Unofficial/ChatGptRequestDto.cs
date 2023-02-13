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
    /// Stop tokens to be used
    /// </summary>
    public List<string> Stop { get; init; }
    /// <summary>
    /// Defines variability of the output (0 = almost no variability, 1 = max)
    /// </summary>
    public double Temperature { get; set; } = 0.7;
    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far
    /// </summary>
    public double PresencePenalty { get; init; } = 0.6;
    /// <summary>
    /// Prompt to be sent, includes your input and possibly other text
    /// </summary>
    public string Prompt { get; init; }
}