using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ArrayToken : ScanToken
    {
        public ArrayToken(Token token, ScanToken array, List<ScanToken> indexes) 
            : base(token)
        {
            Array = array;
            Indexes = indexes;
            Array.Parent = this;
        }

        public ScanToken Array { get; private set; }
        public List<ScanToken> Indexes { get; private set; }

        protected internal override List<Word> Words
        {
            get
            {
                var words = new List<Word>();
                words.AddRange(Array.Words);
                foreach (var token in Indexes)
                {
                    words.AddRange(token.Words);
                }

                return words;
            }
        }
        
        public override ISemanticType GetSemanticType()
        {
            var type = Array.GetSemanticType();
            if (type is ArrayType arrayType)
            {
                const int formalCount = 1;
                var actualCount = Indexes.Count;
                for (var i = 0; i < actualCount; ++i)
                {
                    var actualType = Indexes[i].GetSemanticType();
                    if (i < formalCount)
                    {
                        if (!actualType.Equals(SimpleType.Integer) && !actualType.Equals(SimpleType.Undefined))
                        {
                            GenerateFind(new IndexHasIncorrectType(Token.Operator, Indexes[i].Words, 
                                SyntaxNames.Integer, actualType.FullType));
                        }
                    }
                }

                if (actualCount != formalCount)
                {
                    GenerateFind(new IncorrectIndexesCount(Token.Operator, Array.Words, actualCount, formalCount));
                    return SimpleType.Undefined;
                }

                return (ISemanticType) arrayType.TypeWord;
            }

            if (!type.Equals(SimpleType.Undefined))
            {
                GenerateFind(new NotArray(Token.Operator, Array.Words, type.FullType));
            }
            
            return SimpleType.Undefined;
        }
    }
}