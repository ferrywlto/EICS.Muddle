namespace EICS.WordleBlazor.Game;
public class Game {
    public readonly int Difficulty;
    public readonly List<GuessResult[]> Results = new();
    public event Action? GameUpdated;
    public bool IsWin { get; private set; }

    private readonly string _answer;
    public Game(string answer) {
        _answer = answer;
        Difficulty = _answer.Length;
    }

    public void Guess(string input) {
        var guess = new Guess(_answer);
        Results.Add(guess.Match(input));
        IsWin = guess.Win();
        GameUpdated?.Invoke();
    }
//
//    private bool IndexInValidRange() => _currentInputIdx >= MinIndex && _currentInputIdx <= MaxIndex;
//    private int MinIndex => 0;
//    private int MaxIndex => Difficulty - 1;
//
//    public event Action? InputChanged;
//    private readonly char[] _inputBuffer;
//    private int _currentInputIdx;
//    public char GetInput(int idx) {
//        if (idx < 0) idx = 0;
//        if (idx > Difficulty) idx = Difficulty;
//        return _inputBuffer[idx];
//    }
//    private string Flush() {
//        var text = string.Join(string.Empty, _inputBuffer);
//
//        for (var i = 0; i < _inputBuffer.Length; i++) {
//            _inputBuffer[i] = ' ';
//        }
//        _currentInputIdx = MinIndex;
//
//        return text;
//    }
//    public void Input(char letter) {
//        if(!IndexInValidRange()) return;
//
//        if(_inputBuffer[_currentInputIdx].Equals(' '))
//            _inputBuffer[_currentInputIdx] = letter;
//
//        if (_currentInputIdx < MaxIndex) {
//            _currentInputIdx += 1;
//        }
//
//        InputChanged?.Invoke();
//    }
//
//    public void Back() {
//        if(!IndexInValidRange()) return;
//
//        if (_currentInputIdx > MinIndex) {
//            if (_inputBuffer[_currentInputIdx].Equals(' ')) {
//                _currentInputIdx -= 1;
//                _inputBuffer[_currentInputIdx] = ' ';
//            }
//            else {
//                _inputBuffer[_currentInputIdx] = ' ';
//            }
//        }
//
//        InputChanged?.Invoke();
//    }
}
