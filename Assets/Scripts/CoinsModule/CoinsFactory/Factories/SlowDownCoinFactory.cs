using Strategies;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Factories
{
    public sealed class SlowDownCoinFactory : AbstractFactory
    {
        public IProduct GetProduct(Transform parent)
        {
            InstantiateProduct<SlowDownCoinProduct>(new AssetReference(_addressablesCoinsData.SlowDownCoin), parent);
            return null;
        }

        protected override void InitializeProduct<TProduct>(TProduct product)
        {
            if (product is SlowDownCoinProduct slowDownCoinProduct)
            {
                ICoinEffectStrategy slowDownEffectStrategy = new SlowDownEffectStrategy(_context);
                slowDownCoinProduct.Initialize(slowDownEffectStrategy);
            }
        }
    }
}