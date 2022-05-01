using UnityEngine;

namespace TinyWar
{
    public class StandardBuilding : Building
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _amountNeededForPassing = 5;

        #endregion

        #region BASE

        protected override void OnPreTick()
        {
            if (_constructionPoints < _amountNeededForPassing) return;

            PassConstructionPoints();
        }

        #endregion

        #region METHODS

        private void PassConstructionPoints()
        {
            if (Cell.AvailableCellsCount == 0) return;

            var passingTimes = _constructionPoints / _amountNeededForPassing;

            for (int i = 0; i < passingTimes; i++)
            {
                PassConstructionPointToRandomCell();
            }

            _constructionPoints %= _amountNeededForPassing;
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
