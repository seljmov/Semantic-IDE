using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaEdit.Utils;
using DynamicData;

namespace Semantic.Solution
{
    public class TypeParameter : SemanticObservableContainer, IHaveType
    {
        public TypeParameter(SemanticItem parameters, string mode, bool isLast)
            : base(parameters)
        {
            _items = new Grammar().CreateItems(this);
            ModeWord = (Word) _items.First(x => x.HasType(EItemType.ModeModifier));
            TypeWord = _items.First(x => x.HasType(EItemType.Type));
            ModeWord.SetText(mode);
            _items.Add(new Word(this, ", ", EItemType.Body));
        }

        public Word ModeWord { get; set; }
        public SemanticItem TypeWord { get; set; }
        public bool IsLast { get; protected set; }
        public override string ItemName { get; }
        public Parameters Parameters => _parent as Parameters;

        public override string Signature
            => !ModeWord.Text.Equals(SyntaxNames.In)
                    ? ModeWord.Text + " " + ((ISemanticType) TypeWord).FullType
                    : ((ISemanticType) TypeWord).FullType;

        internal override SemanticItem Clone()
        {
            var parameter = new TypeParameter(null, ModeWord.Text, IsLast) {_items = _items};
            parameter._items.Clear();
            foreach (var item in _items)
            {
                var newItem = item.Clone();
                parameter._items.Add(newItem);
                newItem._parent = parameter;
            }

            parameter.ModeWord = (Word) parameter._items.First(x => x.HasType(EItemType.ModeModifier));
            parameter.TypeWord = parameter._items.First(x => x.HasType(EItemType.Type));
            return parameter;
        }

        internal override void CopyFrom(SemanticItem item)
        {
            var parameter = (TypeParameter) item;
            IsLast = parameter.IsLast;
            _items.Clear();

            for (var i = 0; i < parameter._items.Count; ++i)
            {
                _items.Add(parameter._items[i] is SimpleType
                    ? new SimpleType(this, "", EItemType.General)
                    : new Word(this, "", EItemType.General));
                _items[i].CopyFrom(parameter._items[i]);
            }

            ModeWord = (Word) _items.First(x => x.HasType(EItemType.ModeModifier));
            TypeWord = _items.First(x => x.HasType(EItemType.Type));
        }

        internal override void Scan(bool scanNext) => TypeWord.Scan(scanNext);

        internal override void Changed() => _parent.Changed();

        internal void MakeLast()
        {
            SemanticItem last = _items.Last();
            _items.Remove(last);
            IsLast = true;
        }

        internal void MakeUnLast()
        {
            _items.Add(new Word(this, ", ", EItemType.General | EItemType.Body));
            IsLast = false;
        }
        
        internal override string Save()
        {
            const char typeParameterSplitter = '©';
            const char partsSplitter = '¤';
            
            var wordText = "";
            wordText += ModeWord.Save() + partsSplitter;
            wordText += TypeWord.Save();

            if (!IsLast)
            {
                wordText += typeParameterSplitter;
            }

            return wordText;
        }

        internal override void Load(string text)
        {
            throw new NotImplementedException();
        }
    }
}