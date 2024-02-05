using Strategies;
using UnityEngine;

namespace Factories
{
    public class SlowDownCoinProduct : CoinProduct
    {
        public override void Initialize(ICoinEffectStrategy effectStrategy)
        {
            base.Initialize(_effectStrategy);
            _effectStrategy = effectStrategy;
            ProductName = "SlowDownCoin";
            _isInitialized = true;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!_isInitialized) Initialize(new SlowDownEffectStrategy(_context));
            _effectStrategy?.ApplyEffect(other.GetComponent<Player>());
        }

        protected override void OnTriggerExit(Collider other) =>
            gameObject.SetActive(false);
    }
}