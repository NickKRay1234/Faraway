using Strategies;
using UnityEngine;

namespace Factories
{
    public class SlowDownCoinProduct : CoinProduct
    {
        public override void Initialize(ICoinEffectStrategy effectStrategy)
        {
            _effectStrategy = effectStrategy;
            ProductName = "SlowDownCoin";
            _isInitialized = true;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!_isInitialized) Initialize(new SlowDownEffectStrategy(new CommandContext(_playerSettings, _data, 10)));
            _effectStrategy?.ApplyEffect(other.GetComponent<Player>());
        }

        protected override void OnTriggerExit(Collider other)
        {
            //gameObject.SetActive(false);
        }
    }
}