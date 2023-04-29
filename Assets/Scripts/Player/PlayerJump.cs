using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpStrength = 2f;
    public PlayerMove PlayerMove;
    public Rigidbody PlayerRb;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerMove.IsGrounded)
        {
            Debug.Log("jump");
            PlayerRb.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
        }
    }
}
