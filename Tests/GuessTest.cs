using System.Linq;
using EverythingInCSharp.Muddle.Game;
using Xunit;

namespace Muddle.Tests;

public class GuessTest {
    private const string DefaultAnswer = "apple";
    private const string DefaultAnswer2 = "abbba";
    private MatchResult[] FullHit(int length = 5) => Enumerable.Repeat(MatchResult.FullHit, length).ToArray();
    
    [Theory]
    [InlineData("abbbb")]
    [InlineData("abbba")]
    [InlineData("bbbbe")]
    [InlineData("ebbbe")]
    [InlineData("blblb")]
    public void ShouldNotCountHitAfterFullHit(string input) {
        var guess = new Guess(DefaultAnswer);
        var results = guess.Match(input);
        var fullHitCount = results.Count(r => r.Result == MatchResult.FullHit);
        var charHitCount = results.Count(r => r.Result == MatchResult.CharHit);
        Assert.True(fullHitCount == 1 && charHitCount == 0);
    }
    
    [Theory]
    [InlineData("aaaaa")]
    public void ShouldNotCountHitAfterFullHit2(string input) {
        var guess = new Guess(DefaultAnswer2);
        var results = guess.Match(input);

        var expected = FullHit();

        var actual = results.GetMatchResults();
        
        Assert.True(expected.SequenceEqual(actual));
    }
    
    [Fact]
    public void ShouldFullyMatch() {
        var guess = new Guess(DefaultAnswer);
        var results = guess.Match(DefaultAnswer);

        var expected = FullHit();

        var actual = results.GetMatchResults();
        
        Assert.True(expected.SequenceEqual(actual));
    }
}
