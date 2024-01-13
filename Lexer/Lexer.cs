using System.Net.Http.Headers;

namespace PureType
{
    public class Lexer
    {
        public string Input { get; }
        public int Position { get; set; }
        public char? CurrentChar { get; set; }

        public Lexer(string input)
        {
            Input = input;
            Position = -1;
            CurrentChar = null;
            NextChar();
        }

        public void NextChar()
        {
            do
            {
                Position += 1;

                if (Position < Input.Length)
                {
                    CurrentChar = Input[Position];
                }
                else
                {
                    CurrentChar = null;
                    break;
                }
            } while (char.IsWhiteSpace(CurrentChar.Value));
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (CurrentChar != null)
            {
                if (char.IsLetter(CurrentChar.Value))
                {
                    throw new UnexpectedCharacterException(CurrentChar.Value);
                }
                if (char.IsWhiteSpace(CurrentChar.Value))
                {
                    NextChar();
                }

                if (char.IsDigit(CurrentChar.Value))
                {
                    Token token = MakeNumber();
                    tokens.Add(token);
                }

                if (CurrentChar == '+')
                {
                    tokens.Add(new Token(TokenType.Plus, null));
                    NextChar();
                }
                else if (CurrentChar == '-')
                {
                    tokens.Add(new Token(TokenType.Minus, null));
                    NextChar();
                }
                else if (CurrentChar == '*')
                {
                    tokens.Add(new Token(TokenType.Multiply, null));
                    NextChar();
                }
                else if (CurrentChar == '/')
                {
                    tokens.Add(new Token(TokenType.Div, null));
                    NextChar();
                }
                else if (CurrentChar == '(')
                {
                    tokens.Add(new Token(TokenType.LeftParen, null));
                    NextChar();
                }
                else if (CurrentChar == ')')
                {
                    tokens.Add(new Token(TokenType.RightParen, null));
                    NextChar();
                }
            }
            return tokens;
        }

        private Token MakeNumber()
        {
            string Number = "";
            int DotCount = 0;

            while (CurrentChar != null && (char.IsDigit(CurrentChar.Value) || CurrentChar == '.'))
            {
                if (CurrentChar == '.')
                {
                    if (DotCount == 1)
                    {
                        break;
                    }
                    DotCount += 1;
                    Number += '.';
                }
                else
                {
                    Number += CurrentChar.Value;
                }

                NextChar();
            }

            if (DotCount == 0)
            {
                if (int.TryParse(Number, out int intValue))
                {
                    return new Token(TokenType.Int, intValue.ToString());
                }
            }
            else
            {
                if (float.TryParse(Number, out float floatValue))
                {
                    return new Token(TokenType.Float, floatValue.ToString());
                }
            }

            // Add a default return statement in case neither condition is met
            throw new UnexpectedCharacterException(CurrentChar);
        }
    }
}
