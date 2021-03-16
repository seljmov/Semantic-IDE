using System.Collections;

namespace Semantic.Solution
{
    public class CursorSubject
    {
        protected ArrayList _observers = new ArrayList();

        public void NotifyObservers()
        {
            foreach (ICursorObserver observer in _observers)
            {
                observer.Update();
            }
        }

        public void RegisterObserver(ICursorObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void DeleteObserver(ICursorObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}