using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderReadyLight : MonoBehaviour
{
    public Renderer renderer;

    [ColorUsage(true, true)]
    public Color EmissionRed;
    
    [ColorUsage(true, true)]
    public Color EmissionGreen;
    
    void FixedUpdate()
    {
        if (GamemodeManager.Instance.GameIsCurrentlyHappening)
        {
            renderer.material.SetColor("_EmissionColor", EmissionRed);
        }
        else
        {
            renderer.material.SetColor("_EmissionColor", EmissionGreen);
        }
    }
}
