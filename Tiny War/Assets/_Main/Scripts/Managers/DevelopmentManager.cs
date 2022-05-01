using System;

using Zenject;
using UniRx;

namespace TinyWar
{
    public partial class DevelopmentManager
    {
        #region FIELDS

        [Inject] private TickManager _tickManager = null;

        private int _developmentPoints = default;
        private Subject<int> _developmentPointsSubject = new Subject<int>();
        private DisposableList _disposables = new DisposableList();

        public IObservable<int> DevelopmentPointsChangeObservable => _developmentPointsSubject.AsObservable();

        #endregion

        #region METHODS

        public void IncreasePoints(int amount)
        {
            _developmentPoints += amount;
        }

        private void TriggerDevelopmentPointsChange()
        {
            _developmentPointsSubject.OnNext(_developmentPoints);
        }

        #endregion
    }

    public partial class DevelopmentManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            _tickManager.OnPostTickObservable.Subscribe(TriggerDevelopmentPointsChange).AddTo(_disposables);
        }
    }

    public partial class DevelopmentManager : IDisposable
    {
        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}
