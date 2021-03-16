using System.Collections.Generic;

namespace Semantic.Solution
{
    public abstract class ScanToken
    {
        public ScanToken(Token token)
        {
            Token = token;
            Token.ScanToken = this;
        }

        public Token Token { get; private set; }
        public ScanToken Parent { get; internal set; }

        protected internal abstract List<Word> Words { get; }
        public bool IsRecord { get; private set; }

        protected static void GenerateFind(SemanticFind find) => SemanticItem.GenerateFind(find);

        private static ProcedureType CreateProcedureType(SemanticOperator procedure)
            => new(procedure.SelectionWord);

        protected static ISemanticType GetSubprogramType(Subprogram subprogram)
        {
            return  (subprogram is IHaveType type)
                    ? (ISemanticType)type.TypeWord
                    : CreateProcedureType(subprogram);
        }

        protected ISemanticType GetItemType(SemanticItem item)
        {
            if (item is Subprogram subprogram)
            {
                return CreateProcedureType(subprogram);
            }

            if (item is Record record)
            {
                IsRecord = true;
                return new SimpleType(null, item.Tree.Name + "." + record.NameWord.Text, EItemType.General);
            }

            if (item is IHaveType type)
            {
                return (ISemanticType)type.TypeWord;
            }

            return SimpleType.Undefined;
        }

        public abstract ISemanticType GetSemanticType();

        // Left? :)
        public bool IsLValue()
        {
            if (this is DotToken
                || this is ArrayToken
                || this is OperandToken token && !StringAnalyzer.IsConstantValue(token.Name))
            {
                if (this is OperandToken operandToken)
                {
                    Token.AddInitializedName(operandToken.Name);
                }

                return true;
            }

            return false;
        }
    }
}