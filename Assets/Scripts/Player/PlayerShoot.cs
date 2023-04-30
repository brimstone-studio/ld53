using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform CameraHolder;
    public GameObject RocketPrototype;
    public Animator ViewmodelAnimator;
    void Update()
    {
        if (EscapeMenuManager.Instance.IsActive)
            return;
        
        if (Input.GetMouseButtonDown(0) && PlayerVitalsManager.Instance.Ammo > 0)
        {
            PlayerVitalsManager.Instance.AmmoDecrease(1);
            var spawnedRocket = GameObject.Instantiate(RocketPrototype, RocketPrototype.transform.position, CameraHolder.rotation);
            ViewmodelAnimator.SetTrigger("Shoot");
            spawnedRocket.SetActive(true);
            // var ray = new Ray(CameraHolder.position, CameraHolder.forward);
            // if (Physics.Raycast(ray, out var hit, 50f, 7))
            // {
            //     Debug.DrawRay(CameraHolder.position, hit.point, Color.red);
            // }
        }
    }
}
