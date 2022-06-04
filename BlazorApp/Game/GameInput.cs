namespace EverythingInCSharp.Muddle.Game;

public class GameInput {
    public event Action? InputChanged;
    private char[] _buffer = Array.Empty<char>();
    private int _idx;
    private int _size;
    public char GetInputAt(int idx) {
        if (BufferNotSet()) return ' ';

        if (idx < 0) idx = 0;
        if (idx > _buffer.Length) idx = _buffer.Length;
        return _buffer[idx];
    }
    public string Flush() {
        if (!CanFlush())
            throw new Exception("Pre-mature flush. Do it only when buffer is fully filled.");

        var text = string.Join(string.Empty, _buffer);
        ResetBuffer();

        return text;
    }

    public void Input(char letter) {
        if(BufferNotSet() || InvalidIndexRange()) return;

        if(_buffer[_idx].Equals(' '))
            _buffer[_idx] = letter;

        if (_idx < MaxIndex) {
            _idx += 1;
        }

        InputChanged?.Invoke();
    }
    public void Back() {
        if(BufferNotSet() || InvalidIndexRange()) return;

        if (_idx > MinIndex) {
            if (_buffer[_idx].Equals(' ')) {
                _idx -= 1;
                _buffer[_idx] = ' ';
            }
            else {
                _buffer[_idx] = ' ';
            }
        }

        InputChanged?.Invoke();
    }

    public void Reset(int length) {
        _size = length;

        ResetBuffer();
    }

    private void ResetBuffer() {
        _buffer = Enumerable.Repeat(' ', _size).ToArray();
        _idx = MinIndex;
    }

    public bool CanFlush() => _buffer.All(char.IsLetter);
    private bool BufferNotSet() => _buffer.Length == 0;
    private bool InvalidIndexRange() => _idx < MinIndex || _idx > MaxIndex;
    private int MinIndex => 0;
    private int MaxIndex => _buffer.Length - 1;
}
