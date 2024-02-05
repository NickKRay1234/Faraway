using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [FormerlySerializedAs("MovementSettings")] public PlayerSettings _settings;
    public Rigidbody Rigidbody { get; private set; }
    public bool CanMove { get; set; } = true;

    private Action Movement;  

    private void Awake() {
        Rigidbody = GetComponent<Rigidbody>();
        SetupMovement();
    }

    private void SetupMovement() {
#if UNITY_EDITOR
        Movement = MoveEditor;
#else
        Movement = MoveMobile;
#endif
    }

    private void FixedUpdate()
    {
        if (CanMove) 
            Movement?.Invoke();
    }

    private void MoveEditor()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = horizontalInput * Vector3.right * _settings.Sensitivity * Time.fixedDeltaTime;
        Vector3 forwardMovement = GetMoveForward();
        Rigidbody.MovePosition(transform.position + horizontalMovement + forwardMovement);
    }

    private void MoveMobile() {
        Vector3 horizontalMovement = Vector3.zero;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                Vector3 moveDirection = new Vector3(touchDeltaPosition.x, 0f, touchDeltaPosition.y).normalized;
                moveDirection = Camera.main.transform.TransformDirection(moveDirection);
                moveDirection.y = 0f;
                horizontalMovement = moveDirection * _settings.Sensitivity * Time.fixedDeltaTime;
            }
        }
        
        Vector3 forwardMovement = GetMoveForward();
        Rigidbody.MovePosition(transform.position + horizontalMovement + forwardMovement);
    }

    private Vector3 GetMoveForward() => transform.forward * _settings.ForwardSpeed * Time.fixedDeltaTime;
}