namespace EverythingInCSharp.Muddle.Game;

public record GuessResult(char Letter, MatchResult Result);

public static class GuessResultExtenstion {
    public static MatchResult[] GetMatchResults(this GuessResult[] input) => input.Select(x => x.Result).ToArray();
}
