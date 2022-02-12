namespace EICS.WordleBlazor.Game;
public class Game {
    //Override this load answer from elsewhere
    private static string SelectAnswer() => WordList[new Random().Next() % WordList.Count];
    private static List<string> WordList = new() {"hello", "world", "fiddle", "middle", "sharp", "server"};
    public event Action? InputChanged;
    public event Action? GameUpdated;

    public readonly int Difficulty;
    private readonly char[] _inputBuffer;
    private readonly string _answer;
    private int _currentInputIdx;

    public readonly List<GuessResult[]> Results = new();

    public bool IsWin { get; private set; }



    public Game(string answer) {
        _answer = SelectAnswer();
        Difficulty = _answer.Length;
        _inputBuffer = new char[Difficulty];

        for (var i = 0; i < _inputBuffer.Length; i += 1) {
            _inputBuffer[i] = ' ';
        }
    }

    private string Flush() {
        var text = string.Join(string.Empty, _inputBuffer);

        for (var i = 0; i < _inputBuffer.Length; i++) {
            _inputBuffer[i] = ' ';
        }
        _currentInputIdx = MinIndex;

        return text;
    }

    public void Guess() {
        if (!_inputBuffer.All(char.IsLetter)) return;

        var guess = new Guess(_answer);
        var strToGuess = Flush();
        Results.Add(guess.Match(strToGuess));
        IsWin = guess.Win();
        GameUpdated?.Invoke();
    }

    public void Input(char letter) {
        if(!IndexInValidRange()) return;

        if(_inputBuffer[_currentInputIdx].Equals(' '))
            _inputBuffer[_currentInputIdx] = letter;

        if (_currentInputIdx < MaxIndex) {
            _currentInputIdx += 1;
        }

        InputChanged?.Invoke();
    }

    public void Back() {
        if(!IndexInValidRange()) return;

        if (_currentInputIdx > MinIndex) {
            if (_inputBuffer[_currentInputIdx].Equals(' ')) {
                _currentInputIdx -= 1;
                _inputBuffer[_currentInputIdx] = ' ';
            }
            else {
                _inputBuffer[_currentInputIdx] = ' ';
            }
        }

        InputChanged?.Invoke();
    }

    public char GetInput(int idx) {
        if (idx < 0) idx = 0;
        if (idx > Difficulty) idx = Difficulty;
        return _inputBuffer[idx];
    }

    private bool IndexInValidRange() => _currentInputIdx >= MinIndex && _currentInputIdx <= MaxIndex;
    private int MinIndex => 0;
    private int MaxIndex => Difficulty - 1;
}
