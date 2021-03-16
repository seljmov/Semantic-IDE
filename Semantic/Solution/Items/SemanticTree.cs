using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Xml;

namespace Semantic.Solution
{
    public class SemanticTree : SemanticItem, IProjectItem
    {
        private readonly CommandHistory _history = new();
        public Guid Id;
        public SemanticTree(string name, Project project) 
            : base(null) 
        {
            Name = name;
            Project = project;
        }

        public SemanticTree(string name, Project project, SemanticTree testedTree)
            : base(null)
        {
            Name = name;
            Project = project;
            testedTree.TestModule = this;
            ImplementModule = testedTree;
        }

        public SemanticOperator? Root { get; private set; }
        public Project Project { get; private set; }
        public SemanticTree? ImplementModule { get; private set; }
        public SemanticTree? TestModule { get; private set; }

        internal bool IsStartup { get; set; }
        public string Version { get; set; }
        public string Name { get; internal set; }
        public bool IsSystem { get; set; }
        public bool IsTest => ImplementModule != null;

        public IProjectItem Parent { get; set; }

        public override string ItemName => Strings.SemanticTree;
        public override List<Word> Words => null;
        public override string Signature => "";

        public IEnumerable<SemanticTree> GetModules(Func<SemanticTree, bool> predicate)
            => predicate(this) ? new List<SemanticTree> { this } : new List<SemanticTree>();

        public void AddItem(IProjectItem item) { }
        public void RemoveItem(IProjectItem item) { }

        internal override void ManageLinks() { }

        internal override void Scan(bool scanNext)
        {
            if (Root != null)
            {
                Root.Scan(scanNext);
            }
        }

        public void Sort() { }

        internal override void Changed()
        {
            var module = FindModule();
            if (module != null)
            {
                module.Changed();
            }
        }

        public Module? FindModule()
        {
            SemanticOperator current = Root.Child;
            while (current != null && !(current is Module))
            {
                current = current.Next;
            }

            return (Module?)current;
        }

        internal void SaveChanges()
        {
            _history.Save();
        }

        // Todo: написать данные функции
        public void UndoMacroCommand() { }
        
        public void RedoMacroCommand() { }

        public static SemanticOperator GetPlaceForGlobalVariable(SemanticOperator @operator)
        {
            SemanticOperator current = @operator;
            while (!(current is Function) && !(current is Procedure) && !(current is Beginning) && !(current is Module))
            {
                current = current.Previous ?? current.Parent;
            }

            return (current is Beginning) ? current.Tree.FindModule() : current;
        }

        internal void BuildDefaultTree()
        {
            SemanticCursor.Instance.SetTree(this);
            var root = new Root();
            InsertRoot(root);
            var nullOperator = new NullOperator();
            nullOperator.KeyWord.SetText("");
            InsertOperatorAsChild(nullOperator, root);
            _history.Clear();
            NotifyObservers(EObserverHint.TreeLoaded);
        }

        private void InsertRoot(SemanticOperator newOperator) => Root = newOperator;

        // Todo: написать данные функции
        internal void InsertOperatorAsPrevious(SemanticOperator @operator, SemanticOperator prevOperator) { }

        internal void InsertOperatorAsNext(SemanticOperator @operator, SemanticOperator nextOperator) { }
        
        internal void InsertOperatorAsChild(SemanticOperator @operator, SemanticOperator parentOperator) { }
        
        internal void ReplaceOperator(SemanticOperator @operator, SemanticOperator oldOperator) { }
        
        internal void DeleteOperator(SemanticOperator @operator) { }

        internal void AddCommandAndExecute(Command command)
        {

        }

        internal override SemanticItem Clone() => throw new NotImplementedException();

        internal override void CopyFrom(SemanticItem item) => throw new NotImplementedException();

        public override SemanticItem GetPreviousItem(SemanticItem currentItem) 
        {
            var currentOperator = (SemanticOperator)currentItem;
            SemanticOperator prevItem = currentOperator.Previous ?? currentOperator.Parent;

            return  !(prevItem is Root) && prevItem != null
                    ? prevItem.GetLastOperator(currentOperator)
                    : null;
        }
        
