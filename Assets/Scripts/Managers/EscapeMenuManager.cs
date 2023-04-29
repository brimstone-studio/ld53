using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuManager : MonoBehaviour
{
    public static EscapeMenuManager Instance;
    public Canvas EscapeMenuCanvas;
    
    public bool IsActive {
        get
        {
            return _isActive;
        }
    }

    private bool _isActive = false;

    private void Start()
    {
        EscapeMenuCanvas.enabled = false;
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isActive = !_isActive;
            EscapeMenuCanvas.enabled = !EscapeMenuCanvas.enabled;

            if (_isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
