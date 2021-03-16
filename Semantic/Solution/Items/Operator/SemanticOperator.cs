using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Semantic.Solution
{
    /// <summary>
    ///     Базовый абстрактный класс всех операторов
    /// </summary>
    public abstract class SemanticOperator : SemanticObservableContainer, INotifyPropertyChanged
    {
        internal static int _counter;
        protected static readonly Stack<Branch> _returnsBranches = new();
        private int _number;
        private SemanticOperator _parent;

        protected SemanticOperator() 
            : base(SemanticCursor.Instance.CurrentTree)
        {
            _items = new Grammar().CreateItems(this).ToList();
            SelectionWord = _items.FirstOrDefault(x => x is Expression || x is Parameters || x is ISemanticType || (x is Word xWord && xWord.IsEditable));
        }

        public SemanticItem? SelectionWord { get; private set; }
        public SemanticOperator? Previous { get; internal set; }
        public SemanticOperator? Next { get; internal set; }
        public SemanticOperator? Child { get; internal set; }
        public SemanticOperator? Parent { get; internal set; }

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Number"));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void CalculateNumber()
        {
            _counter++;
            _number = _counter;
            
            if (Child != null)
            {
                Child.CalculateNumber();
            }

            if (Next != null)
            {
                Next.CalculateNumber();
            }
        }

        internal override SemanticItem Clone() => CloneSelfAndBody();

        private SemanticOperator CloneSelfAndBody()
        {
            var result = (SemanticOperator)Activator.CreateInstance(GetType());

            for (var i = 0; i < result._items.Count; ++i)
            {
                result._items[i].CopyFrom(_items[i]);
            }

            if (Child != null)
            {
                SemanticOperator child = Child.CloneNext();
                result.Child = child;
                child.Parent = result;
            }

            return result;
        }

        private SemanticOperator CloneNext()
        {
            SemanticOperator result = CloneSelfAndBody();
            if (Next != null)
            {
                SemanticOperator next = Next.CloneNext();
                result.Next = next;
                next.Previous = result;
            }

            return result;
        }

        public static SemanticOperator CreateFromType(string type)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            Type? first = types.FirstOrDefault(x => x.Name == type);
            Debug.Assert(first != null, "first != null");
            return Activator.CreateInstance(first) as SemanticOperator;
        }

        public static SemanticOperator CreateFromKeyword(string keyword)
        {
            SemanticOperator result = null;

            if (keyword == SyntaxNames.Module)
            {
                result = new Module();
                ((Module)result).NameWord.SetText(SemanticCursor.Instance.CurrentTree.Name);
                ((Module)result).EndNameWord.SetText(SemanticCursor.Instance.CurrentTree.Name);
            }
            if (keyword == SyntaxNames.If)
            {
                result = new If();
            }
            if (keyword == SyntaxNames.Beginning)
            {
                result = new Beginning();
            }
            if (keyword == SyntaxNames.While)
            {
                result = new While();
            }
            if (keyword == SyntaxNames.DoNTimes)
            {
                result = new DoNTimes();
            }
            if (keyword == SyntaxNames.Loop)
            {
                result = new Do();
            }
            if (keyword == SyntaxNames.Type)
            {
                result = new Record();
            }
            if (keyword == SyntaxNames.Function)
            {
                result = new Function();
            }
            if (keyword == SyntaxNames.Procedure)
            {
                result = new Procedure();
            }
            if (keyword == SyntaxNames.Variable)
            {
                result = new Variable();
            }
            if (keyword == SyntaxNames.InitializedVariable)
            {
                result = new VariableWithInit();
            }
            if (keyword == SyntaxNames.Return)
            {
                result = new Return();
            }
            if (keyword == SyntaxNames.Call)
            {
                result = new Call();
            }
            if (keyword == SyntaxNames.Assign)
            {
                result = new Assign();
            }
            if (keyword == SyntaxNames.Constant)
            {
                result = new Constant();
            }
            if (keyword == SyntaxNames.Output)
            {
                result = new Output();
            }
            if (keyword == SyntaxNames.Input)
            {
                result = new Input();
            }
            if (keyword == SyntaxNames.Field)
            {
                result = new Field();
            }
            if (keyword == SyntaxNames.MethodFunction)
            {
                result = new MethodFunction();
            }
            if (keyword == SyntaxNames.MethodProcedure)
            {
                result = new MethodProcedure();
            }
            // Todo: Сделать класс комментария
            if (keyword == SyntaxNames.KeyWordText)
            {
                result = new Comment();
            }
            if (keyword == SyntaxNames.Import)
            {
                result = new Import();
            }
            if (keyword == SyntaxNames.Delete)
            {
                result = new Delete();
            }

            return result;
        }

        internal override void Load(string text) => throw new NotImplementedException();

        internal override void Scan(bool scanNext)
        {
            if (Child != null)
            {
                Child.Scan(true);
            }

            if (Next != null && scanNext)
            {
                Next.Scan(true);
            }
        }

        internal void DeleteErrors()
        {
            for (var i = 0; i < _errors.Count; ++i)
            {
                SemanticFind find = _errors[i];
                if (find.Operator == this)
                {
                    _errors.Remove(find);
                    --i;
                }
            }

            var stack = new Stack<SemanticOperator>();
            if (Child != null)
            {
                stack.Push(Child);
            }

            if (this is If @if)
            {
                if (@if.ElseIfList.Any())
                {
                    stack.Push(@if.ElseIfList.First());
                }
            }

            while (stack.Count != 0)
            {
                SemanticOperator @operator = stack.Pop();
                for (var i = 0; i < _errors.Count; ++i)
                {
                    SemanticFind find = _errors[i];
                    if (find.Operator == @operator)
                    {
                        _errors.Remove(find);
                        --i;
                    }
                }

                if (@operator.Next != null)
                {
                    stack.Push(@operator.Next);
                }

                if (@operator.Child != null)
                {
                    stack.Push(@operator.Child);
                }

                if (@operator is If @iff)
                {
                    if (@iff.ElseIfList.Any())
                    {
                        stack.Push(@iff.ElseIfList.First());
                    }
                }
            }
        }

        internal void RescanParent()
        {
            if (Tree.Project.IsLoaded)
            {
                _rescannedOperators.Clear();

                if (this is Subprogram || this is Beginning)
                {
                    _rescannedOperators.Add(this);
                }
                else if (this is Record record)
                {
                    _rescannedOperators.Add(record);
                    foreach (Word word in record.Usages)
                    {
                        if (!(word._parent is ClassParameter))
                        {
                            word.Changed();
                        }
                    }
                }
                else if (this is Module module)
                {
                    _rescannedOperators.Add(module);
                    foreach (Word word in module.Usages)
                    {
                        word.Changed();
                    }
                }
                else if (this is Root root)
                {
                    _rescannedOperators.Add(Tree.Project);
                }

                Project? project = _rescannedOperators.OfType<Project>().FirstOrDefault();

                if (project != null)
                {
                    project.Rescan();
                    return;
                }

                for (var i = 0; i < _rescannedOperators.Count; ++i)
                {
                    for (var j = i + 1; j < _rescannedOperators.Count; ++j)
                    {
                        var operator1 = (SemanticOperator)_rescannedOperators.ElementAt(i);
                        var operator2 = (SemanticOperator)_rescannedOperators.ElementAt(j);

                        if (operator1.IsChild(operator2))
                        {
                            _rescannedOperators.Remove(operator1);
                            j = i;
                        }
                        else if (operator2 == operator1 || operator2.IsChild(operator1))
                        {
                            _rescannedOperators.Remove(operator2);
                            --j;
                        }
                    }
                }

                foreach (SemanticOperator @operator in _rescannedOperators)
                {
                    @operator.Scan(false);
                }

                _scannerObserver?.ScanCompleted(_errors);
            }
        }

        internal override void ManageLinks()
        {
            Words.ForEach(x => x.ManageLinks());
            if (Child != null)
            {
                Child.ManageLinks();
            }

            if (Next != null)
            {
                Next.ManageLinks();
            }
        }

        internal override void Changed()
        {
            SemanticOperator parent = FindINameableParent();
            if (parent is Subprogram || parent is Beginning)
            {
                _rescannedOperators.Add(parent);
            }
            else if (parent is Record record)
            {
                _rescannedOperators.Add(parent);
                foreach (Word word in record.Usages)
                {
                    if (!word.Operator.IsChild(parent))
                    {
                        word.Changed();
                    }
                }
            }
            else if (parent is Module module)
            {
                _rescannedOperators.Add(parent);
                foreach (Word word in module.Usages)
                {
                    word.Changed();
                }
            }
            else if (this is Module)
            {
                _rescannedOperators.Add(Tree.Project);
            }
            else
            {
                _rescannedOperators.Add(this);
            }
        }

        public static SemanticOperator FindINameableParent()
        {
            SemanticOperator? @operator = null;
            do
            {
                @operator = @operator?.FindParent();
            } while (@operator != null && !(@operator is Root) && !(@operator is INameable) && !(@operator is Beginning));
            
            return @operator;
        }

        public virtual void CheckControlFlow()
        {
            if (_returnsBranches.Count > 0 && _returnsBranches.First().HasReturn)
            {
                GenerateFind(new UnreachableCode(this, Words));
            }

            if (Child != null)
            {
                Child.CheckControlFlow();
            }

            if (Next != null)
            {
                Next.CheckControlFlow();
            }
        }

        internal override string Save() => throw new NotImplementedException();

        internal override void CopyFrom(SemanticItem item) => throw new NotImplementedException();

        public virtual List<string> ToOpsString()
        {
            var opsString = new List<string>();

            if (!(this is Root) && !(this is NullOperator))
            {
                opsString.Add(ItemName);
            }

            if (Child != null)
            {
                opsString.AddRange(Child.ToOpsString());
            }

            if (Next != null)
            {
                opsString.AddRange(Next.ToOpsString());
            }

            return opsString;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual SemanticOperator FindParent() => _parent;

        internal void SetParent(SemanticOperator parent, bool setNext = false)
        {
            _parent = parent;
            if (setNext)
            {
                SemanticOperator current = this;
                while (current.Next != null)
                {
                    current = current.Next;
                    current._parent = parent;
                }
            }
        }

        public virtual SemanticItem GetLastOperator(SemanticOperator @operator)
        {
            SemanticOperator current = Child;
            if (current != null && current != @operator)
            {
                if (this is MultiLineOperator multiLine && multiLine.IsCollapsed)
                {
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }

                    return current.GetLastOperator(@operator);
                }
            }

            return this;
        }

        public bool IsChild(SemanticOperator @operator)
        {
            SemanticOperator? parent = this;
            do
            {
                parent = parent?.FindParent();
            } while (parent != null && parent != @operator);

            return parent == @operator;
        }

        protected class Branch
        {
            public SemanticOperator SemanticOperator { get; set; }
            public bool HasReturn { get; set; }
        }
    }
}