using UnityEngine;

namespace ColorBump.Manager.CoinsModule.CoinsCommand.Data
{
    [CreateAssetMenu(fileName = "CommandsData", menuName = "ScriptableObjects/CommandsData", order = 1)]
    public class CommandsData : ScriptableObject
    {
        private void OnEnable()
        {
            NormalHeight = 0.5f;
            NormalScale = new(1, 1, 1);
        }
        
        public float MinSpeed = 2.5f;
        public float MaxSpeed = 10f;
        public float MaxHeight = 10f;
        public float NormalHeight = 0.5f;
        public Vector3 NormalScale = new(1, 1, 1);
        public Vector3 MaxScale = new(3, 3, 3);
    }
}