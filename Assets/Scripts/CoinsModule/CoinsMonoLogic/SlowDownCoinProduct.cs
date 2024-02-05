using UnityEngine;

namespace Factories
{
    public class SlowDownCoinProduct : CoinProduct, IProduct
    {
        public override void Initialize(ICoinEffectStrategy effectStrategy)
        {
            _effectStrategy = effectStrategy;
            ProductName = "SlowDownCoin";
            _isInitialized = true;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!_isInitialized) Initialize(new SlowDownEffectStrategy(_playerSettings, _data));
            _effectStrategy?.ApplyEffect(other.GetComponent<Player>());
        }

        protected override void OnTriggerExit(Collider other) =>
            gameObject.SetActive(false);
    }
}