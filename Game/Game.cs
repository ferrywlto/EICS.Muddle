namespace EICS.WordleBlazor.Game;
public class Game {
    public readonly string Answer;
    public readonly List<GuessResult[]> Results = new();
    public event Action? GameUpdated;
    public bool IsWin { get; private set; }
    public bool IsLose => !IsWin && Results.Count >= _maxAttempt;

    private readonly int _maxAttempt;
    public Game(string answer, int maxAttempt) {
        Answer = answer;
        _maxAttempt = maxAttempt;
    }

    public void Guess(string input) {
        var guess = new Guess(Answer);
        Results.Add(guess.Match(input));
        IsWin = guess.Win();
        GameUpdated?.Invoke();
    }
}
