namespace PureType
{
    public enum TokenType
    {
        Int,
        Plus,
        Minus,
        Float,
        Multiply,
        Div,
        LeftParen,
        RightParen,
    }

    public class Token
    {
        public TokenType Type { get; }
        public string? Value { get; }

        public Token(TokenType type, string? value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return Value != null ? $"{Type}:{Value}" : $"{Type}";
        }
    }
}
