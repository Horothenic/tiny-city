using System;

using UniRx;
using Zenject;

namespace TinyWar
{
    public partial class BuildingsDataManager
    {
        #region FIELDS

        [Inject] private BuildingCreatorManager _buildingCreatorManager = null;
        [Inject] private TickManager _tickManager = null;
        [Inject] private DevelopmentManager _developmentManager = null;

        #endregion
    }

    public partial class BuildingsDataManager : IBuildingDataSource
    {
        IObservable<Unit> IBuildingDataSource.OnPreTick => _tickManager.OnPreTickObservable;
        IObservable<Unit> IBuildingDataSource.OnTick => _tickManager.OnTickObservable;
        IObservable<Unit> IBuildingDataSource.OnPostTick => _tickManager.OnPostTickObservable;

        void IBuildingDataSource.CreateBuilding(GridCell originCell, GridCell newCell, BuildingType type)
        {
            _buildingCreatorManager.CreateBuilding(originCell, newCell, type);
        }

        void IBuildingDataSource.IncreaseDevelopmentPoints(int amount)
        {
            _developmentManager.IncreasePoints(amount);
        }
    }
}
