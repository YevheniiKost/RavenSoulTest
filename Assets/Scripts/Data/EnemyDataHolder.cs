using UnityEngine;

namespace RavenSoul.Data
{
    [CreateAssetMenu(fileName = "EnemyDataHolder", menuName = "Data/EnemyDataHolder")]
    public class EnemyDataHolder : DataHolder<EnemyBlueprint>
    {
        [SerializeField] private string _name;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _initialDamage;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _attackRadius;
        
        public override EnemyBlueprint GetData()
        {
            return new EnemyBlueprint
            {
                Name = _name,
                MovementSpeed = _movementSpeed,
                AttackSpeed = _attackSpeed,
                MaxHealth = _maxHealth,
                InitialDamage = _initialDamage,
                Prefab = _prefab,
                AttackRadius = _attackRadius
            };
        }
    }
}