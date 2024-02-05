using Factories;
using UnityEngine;

public class SpeedUpCoinProduct : CoinProduct, IProduct
{
    public override void Initialize(ICoinEffectStrategy effectStrategy)
    {
        _effectStrategy = effectStrategy;
        ProductName = "SpeedUpCoin";
        _isInitialized = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!_isInitialized) Initialize(new SpeedUpEffectStrategy(_playerSettings, _data));
        _effectStrategy?.ApplyEffect(other.GetComponent<Player>());
    }

    protected override void OnTriggerExit(Collider other) =>
        gameObject.SetActive(false);
}