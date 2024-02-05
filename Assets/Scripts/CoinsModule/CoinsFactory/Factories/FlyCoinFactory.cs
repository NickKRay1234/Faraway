using Strategies;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Factories
{
    public sealed class FlyCoinFactory : AbstractFactory
    {
        public IProduct GetProduct(Transform parent)
        {
            InstantiateProduct<FlyCoinProduct>(new AssetReference(_addressablesCoinsData.FlyCoin), parent);
            return null;
        }

        protected override void InitializeProduct<TProduct>(TProduct product)
        {
            if (product is FlyCoinProduct flyCoinProduct)
            {
                ICoinEffectStrategy flyEffectStrategy = new FlyEffectStrategy(_context);
                flyCoinProduct.Initialize(flyEffectStrategy);
            }
        }
    }
}