using System;
using System.Collections.Generic;
using RavenSoul.Data;
using UnityEngine;
using RavenSoul.Domain.Level;
using RavenSoul.Domain.Unit;
using RavenSoul.Presentation.Unit;
using RavenSoul.Utilities.Logger;
using RavenSoul.Utilities.Managers;

namespace RavenSoul.Presentation.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private Transform[] _enemySpawnPoints;
        [SerializeField] private List<LevelObject> _levelObjects = new List<LevelObject>();

        private ILevelModel _levelModel;
        private List<IEnemyView> _enemies = new List<IEnemyView>();
        private LevelObject _currentLevelObject;
        private ICharacterModel _characterModel;

        private void OnDestroy()
        {
            _levelModel.OnActionsExpire -= OnLevelEnded;
        }

        public void Init(ILevelModel levelModel)
        {
            _levelModel = levelModel;
            _levelModel.OnActionsExpire += OnLevelEnded;
            SpawnCharacter();
            StartNextAction();
        }
        
        private ICharacterView SpawnCharacter()
        {
            var characterBlueprint = _levelModel.CharacterModel.Blueprint;
            _characterModel = _levelModel.CharacterModel;
            _characterModel.Attributes.OnDeath += OnCharacterDead;
            var character = Instantiate(characterBlueprint.Prefab, _playerSpawnPoint.position, Quaternion.identity);
            if (character.TryGetComponent(out ICharacterView unitModel))
            {
                _playerCamera.SetFollowTarget(character.transform);
                unitModel.Setup(_levelModel.CharacterModel);
                return unitModel;
            }

            MyLogger.LogError(
                $"Character prefab {characterBlueprint.Prefab.name} does not have a component that implements ICharacterView");
            return null;
        }
        
        private void StartNextAction()
        {
            var levelAction = _levelModel.GetNextAction();
            
            if (_levelModel.IsLevelEnded)
                return;

            if (levelAction is EnemySpawnAction enemySpawnAction)
            {
                CoroutineManager.DelayedAction(levelAction.Delay, () => ProcessSpawnEnemiesAction(enemySpawnAction));
            }else if (levelAction is ProcessObjectAction doorOpenAction)
            {
                CoroutineManager.DelayedAction(levelAction.Delay, () => ProcessDoorOpenAction(doorOpenAction));
            }else if(levelAction is InteractWithObjectAction interactWithObjectAction)
            {
                CoroutineManager.DelayedAction(levelAction.Delay, () => ProcessInteractWithObjectAction(interactWithObjectAction));
            }
            else
            {
                MyLogger.LogError($"Unknown level action type {levelAction.GetType().Name}");
            }
        }

        private void ProcessSpawnEnemiesAction(EnemySpawnAction enemySpawnAction)
        {
            EnemyWaveParams waveParams = enemySpawnAction.WaveParams;
            for (int i = 0; i < waveParams.Count; i++)
            {
                IEnemyModel enemyModel = _levelModel.CreateEnemy(waveParams.EnemyBlueprint);
                var enemy = Instantiate(enemyModel.Blueprint.Prefab, GetNextSpawnPoint(i).position,
                    Quaternion.identity);
                if (enemy.TryGetComponent(out IEnemyView enemyView))
                {
                    enemyView.Setup(enemyModel);
                    _enemies.Add(enemyView);
                    enemyView.OnDeath += OnEnemyDeath;
                }
                else
                {
                    MyLogger.LogError(
                        $"Enemy prefab {enemyModel.Blueprint.Prefab.name} does not have a component that implements IEnemyView");
                }
            }
        }

        private Transform GetNextSpawnPoint(int i)
        {
            if (_enemySpawnPoints.Length == 0)
            {
                MyLogger.LogError("No enemy spawn points found");
                return null;
            }

            return _enemySpawnPoints[i % _enemySpawnPoints.Length];
        }

        private void OnEnemyDeath(IEnemyView enemyView)
        {
            _enemies.Remove(enemyView);
            enemyView.OnDeath -= OnEnemyDeath;
            
            if (_enemies.Count == 0)
            {
                StartNextAction();
            }
        }
        
        private void ProcessDoorOpenAction(ProcessObjectAction processObjectAction)
        {
            LevelObject levelObject = _levelObjects.Find(obj => obj.ObjectName == processObjectAction.ObjectName);
            
            if (levelObject != null)
            {
                levelObject.Process();
                StartNextAction();
            }
            else
            {
                MyLogger.LogError($"No level object with name {processObjectAction.ObjectName} found");
            }
        }
        
        private void ProcessInteractWithObjectAction(InteractWithObjectAction interactWithObjectAction)
        {
            _currentLevelObject = _levelObjects.Find(obj => obj.ObjectName == interactWithObjectAction.ObjectName);
            
            if (_currentLevelObject != null)
            {
                _currentLevelObject.OnInteract += OnObjectInteract;
            }
            else
            {
                MyLogger.LogError($"No level object with name {interactWithObjectAction.ObjectName} found");
            }
        }

        private void OnObjectInteract()
        {
            _currentLevelObject.OnInteract -= OnObjectInteract;
            StartNextAction();
        }
        
        private void OnCharacterDead()
        {
            _characterModel.Attributes.OnDeath -= OnCharacterDead;
            CoroutineManager.DelayedAction(3f, () => _levelModel.EndLevel(new LevelEndsResult(LevelEndsReason.CharacterDied)));
        }
        
        private void OnLevelEnded(LevelEndsResult obj)
        {
            _levelModel.EndLevel(new LevelEndsResult(LevelEndsReason.CharacterWin));
        }
    }
}