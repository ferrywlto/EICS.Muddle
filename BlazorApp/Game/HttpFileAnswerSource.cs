namespace EverythingInCSharp.Muddle.Game;

public sealed class HttpFileAnswerSource : IAnswerSource {
    private readonly string _filePath;
    private readonly HttpClient _httpClient;
    public HttpFileAnswerSource(HttpClient httpClient, string filePath) {
        _filePath = filePath;
        _httpClient = httpClient;
    }

    public async Task<string[]> LoadAsync() {
        try {
            var fileContent = await _httpClient.GetStringAsync(_filePath);

            return string.IsNullOrEmpty(fileContent)
                ? Array.Empty<string>()
                : fileContent.Split(Environment.NewLine);
        }
        catch (Exception) {
            return Array.Empty<string>();
        }
    }
}
