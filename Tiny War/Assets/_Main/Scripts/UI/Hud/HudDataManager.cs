using System;

using Zenject;

namespace TinyWar
{
    public partial class HudDataManager
    {
        #region FIELDS

        [Inject] private DevelopmentManager _developmentManager = null;

        #endregion
    }

    public partial class HudDataManager : IDevelopmentPointsAmountDataSource
    {
        IObservable<int> IDevelopmentPointsAmountDataSource.DevelopmentPointsChangeObservable => _developmentManager.DevelopmentPointsChangeObservable;
    }
}
