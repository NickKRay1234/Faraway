using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Factories
{
    public abstract class AbstractFactory : MonoBehaviour
    {
        protected CommandContext _context;
        protected AddressablesCoinsData _addressablesCoinsData;
        protected PlayerSettings _playerSettings;
        protected CommandsData _data;
        

        protected void InstantiateProduct<TProduct>(AssetReference assetReference, Transform parent) where TProduct : MonoBehaviour, IProduct
        {
            _context = new CommandContext(_playerSettings, _data, 10);
            var handle = Addressables.InstantiateAsync(assetReference, parent.position, Quaternion.identity, parent);
            handle.Completed += OnObjectInstantiated<TProduct>;
        }

        private void OnObjectInstantiated<TProduct>(AsyncOperationHandle<GameObject> handle) where TProduct : MonoBehaviour, IProduct
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject instance = handle.Result;
                TProduct product = instance.GetComponent<TProduct>();
                if (product != null)
                    InitializeProduct(product);
            }
            else
#if UNITY_EDITOR
                Debug.LogError($"Failed to instantiate product via Addressables. Type: {typeof(TProduct)}");
#endif
        }

        protected abstract void InitializeProduct<TProduct>(TProduct product) where TProduct : MonoBehaviour, IProduct;
    }
}