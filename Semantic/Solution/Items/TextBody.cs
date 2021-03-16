using System.Collections.Generic;
using JetBrains.Annotations;

namespace Semantic.Solution
{
    public class TextBody : SimpleItem
    {
        public TextBody(SemanticItem parent) 
            : base(parent) { }

        public string Rtf { get; set; } 
            
        public override string ItemName => SyntaxNames.Comment;
        public override List<Word> Words => new();
        
        internal override void ManageLinks() => throw new System.NotImplementedException();
        
        internal override SemanticItem Clone() => throw new System.NotImplementedException();
        
        internal override void CopyFrom(SemanticItem item) => throw new System.NotImplementedException();
        
        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
        
        internal override void Changed() => throw new System.NotImplementedException();

        internal override string Save()
        {
            NotifyObservers(EObserverHint.CommentSaving);
            return Rtf;
        }

        internal override void Load(string text) => Rtf = text;

        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, 
            bool printNext = false, int tabLevel = 0)
        {
            NotifyObservers(EObserverHint.CommentSaving);
            return "Rtf null";
        }
    }
}