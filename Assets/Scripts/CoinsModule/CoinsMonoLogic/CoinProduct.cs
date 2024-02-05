using ColorBump.Manager.CoinsModule.CoinsCommand.Data;
using Factories;
using UnityEngine;

public abstract class CoinProduct : MonoBehaviour, IProduct
{
    [SerializeField] protected PlayerSettings _playerSettings;
    [SerializeField] protected CommandsData _data;
    
    public string ProductName { get; set; }
    
    protected bool _isInitialized = false;
    protected ICoinEffectStrategy _effectStrategy;
    

    public abstract void Initialize(ICoinEffectStrategy effectStrategy);
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}