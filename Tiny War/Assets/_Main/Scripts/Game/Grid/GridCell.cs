using UnityEngine;
using System.Collections.Generic;

namespace TinyWar
{
    public class GridCell : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Collider _collider = null;
        [SerializeField] private Transform _buildingSpawn = null;

        private List<GridCell> _availableCells = null;

        public Building CurrentBuilding { get; private set; }
        public GridCell[] SurroundingCells { get; private set; }
        public GridCell[] AvailableCells => _availableCells.ToArray();
        public int AvailableCellsCount => _availableCells.Count;
        public Vector3 BuildingSpawnPosition => _buildingSpawn.position;

        #endregion

        #region METHODS

        public void Initialize(GridCell[] surroundingCells)
        {
            SurroundingCells = surroundingCells;
            _availableCells = new List<GridCell>(surroundingCells);
        }

        private void RemoveParentCells(GridCell parentCell)
        {
            if (parentCell == null) return;

            var cellsToRemove = new List<GridCell>();
            cellsToRemove.Add(parentCell);
            cellsToRemove.AddRange(parentCell.SurroundingCells);

            foreach (var cell in cellsToRemove)
            {
                _availableCells.Remove(cell);
            }
        }

        public void AttachBuilding(Building building, GridCell parentCell)
        {
            RemoveParentCells(parentCell);
            CurrentBuilding = building;
            DisableCollider();
        }

        public void DetachCurrentBuilding()
        {
            CurrentBuilding = null;
            EnableCollider();
        }

        public void EnableCollider()
        {
            if (_collider == null) return;

            _collider.enabled = true;
        }

        public void DisableCollider()
        {
            if (_collider == null) return;

            _collider.enabled = false;
        }

        #endregion
    }
}
