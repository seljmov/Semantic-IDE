using System;
using System.Linq;

namespace Semantic.Solution
{
    public class PointerType : SemanticObservableContainer, ISemanticType, IHaveType
    {
        public PointerType(SemanticItem parent)
            : base(parent)
        {

        }

        public override string ItemName { get; }
        internal override SemanticItem Clone()
        {
            throw new NotImplementedException();
        }

        internal override void CopyFrom(SemanticItem item)
        {
            throw new NotImplementedException();
        }

        internal override void Scan(bool scanNext)
        {
            throw new NotImplementedException();
        }

        internal override void Changed()
        {
            throw new NotImplementedException();
        }

        internal override string Save()
        {
            throw new NotImplementedException();
        }

        internal override void Load(string text)
        {
            throw new NotImplementedException();
        }

        public bool IsUserType { get; }
        public string FullType { get; }
        public bool CanCastTo(ISemanticType semanticType, bool allowIntToReadl)
        {
            throw new NotImplementedException();
        }

        public SemanticItem TypeWord { get; set; }
    }
}