using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class SemanticSubject
    {
        private readonly List<ISemanticObserver> _observers = new List<ISemanticObserver>();

        public object Observer => _observers.FirstOrDefault();

        internal void NotifyObservers(Command command)
        {
            foreach (var observer in _observers)
            {
                observer.Update(command);
            }
        }

        internal void NotifyObservers(EObserverHint hint)
        {
            foreach (var observer in _observers)
            {
                observer.Update(hint);
            }
        }

        public void RegisterObserver(ISemanticObserver observer)
        {
            _observers.Clear();
            _observers.Add(observer);
        }

        public void DeleteObserver(ISemanticObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}