        public override SemanticItem GetNextItem(SemanticItem currentItem) 
        {
            var @operator = (SemanticOperator)currentItem;

            if (@operator.Child != null && !(@operator is MultiLineOperator lineOperator && lineOperator.IsCollapsed))
            {
                return @operator.Child;
            }

            if (@operator is Module module && ((MultiLineOperator) @operator).IsCollapsed)
            {
                if (module.Beginning != null)
                {
                    return module.Beginning;
                }
            }
            else if (@operator is If @if && ((MultiLineOperator) @operator).IsCollapsed)
            {
                if (@if.ElseIfList.Count > 0)
                {
                    return @if.ElseIfList.First();
                }

                if (@if.Else != null)
                {
                    return @if.Else;
                }
            }

            if (@operator.Next != null)
            {
                return @operator.Next;
            }

            return GetParentNextOperator(@operator);
        }

        private static SemanticOperator GetParentNextOperator(SemanticOperator @operator)
        {
            SemanticOperator parent = @operator.FindParent();
            if (parent != null)
            {
                if (parent is Module module)
                {
                    if (module.Beginning != null && module.Beginning != @operator)
                    {
                        return module.Beginning;
                    }
                }
                else if (parent is If @if)
                {
                    if (@if.ElseIfList.Count > 0 && !@if.ElseIfList.Contains(@operator) && @if.Else != @operator)
                    {
                        return @if.ElseIfList.First();
                    }

                    if (@if.Else != null && @if.Else != @operator)
                    {
                        return @if.Else;
                    }
                }

                if (parent.Next != null)
                {
                    return parent.Next;
                }

                return GetParentNextOperator(parent);
            }

            return null;
        }

        public void SaveTree() => Save();

        internal override string Save()
        {
            var buffer = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(buffer);
            writer.WriteStartDocument();
            SaveItem(Root, writer);
            writer.WriteEndDocument();
            writer.Close();
            return buffer.ToString();
        }

        private void SaveItem(SemanticOperator @operator, XmlWriter writer)
        {
            writer.WriteStartElement(@operator.GetType().Name);
            foreach (PropertyInfo info in @operator.GetType().GetProperties())
            {
                if (info.Name != "Item" && info.Name != "SelectionWord" && info.Name != "EndNamwWord")
                {
                    var item = info.GetValue(@operator, Array.Empty<object>()) as SemanticItem;
                    if (item != null && !(item is SemanticTree) && !(item is SemanticOperator))
                    {
                        writer.WriteAttributeString(info.Name, item.Save());
                    }
                }
            }

            if (@operator is MultiLineOperator multiLine)
            {
                if (multiLine.IsCollapsed)
                {
                    writer.WriteAttributeString("IsCollapsed", "True");
                }
            }

            if (@operator is Root)
            {
                writer.WriteAttributeString("Id", Id.ToString());
                writer.WriteAttributeString("Version", "1.2");
                writer.WriteAttributeString("TestModule", @operator.Tree.TestModule == null ? "" : @operator.Tree.TestModule.Name);
                writer.WriteAttributeString("ImplementModule", @operator.Tree.ImplementModule == null ? "" : @operator.Tree.ImplementModule.Name);
            }

            if (@operator.Child != null)
            {
                SaveItem(@operator.Child, writer);
            }

            writer.WriteFullEndElement();

            if (@operator is If @if)
            {
                if (@if.ElseIfList.Count > 0)
                {
                    SaveItem(@if.ElseIfList.First(), writer);
                }
                else if (@if.Else != null)
                {
                    SaveItem(@if.Else, writer);
                }
            }

            if (@operator is Module module)
            {
                if (module.Beginning != null)
                {
                    SaveItem(module.Beginning, writer);
                }
            }

            if (@operator.Next != null)
            {
                SaveItem(@operator.Next, writer);
            }
        }

