namespace Lab9.Purple;

public class Task2 : Purple
{
    private string[] _output;
    public string[] Output => _output;

    public Task2(string text) : base(text)
    {
        _output = null;
    }

    public override void Review()
    {
        if (Input == null)
        {
            _output = null;
            return;
        }

        string[] raw = Input.Split(' ');

        string[] words = new string[raw.Length];
        int wordCount = 0;
        for (int i = 0; i < raw.Length; i++) 
        {
            if (raw[i] != "")
            {
                words[wordCount] = raw[i];
                wordCount++;
            }
        }

        string[] lines = new string[words.Length];
        int lineCount = 0;
        string[] current = new string[words.Length];
        int count = 0; 
        int letters = 0; 

        for (int i = 0; i < wordCount; i++)
        {
            string word = words[i];

            if (count == 0) 
            {
                current[count] = word;
                count++;
                letters = word.Length;
            }
            else
            {
                int length = letters + word.Length + count; 

                if (length <= 50)
                {
                    current[count] = word;
                    count++;
                    letters += word.Length;
                }
                else
                {
                    lines[lineCount] = MakeLine(current, count, letters);
                    lineCount++;

                    current = new string[words.Length];
                    current[0] = word; 
                    count = 1;
                    letters = word.Length;
                }
            }
        }

        if (count > 0) 
        {
            lines[lineCount] = MakeLine(current, count, letters);
            lineCount++;
        }

        _output = new string[lineCount]; 
        for (int i = 0; i < lineCount; i++)
        {
            _output[i] = lines[i];
        }
    }

    private string MakeLine(string[] words, int count, int letters)
    {
        if (count == 1) 
        {
            return words[0];
        }

        int gaps = count - 1; 
        int spaces = 50 - letters; 

        int baseSpaces = spaces / gaps; 
        int extra = spaces % gaps; 

        string result = "";

        for (int i = 0; i < count; i++)
        {
            result += words[i];

            if (i < gaps)
            {
                int s = baseSpaces;

                if (extra > 0) 
                {
                    s++;
                    extra--;
                }

                for (int j = 0; j < s; j++) 
                {
                    result += " ";
                }
            }
        }
        return result;
    }

    public override string ToString()
    {
        if (Output == null)
            return "";

        return string.Join(Environment.NewLine, Output);
    }
}