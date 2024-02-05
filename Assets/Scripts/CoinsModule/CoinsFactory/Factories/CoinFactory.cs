using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Factories
{
    public sealed class CoinFactory
    {
        private readonly CommandContext _context;
        private readonly Func<CommandContext, ICoinEffectStrategy> _strategyFactory;

        public CoinFactory(CommandContext context, Func<CommandContext, ICoinEffectStrategy> strategyFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _strategyFactory = strategyFactory ?? throw new ArgumentNullException(nameof(strategyFactory));
        }

        public async Task GetProduct<TProduct>(AssetReference assetReference, Transform parent) where TProduct : MonoBehaviour, IProduct
        {
            var handle = Addressables.InstantiateAsync(assetReference, parent.position, Quaternion.identity, parent);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject instance = handle.Result;
                TProduct product = instance.GetComponent<TProduct>();
                if (product != null)
                {
                    InitializeProduct(product);
                    return;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Failed to instantiate product via Addressables. Type: {typeof(TProduct)}");
#endif
            }
        }

        private void InitializeProduct<TProduct>(TProduct product) where TProduct : IProduct
        {
            var strategy = _strategyFactory(_context);
            product.Initialize(strategy);
        }
    }
}