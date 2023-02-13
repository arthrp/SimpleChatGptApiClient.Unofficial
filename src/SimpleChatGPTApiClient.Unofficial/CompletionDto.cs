namespace SimpleChatGPTApiClient.Unofficial;

public record CompletionDto()
{   
    public string Id { get; init; }
    public string Model { get; init; }
    public IEnumerable<ChoiceDto> Choices { get; init; }
    public UsageDto Usage { get; set; }
}

public record ChoiceDto(string Text);

public record UsageDto(int PromptTokens, int CompletionTokens, int TotalTokens);