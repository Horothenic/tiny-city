using UnityEngine;
using System.Collections.Generic;

namespace TinyWar
{
    public class GridManager : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _width = 0;
        [SerializeField] private int _height = 0;
        [SerializeField] private Transform _gridParent = null;
        [SerializeField] private GridCell _gridCellPrefab = null;

        private GridCell[,] grid = null;

        #endregion

        #region UNITY

        private void Start()
        {
            CreateGrid();
        }

        #endregion

        #region METHODS

        private void CreateGrid()
        {
            grid = new GridCell[_width, _height];

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var offsetPosition = new Vector3(i, 0, j);
                    grid[i, j] = Instantiate(_gridCellPrefab, _gridParent.position + offsetPosition, Quaternion.identity, _gridParent);
                }
            }

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    grid[i, j].Initialize(FindSurroundingCells(i, j));
                }
            }
        }

        private GridCell[] FindSurroundingCells(int x, int y)
        {
            var list = new List<GridCell>();

            if (x > 0)
            {
                list.Add(grid[x - 1, y]);

                if (y > 0)
                {
                    list.Add(grid[x - 1, y - 1]);
                }

                if (y < _height - 1)
                {
                    list.Add(grid[x - 1, y + 1]);
                }
            }

            if (x < _width - 1)
            {
                list.Add(grid[x + 1, y]);

                if (y > 0)
                {
                    list.Add(grid[x + 1, y - 1]);
                }

                if (y < _height - 1)
                {
                    list.Add(grid[x + 1, y + 1]);
                }
            }

            if (y > 0)
            {
                list.Add(grid[x, y - 1]);
            }

            if (y < _height - 1)
            {
                list.Add(grid[x, y + 1]);
            }

            return list.ToArray();
        }

        #endregion
    }
}
