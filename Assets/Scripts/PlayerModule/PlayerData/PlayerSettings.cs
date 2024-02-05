using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "ScriptableObjects/PlayerMovementSettings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    private void OnEnable()
    {
        ForwardSpeed = 5f;
        Sensitivity = 5f;
        StartSpeed = 5f;
        MovementRadius = 40f;
    }

    public float Sensitivity = 10f;
    public float MovementRadius = 42f;
    public float ForwardSpeed = 5f;
    public float StartSpeed = 5f;
}