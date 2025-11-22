namespace RaylibConsole.oodoor;

public class Lexer
{
    private string text;
    private int pos;
    private List<Token> tokens;

    public Lexer(string text)
    {
        this.text = text;
        this.pos = 0;
        this.tokens = new List<Token>();
    }
    
    static List<string> GetData(string file)
    {
        List<string> textStructure = new List<string>();
        using (StreamReader rFile = new StreamReader(file))
        {
            string linesFromText = rFile.ReadLine();
            textStructure.Add(linesFromText);
        }
        return textStructure;
    }

    public List<Token> Evaluate()
    {
        while (!IsAtEnd())
        {
            char c = Advance();

            if (char.IsWhiteSpace(c))
                continue;

            if (ReadDelimiter(c))
            {
                tokens.Add(new Token(TokenType.Delimiter, c.ToString()));
                continue;
            }

            if (c == ';')
            {
                tokens.Add(new Token(TokenType.EndOfLine, ";"));
                continue;
            }

            if (ReadOperator(c))
            {
                tokens.Add(new Token(TokenType.Operator, c.ToString()));
                continue;
            }

            if (char.IsDigit(c))
            {
                tokens.Add(ReadFullNumber(c));
                continue;
            }

            if (c == '"')
            {
                tokens.Add(ReadFullString());
                continue;
            }

            if (char.IsLetter(c))
            {
                tokens.Add(ReadFullIdentifier(c));
                continue;
            }
        }

        tokens.Add(new Token(TokenType.EOF, ""));
        return tokens;
    }

    //-------------------------------------------------------------------
    // Helpers
    //-------------------------------------------------------------------

    private bool IsAtEnd() => pos >= text.Length;

    private char Advance() => text[pos++];

    private char Peek() => IsAtEnd() ? '\0' : text[pos];

    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (text[pos] != expected) return false;
        pos++;
        return true;
    }

    //-------------------------------------------------------------------
    // Token builders
    //-------------------------------------------------------------------

    private Token ReadFullNumber(char first)
    {
        string buffer = first.ToString();

        while (!IsAtEnd() && char.IsDigit(Peek()))
            buffer += Advance();

        return new Token(TokenType.Number, buffer);
    }

    private Token ReadFullString()
    {
        string buffer = "";

        while (!IsAtEnd() && Peek() != '"')
            buffer += Advance();

        Advance(); // consume closing quote

        return new Token(TokenType.StringLiteral, buffer);
    }

    private Token ReadFullIdentifier(char first)
    {
        string buffer = first.ToString();

        while (!IsAtEnd() && char.IsLetterOrDigit(Peek()))
            buffer += Advance();

        if (IsKeyword(buffer))
            return new Token(TokenType.Keyword, buffer);

        return new Token(TokenType.Identifier, buffer);
    }

    //-------------------------------------------------------------------
    // Recognition helpers
    //-------------------------------------------------------------------

    private bool ReadDelimiter(char c)
    {
        return "(){}[]'".Contains(c);
    }

    private bool ReadOperator(char c)
    {
        return "+-/*".Contains(c);
    }

    private bool IsKeyword(string word)
    {
        switch (word)
        {
            case "create":
            case "say":
            case "if":
                return true;
            default:
                return false;
        }
    }
}