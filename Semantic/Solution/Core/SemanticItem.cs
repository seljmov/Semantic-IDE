using System;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public abstract class SemanticItem : SemanticSubject
    {
        public static IScannerObserver _scannerObserver;
        public static List<SemanticFind> _errors = new();
        protected internal static HashSet<object> _rescannedOperators = new();
        protected internal static Stack<List<string>> _initializedNames = new();
        protected internal static Stack<List<Word>> _mustBeInitialized = new();
        protected internal EItemType _itemType;
        public SemanticItem _parent;

        protected SemanticItem(SemanticItem parent)
        {
            _parent = parent;
        }

        public abstract string ItemName { get; }

        public SemanticOperator Operator
        {
            get
            {
                if (this is SemanticTree)
                {
                    return null;
                }

                if (this is SemanticOperator)
                {
                    return (SemanticOperator) this;
                }

                SemanticItem parent = _parent;
                while (!(parent is SemanticOperator))
                {
                    parent = parent._parent;
                }

                return (SemanticOperator) parent;
            }
        }

        public abstract List<Word> Words { get; }

        public virtual SemanticTree Tree
        {
            get
            {
                if (this is SemanticOperator)
                {
                    return (SemanticTree) _parent;
                }

                if (this is SemanticTree)
                {
                    return (SemanticTree) this;
                }

                return (SemanticTree?) Operator?._parent;
            }
        }

        public virtual string Signature => ItemName;

        internal abstract void ManageLinks();

        public bool HasType(EItemType type) => (_itemType & type) == type;

        internal abstract SemanticItem Clone();

        internal abstract void CopyFrom(SemanticItem item);

        internal static void GenerateFind(SemanticFind find)
        {
            if (_errors.All(x => x.CompareTo(find) == 1))
            {
                _errors.Add(find);
            }
        }

        internal abstract void Scan(bool scanNext);

        internal void Rescan()
        {
            if (Tree.Project.IsLoaded)
            {
                _rescannedOperators.Clear();
                if (this is Expression expr)
                {
                    expr.BuildTree();
                }

                Changed();

                Project project = _rescannedOperators.OfType<Project>().FirstOrDefault();
                if (project != null)
                {
                    project.Rescan();
                    return;
                }

                for (var i = 0; i < _rescannedOperators.Count; ++i)
                {
                    for (var j = i+1; j < _rescannedOperators.Count; ++j)
                    {
                        var oper1 = (SemanticOperator)_rescannedOperators.ElementAt(i);
                        var oper2 = (SemanticOperator)_rescannedOperators.ElementAt(j);

                        if (oper1.IsChild(oper2))
                        {
                            _rescannedOperators.Remove(oper1);
                            j = i;
                        }
                        else if (oper2 == oper1 || oper2.IsChild(oper1))
                        {
                            _rescannedOperators.Remove(oper2);
                            --j;
                        }
                    }
                }

                foreach (SemanticOperator @operator in _rescannedOperators)
                {
                    @operator.Scan(false);
                }

                _scannerObserver.ScanCompleted(_errors);
            }
        }

        internal abstract void Changed();

        public SemanticOperator? GetOperatorInSameScropeWith(Func<SemanticOperator, bool> predicate)
        {
            SemanticOperator? current = Operator?.Previous ?? Operator?.Parent;
            if (current == null)
            {
                return null;
            }

            while (!predicate.Invoke(current) && (current.Parent != null || current.Previous != null))
            {
                current = current.Previous ?? current.Parent;
            }

            return predicate.Invoke(current) ? current : null;
        }

        public List<INameable> GetAllNameInSameScope()
        {
            var names = new List<INameable>();
            var isInSubprogramme = true;
            var current = Operator;

            do
            {
                current = current.Previous ?? current.Parent;

                if (current == null)
                {
                    return null;
                }

                if (current is Subprogram subprogramme && isInSubprogramme)
                {
                    if (current is IMethod method)
                    {
                        names.Add(method.ClassParameter);
                    }

                    foreach (Parameter parameter in subprogramme.ParametersWord)
                    {
                        names.Add(parameter);
                    }

                    isInSubprogramme = false;
                }
                else if (current is Beginning)
                {
                    isInSubprogramme = false;
                }

                if (current is INameable nameable)
                {
                    names.Add(nameable);
                }
            } while (current.Previous != null || current.Parent != null);

            return names;
        }

        public INameable? GetNameInSameScope(string name) 
        {
            var isInSubprogram = true;
            if (this is ClassParameter)
            {
                foreach (Parameter parameter in ((Subprogram)Operator).ParametersWord)
                {
                    if (parameter.NameWord.Text == name)
                    {
                        return parameter;
                    }
                }

                if (Operator is INameable nameable && !(Operator is Field) && nameable.NameWord.Text == name)
                {
                    return nameable;
                }

                isInSubprogram = false;
            }

            if (this is Parameter)
            {
                Parameters parameters = ((Parameter)this).Parameters;
                for (var i = parameters.IndexOfItem(this) - 1; i >= 0; --i)
                {
                    var param = (Parameter)parameters[i];
                    if (param.NameWord.Text == name)
                    {
                        return param;
                    }
                }

                if (Operator is INameable nameable && !(Operator is Field) && nameable.NameWord.Text == name)
                {
                    return nameable;
                }

                isInSubprogram = false;
            }

            var current = Operator;
            do
            {
                if (current != null)
                {
                    current = current.Previous ?? current.Parent;
                }

                if (current == null)
                {
                    return null;
                }

                if (current is IMethod method && isInSubprogram)
                {
                    if (method.ClassParameter.NameWord.Text == name)
                    {
                        return method.ClassParameter;
                    }
                }
                if (current is Subprogram subprogramme && isInSubprogram)
                {
                    foreach (Parameter parameter in subprogramme.ParametersWord)
                    {
                        if (parameter.NameWord.Text == name)
                        {
                            return parameter;
                        }
                    }
                    isInSubprogram = false;
                }
                else if (current is Beginning)
                {
                    isInSubprogram = false;
                }

                if (Operator is INameable nameable && !(Operator is Field) && nameable.NameWord.Text == name)
                {
                    return nameable;
                }

            } while (current.Previous != null || current.Parent != null);

            return null;
        }

        public abstract SemanticItem GetPreviousItem(SemanticItem currentItem);
        public abstract SemanticItem GetNextItem(SemanticItem currentItem);

        internal abstract string Save();
        internal abstract void Load(string text);

        // TODO: write this func
        internal virtual void ChangeType(SemanticItem item)
        {
            // Tree.AddCommandAndExecute()
        }

        internal void LoadType(string typeText)
        {
            string typeBody = typeText.Substring(2, typeText.Length - 2);
            SemanticItem newType = typeText[0] switch
            {
                '1' => new SimpleType(this, typeBody, EItemType.Type),
                '2' => new ArrayType(this),
                '3' => new ProcedureType(this),
                '4' => new PointerType(this),
                _ => throw new Exception(Strings.InvalidType),
            };

            newType.Load(typeBody);
            if (this is SimpleType)
            {
                _parent.ChangeType(newType);
            }
            else
            {
                ChangeType(newType);
            }
        }

        public abstract string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, bool printNext = false, int tabLevel = 0);

        public static void AddUsages(INameable nameable, Word token)
        {
            nameable.Usages.Add(token);
            token._declarations.Add(nameable);
        }
    }
}