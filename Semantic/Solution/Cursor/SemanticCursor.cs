using System.Collections.Generic;

namespace Semantic.Solution
{
    public class SemanticCursor : CursorSubject, INavigation
    {
        private static SemanticCursor _instance;
        private static readonly Dictionary<ETokenType, ETokenCursor> _tokenCursors = new Dictionary<ETokenType, ETokenCursor>()
        {
            {ETokenType.Operand, ETokenCursor.Both },
            {ETokenType.Function, ETokenCursor.Both },
            {ETokenType.Array, ETokenCursor.Both },
            {ETokenType.UnaryOperation, ETokenCursor.Start },
            {ETokenType.BinaryOperation, ETokenCursor.Both },
            {ETokenType.LeftBracket, ETokenCursor.Start },
            {ETokenType.Comma, ETokenCursor.End },
            {ETokenType.RightBracket, ETokenCursor.End },
            {ETokenType.RightFuncBracket, ETokenCursor.End },
            {ETokenType.RightIndex, ETokenCursor.End },
        };

        private INavigation _navigation;

        private SemanticCursor()
        {
            _navigation = new TextNavigation();
        }

        public SemanticItem CurrentItem { get; internal set; }
        public SemanticTree CurrentTree { get; protected internal set; }
        public SemanticOperator CurrentOperator { get; internal set; }
        public int Offset { get; internal set; }
        public SemanticItem OldItem { get; internal set; }
        public bool IsInterrupted { get; internal set; }
        public bool IsTextBody { get; internal set; }

        public static SemanticCursor Instance => _instance ??= new SemanticCursor();

        public bool IsSemanticNavigation => _navigation is SemanticNavigation;

        public SemanticItem GetUpItem() => _navigation.GetUpItem();

        public SemanticItem GetDownItem() => _navigation.GetDownItem();

        public SemanticItem GetLeftItem() => _navigation.GetLeftItem();

        public SemanticItem GetRightItem() => _navigation.GetRightItem();

        public void MoveUp() => _navigation.MoveUp();

        public void MoveUpFast() => _navigation.MoveUpFast();

        public void MoveDown() => _navigation.MoveDown();

        public void MoveDownFast() => _navigation.MoveDownFast();

        public void MoveLeft(bool fast = false) => _navigation.MoveLeft(fast);

        public void MoveRight(bool fast = false) => _navigation.MoveRight(fast);

        public void ShiftTab() => _navigation.ShiftTab();

        public void Tab() => _navigation.Tab();

        public void SetSemanticNavigation() => _navigation = new SemanticNavigation();

        public void SetTextNavigation() => _navigation = new TextNavigation();

        public bool IsFullSelected() => Offset == -1;

        public void Focus(SemanticItem item, bool isInterrupted = false, bool isTextBody = false)
        {
            OldItem = CurrentItem;
            CurrentItem = item;
            if (CurrentItem is SemanticOperator)
            {
                CurrentOperator = (SemanticOperator) CurrentItem;
            }
            else
            {
                CurrentOperator = CurrentItem.Operator;
            }
        }

        public void SetOffset(int offset)
        {
            Offset = offset;
            NotifyObservers();
        }
        
        internal void SetTree(SemanticTree semanticTree) => CurrentTree = semanticTree;
    }
}