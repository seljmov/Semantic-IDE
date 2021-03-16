namespace Semantic.Solution
{
    public static class UnaryOperationTokenFactory
    {
        public static UnaryOperationToken CreateToken(Token token, ScanToken operand, string text)
        {
            if (text.Equals(SyntaxNames.UnaryMinusOperation))
            {
                return new UnaryMinusToken(token, operand);
            }
            else if (text.Equals(SyntaxNames.NotOperation))
            {
                return new NotToken(token, operand);
            }

            return null;
        }
    }
}