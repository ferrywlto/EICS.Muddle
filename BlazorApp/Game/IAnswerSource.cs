namespace EverythingInCSharp.Muddle.Game;

public interface IAnswerSource {
    Task<string[]> LoadAsync();
}
