namespace EICS.WordleBlazor.Game;
public class Guess {
    private readonly GuessResult[] _result;
    private readonly string _answer;
    public Guess(string answer) {
        _answer = Cleanse(answer);
        _result = new GuessResult[answer.Length];
    }
    public bool Win() => _result.All(g => g.Result == MatchResult.FullHit);

    public GuessResult[] Match(string input) {
        if (input.Length != _answer.Length)
            throw new Exception("Should have same length.");

        input = Cleanse(input);

        for (var i = 0; i < input.Length; i += 1) {
            if (!char.IsLetter(input[i]) || !char.IsLetter(_answer[i])) throw new Exception("Should contains letter only.");

            _result[i] = new GuessResult(input[i], DetermineResult(input[i], _answer[i]));
        }

        return _result;
    }

    private MatchResult DetermineResult(char charFromInput, char charFromAnswer) {
        if (charFromInput.Equals(charFromAnswer)) return MatchResult.FullHit;
        return _answer.Contains(charFromInput) ? MatchResult.CharHit : MatchResult.NoneHit;
    }

    private static string Cleanse(string input) => input.Trim().ToLower();
}
