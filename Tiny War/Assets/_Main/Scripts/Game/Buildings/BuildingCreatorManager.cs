using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace TinyWar
{
    public partial class BuildingCreatorManager : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SceneContext _sceneContext = null;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _buildingsParent = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Building[] _buildingPrefabs = null;

        private BuildingType _currentSelectedBuidling = default;

        #endregion

        #region UNITY

        private void Start()
        {
            _currentSelectedBuidling = BuildingType.Base;
        }

        private void Update()
        {
            CheckCreation();
        }

        #endregion

        #region METHODS

        private void CheckCreation()
        {
            if (_currentSelectedBuidling == BuildingType.None) return;

            if (Input.GetMouseButtonDown(0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Physics.Raycast(position, Vector3.down, out var hit, Mathf.Infinity))
                {
                    var cell = hit.collider?.gameObject.GetComponentInParent<GridCell>();

                    if (cell == null) return;

                    CreateBuilding(null, cell, _currentSelectedBuidling);
                    _currentSelectedBuidling = BuildingType.None;
                }
            }
        }

        public void CreateBuilding(GridCell originCell, GridCell newCell, BuildingType type)
        {
            var buildingPrefab = _buildingPrefabs[(int)type - 1];
            var position = newCell.BuildingSpawnPosition;

            var newBuilding = _sceneContext.Container.Instantiate(buildingPrefab, position, Quaternion.identity, _buildingsParent);

            newBuilding.InitializeBuilding(newCell);
            newCell.AttachBuilding(newBuilding, originCell);
        }

        #endregion
    }
}
