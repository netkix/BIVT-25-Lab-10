namespace Lab9.Purple;

public class Task4 : Purple
{
    private string _output;
    private (string, char)[] _codes;

    public string Output => _output;

    public Task4(string text, (string, char)[] codes) : base(text)
    {
        _output = string.Empty;
        _codes = codes;
    }

    public override void Review()
    {
        if (Input == null)
        {
            _output = null;
            return;
        }

        if (_codes == null)
        {
            _output = Input;
            return;
        }

        string result = "";

        for (int i = 0; i < Input.Length; i++)
        {
            bool found = false;

            for (int j = 0; j < _codes.Length; j++)
            {
                if (Input[i] == _codes[j].Item2)
                {
                    result += _codes[j].Item1;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                result += Input[i];
            }
        }

        _output = result;
    }

    public override string ToString()
    {
        return Output ?? "";
    }
}