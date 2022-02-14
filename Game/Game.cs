namespace EICS.WordleBlazor.Game;
public class Game {
    public string Answer { get; private set; }
    private int MaxAttempt { get; set; }
    public readonly List<GuessResult[]> Results;
    public readonly Dictionary<char, MatchResult> Distribution = new();
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
        Distribution.Clear();
        GameUpdated?.Invoke();
    }

    public void Guess(string input) {
        if(IsWin || IsLose) return;
        if(Answer.Equals(string.Empty)) return;
        if(Results.Count == MaxAttempt) return;

        var guess = new Guess(Answer);
        var guessResults = guess.Match(input);

        MatchDistribution(guessResults);

        Results.Add(guessResults);
        IsWin = guess.Win();
        GameUpdated?.Invoke();
    }

    private void MatchDistribution(IEnumerable<GuessResult> results) {
        foreach ((var letter, var matchResult) in results) {
            if (!Distribution.ContainsKey(letter)) {
                Distribution[letter] = matchResult;
                continue;
            }

            var matchType = Distribution[letter];

            if(matchType == MatchResult.NoneHit && matchResult != MatchResult.NoneHit ||
               matchType == MatchResult.CharHit && matchResult == MatchResult.FullHit)
            {
                Distribution[letter] = matchResult;
            }
        }
    }
}
