public class Lexer
{
    private string text;

    public Lexer(string text)
    {
        this.text = text;
    }

    public void Tokenize(params int[] pos)
    {

    }

    public void CheckOnIdentifiers()
    {
        for (int i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case 's':
                    break;
                case 'c':
                    break;
            }
        }
    }

    public bool CheckOnNumber(string token)
    {
        return token.All(char.IsDigit);
    }

    public void CheckOnOperations()
    {
        for (int i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case '+':
                    break;
                case '-':
                    break;
                case '/':
                    break;
                case '*':
                    break;
            }
        }
    }

    public void CheckOnSymbols()
    {
        for (int i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case '"':
                    break;
                case '\'':
                    break;
                case '(':
                    break;
                case ')':
                    break;
                case '-': 
                    break;
                case '}':
                    break;
                case '{':
                    break;
                case '[':
                    break;
                case ']':
                    break;
            }
        }
    }
}

