using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [FormerlySerializedAs("MovementSettings")] public PlayerSettings _settings;
    public Rigidbody Rigidbody { get; private set; }
    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            MoveHorizontally();
            MoveForward();
        }
    }

    private void MoveHorizontally()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        //Vector3 horizontalMovement = horizontalInput * Vector3.right * _settings.Sensitivity * Time.fixedDeltaTime;
        transform.position = new Vector3((horizontalInput * _settings.Sensitivity * Time.fixedDeltaTime),
            transform.position.y, transform.position.z);
    }

    private void MoveForward() =>
        Rigidbody.MovePosition(transform.position + transform.forward * _settings.ForwardSpeed * Time.fixedDeltaTime);
}