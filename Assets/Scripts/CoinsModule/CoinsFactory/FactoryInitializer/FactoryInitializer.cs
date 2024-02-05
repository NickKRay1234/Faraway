using System.Threading.Tasks;
using Strategies;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Factories
{
    public class FactoryInitializer : MonoBehaviour
    {
        private CoinFactory _coinFactory;
        
        public void InitializeFactory(CommandContext context) =>
            _coinFactory = new CoinFactory(context, ctx => new FlyEffectStrategy(ctx));

        public async Task CreateProduct<TProduct>(AssetReference assetReference, Transform parent) where TProduct : MonoBehaviour, IProduct =>
            await _coinFactory.GetProduct<TProduct>(assetReference, parent);
    }
}