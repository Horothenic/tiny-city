using UnityEngine;

namespace TinyWar
{
    public class BaseBuilding : Building
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _contructionsPointsPerTick = 1;
        [SerializeField] private int _developmentPointsPerTick = 5;

        #endregion

        #region BASE

        protected override void OnPreTick()
        {
            PassConstructionPoints();
        }

        protected override void OnTick()
        {
            _constructionPoints += _contructionsPointsPerTick;
            _dataSource.IncreaseDevelopmentPoints(_developmentPointsPerTick);
        }

        #endregion

        #region METHODS

        private void PassConstructionPoints()
        {
            for (int i = 0; i < _constructionPoints; i++)
            {
                PassConstructionPointToRandomCell();
            }

            _constructionPoints = 0;
        }

        private void PassConstructionPointToRandomCell()
        {
            var randomIndex = Random.Range(0, Cell.AvailableCellsCount);
            var chosenCell = Cell.AvailableCells[randomIndex];

            if (chosenCell.CurrentBuilding == null)
            {
                _dataSource.CreateBuilding(Cell, chosenCell, BuildingType.Standard);
            }
            else
            {
                chosenCell.CurrentBuilding.ReceiveConstructionPoints(1);
            }
        }

        #endregion
    }
}
