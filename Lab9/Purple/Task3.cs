namespace Lab9.Purple;

public class Task3 : Purple
{
    private string _output;
    private (string, char)[] _codes; 

    public string Output => _output;
    public (string, char)[] Codes => _codes;

    public Task3(string text) : base(text)
    {
        _output = string.Empty;
        _codes = new (string, char)[0];
    }

    public override void Review()
    {
        if (Input == null)
        {
            _output = null;
            _codes = null;
            return;
        }

        string[] pairs = new string[Input.Length]; 
        int[] counts = new int[Input.Length]; 
        int[] firstPos = new int[Input.Length]; 
        int pairCount = 0; 
        
        for (int i = 0; i < Input.Length - 1; i++) 
        {
            if (char.IsLetter(Input[i]) && char.IsLetter(Input[i + 1]))
            {
                string pair = "" + Input[i] + Input[i + 1]; 
                int index = -1;

                for (int j = 0; j < pairCount; j++) 
                {
                    if (pairs[j] == pair)
                    {
                        index = j;
                        break;
                    }
                }

                if (index == -1)
                {
                    pairs[pairCount] = pair;
                    counts[pairCount] = 1;
                    firstPos[pairCount] = i;
                    pairCount++;
                }
                else
                {
                    counts[index]++;
                }
            }
        }
        
        int topCount = pairCount < 5 ? pairCount : 5; 
        _codes = new (string, char)[topCount];
        
        for (int k = 0; k < topCount; k++)
        {
            int best = -1;

            for (int i = 0; i < pairCount; i++)
            {
                if (counts[i] < 0)
                {
                    continue;
                }

                if (best == -1 ||
                    counts[i] > counts[best] ||
                    (counts[i] == counts[best] && firstPos[i] < firstPos[best]))
                {
                    best = i;
                }
            }

            _codes[k] = (pairs[best], '\0');
            counts[best] = -1; 
        }
        
        bool[] used = new bool[127];

        for (int i = 0; i < Input.Length; i++)
        {
            if (Input[i] >= 32 && Input[i] <= 126)
            {
                used[Input[i]] = true;
            }
        }

        int codeIndex = 0;
        for (int i = 32; i <= 126 && codeIndex < topCount; i++)
        {
            if (!used[i])
            {
                _codes[codeIndex] = (_codes[codeIndex].Item1, (char)i); 
                codeIndex++;
            }
        }
        
        string result = Input; 

        for (int c = 0; c < _codes.Length; c++) 
        {
            string pair = _codes[c].Item1;
            char code = _codes[c].Item2;

            string newText = "";
            int i = 0;

            while (i < result.Length)
            {
                if (i < result.Length - 1 &&
                    result[i] == pair[0] &&
                    result[i + 1] == pair[1]) 
                {
                    newText += code;
                    i += 2; 
                }
                else
                {
                    newText += result[i]; 
                    i++;
                }
            }

            result = newText;
        }

        _output = result;
    }

    public override string ToString()
    {
        return Output ?? ""; 
    }
}