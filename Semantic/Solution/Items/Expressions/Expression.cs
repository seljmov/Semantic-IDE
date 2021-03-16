using System;
using System.Collections.Generic;

namespace Semantic.Solution
{
    public class Expression : SemanticEditableContainer
    {
        public Expression(SemanticItem item) : base(item)
        {
            _items.Add(new Token(this, "", ETokenType.Operand));
            BuildTree();
        }

        public List<IToken> ReverseNotation { get; private set; }
        public ScanToken ExpressionTree { get; private set; }

        public override string ItemName => Strings.Expression;

        public void BuildTree()
        {
            
        }

        public override SemanticItem GetNextItem(SemanticItem currentItem)
        {
            throw new NotImplementedException();
        }

        public override SemanticItem GetPreviousItem(SemanticItem currentItem)
            => throw new NotImplementedException();

        internal override void AddItem(SemanticItem item)
        {
            throw new NotImplementedException();
        }

        internal override void Changed() => _parent.Changed();

        internal void InsertTokenAsNext(Token newToken, Token prevToken) { }
            // => Tree.Add

        internal override SemanticItem Clone()
        {
            throw new NotImplementedException();
        }

        internal override void CopyFrom(SemanticItem item)
        {
            throw new NotImplementedException();
        }

        internal override void InsertItem(int index, SemanticItem item)
        {
            throw new NotImplementedException();
        }

        internal override void Load(string text)
        {
            throw new NotImplementedException();
        }

        internal override void ManageLinks()
        {
            throw new NotImplementedException();
        }

        internal override void RemoveItem(SemanticItem item)
        {
            throw new NotImplementedException();
        }

        internal ISemanticType GetSemanticType() => ExpressionTree.GetSemanticType();

        internal bool IsLValue() => ExpressionTree.IsLValue();

        public bool IsSungleString()
        {
            if (ItemsCount == 1)
            {
                var first = (Token)FirstItem();
                return StringAnalyzer.IsString(first.Text);
            }
            return false;
        }

        internal override string Save()
        {
            throw new NotImplementedException();
        }

        internal override void Scan(bool scanNext) => throw new NotImplementedException();
    }
}