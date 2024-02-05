using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AddressablesCoinsData", menuName = "Coins", order = 1)]
    public class AddressablesCoinsData : ScriptableObject
    {
        [Header("Addressables names: ")] 
        public string FlyCoin = "FlyCoin";
        public string SpeedUpCoin = "SpeedUpCoin";
        public string SlowDownCoin = "SlowDownCoin";
    }
}