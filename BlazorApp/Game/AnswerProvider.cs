namespace EverythingInCSharp.Muddle.Game;
public class AnswerProvider {
    private static readonly List<string> WordList = new();
    private static readonly Random Random = new();
    
    private readonly IAnswerSource _answerSource;
    public AnswerProvider(IAnswerSource answerSource) {
        _answerSource = answerSource;

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
        var answers = await _answerSource.LoadAsync();
        WordList.AddRange(answers);
    }
}
