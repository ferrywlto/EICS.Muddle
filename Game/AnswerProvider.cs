namespace EICS.WordleBlazor.Game;

public class AnswerProvider {
    private static readonly List<string> WordList = new();
    private static readonly Random Random = new();
    private readonly HttpClient _httpClient;
    private readonly string filePath;
    public AnswerProvider(HttpClient httpClient, string filePath) {
        _httpClient = httpClient;
        this.filePath = filePath;
    }
    public async Task<string> GetNewAnswer() {
        if (WordList.Count == 0) {
            await LoadAnswersAsync();
        }

        var idx = Random.Next(WordList.Count - 1);
        return WordList[idx];
    }
    private async Task LoadAnswersAsync() {
        WordList.Clear();
        var fileContent = await _httpClient.GetStringAsync(filePath);
        WordList.AddRange(fileContent.Split(Environment.NewLine));
    }
}
