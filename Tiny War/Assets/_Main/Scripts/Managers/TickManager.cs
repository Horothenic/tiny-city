using UnityEngine;
using Zenject;
using System;
using UniRx;

namespace TinyWar
{
    public partial class TickManager
    {
        #region FIELDS

        [Inject] private StatsContainer _statsContainer = null;

        private Subject<Unit> _onPreTickSubject = new Subject<Unit>();
        private Subject<Unit> _onTickSubject = new Subject<Unit>();
        private Subject<Unit> _onPostTickSubject = new Subject<Unit>();

        public IObservable<Unit> OnPreTickObservable => _onPreTickSubject.AsObservable();
        public IObservable<Unit> OnTickObservable => _onTickSubject.AsObservable();
        public IObservable<Unit> OnPostTickObservable => _onPostTickSubject.AsObservable();

        #endregion

        #region METHODS

        private void TriggerTick()
        {
            _onPreTickSubject.OnNext(Unit.Default);
            _onTickSubject.OnNext(Unit.Default);
            _onPostTickSubject.OnNext(Unit.Default);
        }

        #endregion
    }

    public partial class TickManager : ITickable
    {
        private float _timeElapsed = 0;

        void ITickable.Tick()
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= _statsContainer.TickTime)
            {
                _timeElapsed -= _statsContainer.TickTime;
                TriggerTick();
            }
        }
    }
}
