using System.Collections;

namespace EverythingInCSharp.Muddle.Game;
public class Guess {
    private readonly GuessResult[] _result;
    private readonly string _answer;
    public Guess(string answer) {
        if (!IsValid(answer)) throw new InvalidCharException(InvalidCharExceptionType.Answer);
        
        _answer = Cleanse(answer);
        _result = new GuessResult[answer.Length];
    }
    public bool Win() => _result.All(g => g is {
        Result: MatchResult.FullHit
    });

    public GuessResult[] Match(string input) {
        if (!IsValid(input)) throw new InvalidCharException(InvalidCharExceptionType.Input);
        
        if (input.Length != _answer.Length)
            throw new Exception("Should have same length.");

        input = Cleanse(input);

        for (var i = 0; i < input.Length; i += 1) {
            _result[i] = new GuessResult(input[i], DetermineResult(input[i], _answer[i]));
        }

        return _result;
    }

    private static bool IsValid(string input) => input.Any(c => !char.IsLetter(c));
    
    private MatchResult DetermineResult(char charFromInput, char charFromAnswer) {
        if (charFromInput.Equals(charFromAnswer)) 
            return MatchResult.FullHit;
        
        if (!_answer.Contains(charFromInput))
            return MatchResult.NoneHit;

        return CharInAnswerCount(charFromInput) > CurrentFullMatchCount(charFromInput)
            ? MatchResult.CharHit
            : MatchResult.NoneHit;
    }

    private static string Cleanse(string input) => input.Trim().ToLower();

    private int CharInAnswerCount(char input) =>  _answer.Count(c => c.Equals(input));
    private int CurrentFullMatchCount(char input) => _result.Count(r => r != null 
                           && r.Letter.Equals(input)
                           && r.Result == MatchResult.FullHit);
}
public enum InvalidCharExceptionType { Answer, Input }
public class InvalidCharException : Exception {
    private readonly InvalidCharExceptionType _type;

    public InvalidCharException(InvalidCharExceptionType type)
        : base($"{nameof(type)} should contains letter only.") {
        _type = type;
    }
}
