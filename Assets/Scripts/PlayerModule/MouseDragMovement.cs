using UnityEngine;

public class MouseDragMovement
{
    private Rigidbody _rigidbody;
    private float _sensitivity;
    private Vector3 _lastMousePosition;
    private float _movementRadius;
    private int _velocityLimiter = 5;

    public MouseDragMovement(Rigidbody rigidbody, float sensitivity, float movementRadius)
    {
        _rigidbody = rigidbody;
        _sensitivity = sensitivity;
        _movementRadius = movementRadius;
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
            _lastMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 radiusOffset = Vector3.ClampMagnitude(_lastMousePosition - Input.mousePosition, _movementRadius);
            _rigidbody.AddForce(-radiusOffset * _sensitivity - _rigidbody.velocity / _velocityLimiter, ForceMode.VelocityChange);
            _lastMousePosition = Input.mousePosition;
        }

        _rigidbody.velocity = Vector3.Normalize(_rigidbody.velocity);
    }
}