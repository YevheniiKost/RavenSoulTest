using RavenSoul.Data;
using RavenSoul.Domain.Game;
using RavenSoul.Domain.Level;
using RavenSoul.Domain.Unit;
using RavenSoul.Presentation.Level;
using RavenSoul.Utilities.DependencyInjection;
using RavenSoul.Utilities.Managers;
using UnityEngine;

namespace RavenSoul.Application
{
    public class ApplicationStart : MonoBehaviour
    {
        private void Awake()
        {
            CoroutineManager.CreateInstance();
            PersistantBehaviour.CreateInstance();
            
            CreateDiContainer();
        }

        private void CreateDiContainer()
        {
            DiServiceCollection diServiceCollection = new DiServiceCollection();
            
            diServiceCollection.RegisterSingleton<IGameModel, GameModel>();
            diServiceCollection.RegisterSingleton<ILevelLoader, LevelLoader>();
            diServiceCollection.RegisterSingleton<ILevelsRepository, LevelsRepository>();
            
            diServiceCollection.RegisterTransient<ILevelModel, LevelModel>();
            diServiceCollection.RegisterTransient<ICharacterModel, CharacterModel>();
            diServiceCollection.RegisterTransient<IAttributesModel, AttributesModel>();
            diServiceCollection.RegisterTransient<IAbilitiesModel, AbilitiesModel>();
            diServiceCollection.RegisterTransient<IEnemyModel, EnemyModel>();

            diServiceCollection.GenerateContainer();
        }

        private void Start()
        {
            IGameModel gameModel = DiContainer.Instance.GetService<IGameModel>();
            gameModel.Initialize();
            gameModel.Start();
        }
    }
}