namespace EICS.WordleBlazor.Game;

public class GameInput {
    public event Action? InputChanged;
    private readonly char[] _inputBuffer;
    private int _currentIdx;

    public GameInput(int length) {
        _inputBuffer = new char[length];

        ResetInput();
    }
    public char GetInputAt(int idx) {
        if (idx < 0) idx = 0;
        if (idx > _inputBuffer.Length) idx = _inputBuffer.Length;
        return _inputBuffer[idx];
    }
    public string Flush() {
        if (!CanFlush())
            throw new Exception("Pre-mature flush. Do it only when buffer is fully filled.");

        var text = string.Join(string.Empty, _inputBuffer);

        ResetInput();

        return text;
    }
    public void Input(char letter) {
        if(!IndexInValidRange()) return;

        if(_inputBuffer[_currentIdx].Equals(' '))
            _inputBuffer[_currentIdx] = letter;

        if (_currentIdx < MaxIndex) {
            _currentIdx += 1;
        }

        InputChanged?.Invoke();
    }
    public void Back() {
        if(!IndexInValidRange()) return;

        if (_currentIdx > MinIndex) {
            if (_inputBuffer[_currentIdx].Equals(' ')) {
                _currentIdx -= 1;
                _inputBuffer[_currentIdx] = ' ';
            }
            else {
                _inputBuffer[_currentIdx] = ' ';
            }
        }

        InputChanged?.Invoke();
    }
    private void ResetInput() {
        for (var i = 0; i < _inputBuffer.Length; i++) {
            _inputBuffer[i] = ' ';
        }
        _currentIdx = MinIndex;
    }

    public bool CanFlush() => _inputBuffer.All(char.IsLetter);
    private bool IndexInValidRange() => _currentIdx >= MinIndex && _currentIdx <= MaxIndex;
    private int MinIndex => 0;
    private int MaxIndex => _inputBuffer.Length - 1;
}
