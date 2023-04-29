using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public float Sensitivity = 300f;

    public Rigidbody PlayerRb;
    public Transform CameraHolder;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (EscapeMenuManager.Instance.IsActive)
            return;
        
        // yes they need to be switched up
        float yOffset = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * Sensitivity;
        float xOffset = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * Sensitivity;

        xOffset = Mathf.Clamp(-xOffset, -90f, 90f);
        
        CameraHolder.rotation = Quaternion.Euler(CameraHolder.rotation.eulerAngles.x + xOffset, PlayerRb.rotation.eulerAngles.y + yOffset, 0f);
        PlayerRb.rotation = Quaternion.Euler(0f, PlayerRb.rotation.eulerAngles.y + yOffset, 0f);
    }
}
