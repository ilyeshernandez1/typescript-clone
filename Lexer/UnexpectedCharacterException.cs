namespace PureType
{
    public class UnexpectedCharacterException : Exception
    {
        public char? UnexpectedChar { get; }

        public UnexpectedCharacterException(char? unexpectedChar)
            : base($"Unexpected character: {unexpectedChar}")
        {
            UnexpectedChar = unexpectedChar;
        }
    }
}
