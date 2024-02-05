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
    protected CommandContext _context;
    

    public virtual void Initialize(ICoinEffectStrategy effectStrategy)
    {
        _context = new CommandContext(_playerSettings, _data, 10);
    }

    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}