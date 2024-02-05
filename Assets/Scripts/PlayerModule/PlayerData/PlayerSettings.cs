using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    private void OnEnable()
    {
        ForwardSpeed = 5f;
    }

    public float Sensitivity = 5f;
    public float MovementRadius = 42f;
    public float ForwardSpeed = 5f;
    public float StartSpeed = 5f;
}