using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Record : MultiLineOperator, IHaveType, IEndNameable
    {
        private static readonly List<SemanticOperator> _members = new List<SemanticOperator>();

        public Record()
        {

        }

        public Word VisibilityWord { get; private set; }
        public Word NameWord { get; private set; }
        public Word EndNameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }

        public SemanticItem TypeWord { get; set; }

        public override string ItemName => SyntaxNames.Type;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();

        private static void FindMember(SemanticOperator @operator)
        {
            if (@operator is IField)
            {
                _members.Add(@operator);
            }

            if (@operator.Next != null)
            {
                FindMember(@operator.Next);
            }
        }

        public List<SemanticOperator> GetMembers()
        {
            _members.Clear();
            if (Child != null)
            {
                FindMember(Child);
            }

            string parentType = ((ISemanticType)TypeWord).FullType;
            if (parentType != "Object" && parentType != "Объект")
            {

            }

            return _members;
        }
    }
}