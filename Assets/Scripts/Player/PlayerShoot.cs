using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform CameraHolder;
    void Update()
    {
        if (EscapeMenuManager.Instance.IsActive)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            var ray = new Ray(CameraHolder.position, CameraHolder.forward);
            if (Physics.Raycast(ray, out var hit, 50f, 7))
            {
                Debug.DrawRay(CameraHolder.position, hit.point, Color.red);
                Debug.Log(hit.distance);
            }
        }
    }
}
