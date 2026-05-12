using System.Text;

namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;
        public string Output => _output;

        public Task1(string text) : base(text)
        { 
            _output = string.Empty;
        }
    
        public override void Review()
        {
            if (Input == null)
            {
                _output = null;
                return;
            }

            StringBuilder result = new StringBuilder();
            StringBuilder word = new StringBuilder();

            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];

                if (char.IsLetterOrDigit(c) || c == '-' || c == '\'')
                {
                    word.Append(c);
                }
                else
                {
                    if (word.Length > 0)
                    {
                        bool hasDigit = false;
                        for (int j = 0; j < word.Length; j++)
                        {
                            if (char.IsDigit(word[j]))
                            {
                                hasDigit = true;
                                break;
                            }
                        }
                        if (hasDigit)
                        {
                            result.Append(word);
                        }
                        else
                        {
                            for (int l = word.Length - 1; l >= 0; l--) 
                            {
                                 result.Append(word[l]); 
                            }
                        }
                        word.Clear(); 
                    }
                    result.Append(c);
                }
            }

            if (word.Length > 0)
            {
                bool hasDigit = false;
                for (int j = 0; j < word.Length; j++)
                {
                    if (char.IsDigit(word[j]))
                    {
                        hasDigit = true;
                        break;
                    }
                }
                if (hasDigit)
                {
                    result.Append(word);
                }
                else
                {
                    for (int l = word.Length - 1; l >= 0; l--) 
                    {
                        result.Append(word[l]); 
                    }
                }
            }

            _output = result.ToString();
        }

        public override string ToString()
        {
            return Output ?? string.Empty;
        }
    }
}