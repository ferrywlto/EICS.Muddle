namespace EICS.WordleBlazor.Game;
public class Game {
    public string Answer { get; private set; }
    private int MaxAttempt { get; set; }
    public readonly List<GuessResult[]> Results;
    public event Action? GameUpdated;
    public bool IsWin { get; private set; }
    public bool IsLose => !IsWin && Results.Count >= MaxAttempt && !Answer.Equals(string.Empty);

    public Game() {
        Answer = string.Empty;
        Results = new List<GuessResult[]>();
    }

    public void Reset(string answer, int maxAttempt) {
        Answer = answer;
        MaxAttempt = maxAttempt;
        Results.Clear();
        GameUpdated?.Invoke();
    }

    public void Guess(string input) {
        if(Answer.Equals(string.Empty)) return;

        var guess = new Guess(Answer);
        Results.Add(guess.Match(input));
        IsWin = guess.Win();
        GameUpdated?.Invoke();
    }
}
