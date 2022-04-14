using UnityEngine;
using System;

using Zenject;
using UniRx;

namespace TinyWar
{
    public interface IBuildingDataSource
    {
        IObservable<Unit> OnPreTick { get; }
        IObservable<Unit> OnTick { get; }
        IObservable<Unit> OnPostTick { get; }

        void CreateBuilding(GridCell originCell, GridCell newCell, BuildingType type);
        void IncreaseDevelopmentPoints(int amount);
    }

    [SelectionBase]
    public abstract class Building : MonoBehaviour
    {
        #region FIELDS

        [Inject] protected IBuildingDataSource _dataSource = null;

        [Header("DATA")]
        [ReadOnly][SerializeField] protected int _constructionPoints = default;

        private DisposableList _disposables = new DisposableList();

        protected GridCell Cell { get; private set; }

        #endregion

        #region UNITY

        private void Awake()
        {
            _dataSource.OnPreTick.Subscribe(OnPreTick).AddTo(_disposables);
            _dataSource.OnTick.Subscribe(OnTick).AddTo(_disposables);
            _dataSource.OnPostTick.Subscribe(OnPostTick).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
            Cell.DetachCurrentBuilding();
        }

        #endregion

        #region BASE

        protected virtual void OnPreTick() { }
        protected virtual void OnTick() { }
        protected virtual void OnPostTick() { }

        #endregion

        #region METHODS

        public void InitializeBuilding(GridCell cell)
        {
            Cell = cell;
        }

        public void ReceiveConstructionPoints(int points)
        {
            _constructionPoints += points;
            _dataSource.IncreaseDevelopmentPoints(points);
        }

        #endregion
    }

    public enum BuildingType
    {
        None,
        Base,
        Standard
    }
}
