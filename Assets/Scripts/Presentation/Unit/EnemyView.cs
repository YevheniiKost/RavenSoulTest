using System;
using RavenSoul.Data;
using RavenSoul.Domain.Unit;
using RavenSoul.Utilities.Managers;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class EnemyView : UnitView<EnemyBlueprint>, IEnemyView
    {
        public event Action<IEnemyView> OnDeath;

        [SerializeField] private float _deathDelay = 3f;
        
        public IEnemyModel Model { get; private set; }
        public void Setup(IEnemyModel model)
        {
            Model = model;
            base.Setup(model);
            AbilitiesComponent.SetAttackRadius(Model.Blueprint.AttackRadius);

            if (InputComponent is EnemyAIInput enemyAIInput)
            {
                enemyAIInput.SetBlueprint(Model.Blueprint);
            }
        }

        protected override void OnDeathHandler()
        {
            base.OnDeathHandler();
            OnDeath?.Invoke(this);
            
            CoroutineManager.DelayedAction(_deathDelay, () => Destroy(gameObject));
        }
    }
}