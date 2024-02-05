using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Factories
{
    public sealed class SpeedUpCoinFactory : AbstractFactory
    {
        public IProduct GetProduct(Transform parent)
        {
            InstantiateProduct<SlowDownCoinProduct>(new AssetReference(_addressablesCoinsData.SpeedUpCoin), parent);
            return null;
        }

        protected override void InitializeProduct<TProduct>(TProduct product)
        {
            if (product is SlowDownCoinProduct slowDownCoinProduct)
            {
                ICoinEffectStrategy slowDownEffectStrategy = new SpeedUpEffectStrategy(_playerSettings, _data);
                slowDownCoinProduct.Initialize(slowDownEffectStrategy);
            }
        }
    }
}