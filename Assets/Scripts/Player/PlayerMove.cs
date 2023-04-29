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

        var movementVector = Vector3.Normalize(thisTransform.forward * vInput + thisTransform.right * hInput);
        PlayerRb.AddForce(movementVector * speed, ForceMode.Force);
    }
}
