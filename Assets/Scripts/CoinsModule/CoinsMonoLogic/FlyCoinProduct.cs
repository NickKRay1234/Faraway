using UnityEngine;

namespace Factories
{
    public class FlyCoinProduct : CoinProduct
    {
        public override void Initialize(ICoinEffectStrategy effectStrategy)
        {
            _effectStrategy = effectStrategy;
            ProductName = "FlyCoin";
            _isInitialized = true;
        }
        

        protected override void OnTriggerEnter(Collider other)
        {
            if(!_isInitialized) Initialize(new FlyEffectStrategy(_playerSettings, _data));
            _effectStrategy?.ApplyEffect(other.GetComponent<Player>());
        }

        protected override void OnTriggerExit(Collider other) =>
            gameObject.SetActive(false);
    }
}