        internal override void Load(string text)
        {
            SemanticCursor.Instance.SetTree(this);
            var document = new XmlDocument();
            document.LoadXml(text);
            var nodes = new Stack<LoadPair>();
            nodes.Push(new LoadPair(Root, document.ChildNodes.OfType<XmlElement>().First(), false));
            Id = Guid.Empty;
            Version = "1.0";

            while (nodes.Any())
            {
                LoadPair pair = nodes.Pop();
                XmlNode node = pair.Node;
                SemanticOperator newOperator = SemanticOperator.CreateFromType(node.Name);

                if (node.Attributes != null)
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (newOperator is Root)
                        {
                            switch (attribute.Name)
                            {
                                case "Id":
                                    try
                                    {
                                        Id = Guid.Parse(attribute.Value);
                                    } 
                                    catch
                                    {
                                        Id = Guid.NewGuid();
                                    }
                                    break;
                                case "Version":
                                    Version = attribute.Value;
                                    break;
                                case "TestModule":
                                    string testModuleName = attribute.Value;
                                    if (Project.HasModuleWithName(testModuleName))
                                    {
                                        SemanticTree testModule = Project.GetModuleByName(testModuleName);
                                        testModule.ImplementModule = this;
                                        TestModule = testModule;
                                    }
                                    break;
                                case "ImplementModule":
                                    string implementModuleName = attribute.Value;
                                    if (Project.HasModuleWithName(implementModuleName))
                                    {
                                        SemanticTree implementModule = Project.GetModuleByName(implementModuleName);
                                        implementModule.TestModule = this;
                                        ImplementModule = implementModule;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if (newOperator is MultiLineOperator multiLine && attribute.Value == "IsCollapsed")
                            {
                                if (attribute.Value == "True")
                                {
                                    multiLine.IsCollapsed = true;
                                }
                            }
                            else
                            {
                                PropertyInfo? info = newOperator.GetType().GetProperty(attribute.Name);
                                var item = info?.GetValue(newOperator, Array.Empty<object>()) as SemanticItem;
                                if (item != null)
                                {
                                    var value = attribute.Value;
                                    if (info?.Name != "TypeWord")
                                    {
                                        item.Load(value);
                                    }
                                    else
                                    {
                                        newOperator.LoadType(value);
                                    }
                                }
                            }
                        }
                    }
                }

                if (newOperator is MultiLineOperator multiLine1 && IsSystem
                    && !(newOperator is Root) && !(newOperator is Module) && !(newOperator is Record))
                {
                    multiLine1.IsCollapsed = true;
                }

                var @operator = pair.Operator;
                if (newOperator is Root)
                {
                    InsertRoot(newOperator);
                }
                else if (newOperator is Beginning beginning)
                {
                    ((Module)@operator).SetBeginning(beginning);
                }
                else if (newOperator is ElseIf elseIf)
                {
                    ((If)@operator).AddElseIf(elseIf);
                }
                else if (newOperator is Else @else)
                {
                    ((If)@operator).AddElse(@else);
                }
                else if (pair.AsChild)
                {
                    InsertOperatorAsChild(newOperator, @operator);
                }    
                else
                {
                    InsertOperatorAsNext(newOperator, @operator);
                }

                if (node.NextSibling != null)
                {
                    if (newOperator is Beginning || newOperator is ElseIf || newOperator is Else)
                    {
                        nodes.Push(new LoadPair(@operator, node.NextSibling, false));
                    }
                    else
                    {
                        nodes.Push(new LoadPair(newOperator, node.NextSibling, false));
                    }
                }

                if (node.HasChildNodes)
                {
                    nodes.Push(new LoadPair(newOperator, node.FirstChild, true));
                }
            }

            _history.Clear();
        }

        public List<Record> GetDeclaredTypes()
        {
            Module? module = FindModule();
            return module != null ? module.GetDeclaredTypes() : new();
        }

        public List<Import> GetImports()
        {
            Module? module = FindModule();
            return module != null ? module.GetImports() : new();
        }

        public List<SemanticOperator> GetMembers()
        {
            Module? module = FindModule();
            return module != null ? module.GetMembers() : new();
        }

        public List<string> ToOpsString() => Root.ToOpsString();

        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, bool printNext = false, int tabLevel = 0)
            => Root.PrettyPrinter(mode);

        private class LoadPair
        {
            public LoadPair(SemanticOperator @operator, XmlNode node, bool asChild)
            {
                Operator = @operator;
                Node = node;
                AsChild = asChild;
            }

            public SemanticOperator Operator { get; private set; }
            public XmlNode Node { get; private set; }
            public bool AsChild { get; private set; }
        }
    }
}