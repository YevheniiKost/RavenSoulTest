using RavenSoul.Data;
using RavenSoul.Domain.Unit;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class CharacterView : UnitView<CharacterBlueprint>, ICharacterView
    {
        public ICharacterModel Model => _model;
        public GameObject GameObject => gameObject;

        private ICharacterModel _model;
        private IAttributesComponent _attributesComponent;

        protected override void Awake()
        {
            base.Awake();
            _attributesComponent = GetComponentInChildren<IAttributesComponent>();
        }

        public void Setup(ICharacterModel model)
        {
            _model = model;
            base.Setup(model);
            
            _attributesComponent.Init(model);
        }
    }
}