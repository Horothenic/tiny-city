using UnityEngine;
using Zenject;

namespace TinyWar
{
    public class GlobalInstaller : MonoInstaller
    {
        [Header("COMPONENTS")]
        [SerializeField] private BuildingCreatorManager _buildingCreatorManager = null;
        [SerializeField] private GridManager _gridManager = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private StatsContainer _statsContainer = null;

        public override void InstallBindings()
        {
            InstallGlobalManagers();
            InstallDataManagers();
            InstallStatsManagers();
            InstallSceneManagers();
        }

        public void InstallGlobalManagers()
        {
            Container.BindInterfacesAndSelfTo<TickManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<DevelopmentManager>().AsSingle();
        }

        public void InstallDataManagers()
        {
            Container.BindInterfacesAndSelfTo<BuildingsDataManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<HudDataManager>().AsSingle();
        }

        public void InstallStatsManagers()
        {
            Container.BindInterfacesAndSelfTo<StatsContainer>().FromInstance(_statsContainer);
        }

        public void InstallSceneManagers()
        {
            Container.BindInterfacesAndSelfTo<BuildingCreatorManager>().FromInstance(_buildingCreatorManager);
            Container.BindInterfacesAndSelfTo<GridManager>().FromInstance(_gridManager);
        }
    }
}
