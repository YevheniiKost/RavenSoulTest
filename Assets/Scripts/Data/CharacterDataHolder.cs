using UnityEngine;

namespace RavenSoul.Data
{
    [CreateAssetMenu(fileName = "CharacterDataHolder", menuName = "Data/CharacterDataHolder")]
    public class CharacterDataHolder : DataHolder<CharacterBlueprint>
    {
        [SerializeField] private string _name;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _initialDamage;
        [SerializeField] private GameObject _prefab;

        public override CharacterBlueprint GetData()
        {
            return new CharacterBlueprint
            {
                Name = _name,
                MovementSpeed = _movementSpeed,
                AttackSpeed = _attackSpeed,
                MaxHealth = _maxHealth,
                InitialDamage = _initialDamage,
                Prefab = _prefab
            };
        }
    }
}