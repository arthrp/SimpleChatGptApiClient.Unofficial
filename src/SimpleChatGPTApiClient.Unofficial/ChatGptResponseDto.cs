namespace SimpleChatGPTApiClient.Unofficial;

public record ChatGptResponseDto()
{   
    public string Id { get; init; }
    public string Model { get; init; }
    public IEnumerable<ChoiceDto> Choices { get; init; }
}

public record ChoiceDto(string Text);