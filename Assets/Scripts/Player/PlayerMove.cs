using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMove : MonoBehaviour
{
    public float MovementSpeed;
    public float GroundDrag;
    public Rigidbody PlayerRb;
    public Transform CameraHolder;
    public float MaxSlopeAngle;
    public Animator ViewmodelAnimator;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
    }
    private bool _isGrounded = false;
    void FixedUpdate()
    {
        // efficient access
        var thisTransform = transform;
        _isGrounded = Physics.Raycast(thisTransform.position, Vector3.down, 1.2f, 7);
        Debug.DrawLine(thisTransform.position, thisTransform.position + Vector3.down * 1.2f, Color.green);

        if (EscapeMenuManager.Instance.IsActive)
            return;
        
        var speed = MovementSpeed;
        
        if (_isGrounded)
        {
            PlayerRb.drag = GroundDrag;
        }
        else
        {
            PlayerRb.drag = 0f;
            speed /= 5;
        }
        
        var hInput = Input.GetAxisRaw("Horizontal");
        var vInput = Input.GetAxisRaw("Vertical");
        
        Debug.Log($"{hInput != 0f} {vInput != 0f}");

        if ((hInput != 0f || vInput != 0f) && _isGrounded)
        {
            ViewmodelAnimator.SetBool("Walking", true);
        }
        else
        {
            ViewmodelAnimator.SetBool("Walking", false);
        }

        var movementVector = Vector3.Normalize(thisTransform.forward * vInput + thisTransform.right * hInput);
        movementVector = _slopeMoveDir(movementVector);
        PlayerRb.AddForce(movementVector * speed, ForceMode.Force);
    }

    // Returns original movementVector if not on slope
    private Vector3 _slopeMoveDir(Vector3 movementVector)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, 1.2f))
        {
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            if (angle < MaxSlopeAngle && angle != 0)
            {
                return Vector3.ProjectOnPlane(movementVector, hit.normal).normalized;
            }
            else
            {
                return movementVector;
            }
        }

        return movementVector;
    }
}
