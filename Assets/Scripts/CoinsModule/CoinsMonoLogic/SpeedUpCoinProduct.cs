﻿using Factories;
using Strategies;
using UnityEngine;

public class SpeedUpCoinProduct : CoinProduct
{
    public override void Initialize(ICoinEffectStrategy effectStrategy)
    {
        _effectStrategy = effectStrategy;
        ProductName = "SpeedUpCoin";
        _isInitialized = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!_isInitialized) Initialize(new SpeedUpEffectStrategy(new CommandContext(_playerSettings, _data, 10)));
        _effectStrategy?.ApplyEffect(other.GetComponent<Player>());
    }

    protected override void OnTriggerExit(Collider other)
    {
        //gameObject.SetActive(false);
    }
}