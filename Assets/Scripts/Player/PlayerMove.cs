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
    void FixedUpdate()
    {
        // efficient access
        var thisTransform = transform;
        var isGrounded = Physics.Raycast(thisTransform.position, Vector3.down, 2.2f);

        if (isGrounded)
        {
            PlayerRb.drag = GroundDrag;
        }
        else
        {
            PlayerRb.drag = 0f;
        }
        
        var hInput = Input.GetAxisRaw("Horizontal");
        var vInput = Input.GetAxisRaw("Vertical");

       
        var movementVector = Vector3.Normalize(thisTransform.forward * vInput + thisTransform.right * hInput);
        PlayerRb.AddForce(movementVector * MovementSpeed, ForceMode.Force);
    }
}
