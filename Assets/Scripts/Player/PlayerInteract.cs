using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform CameraHolder;
    public float InteractionMaxDistance = 1.1f;

    private Pickupable _lastInteractable;
    
    void Update()
    {
        if (PlayerVitalsManager.Instance.PlayerDead)
            return;
        
        var ray = new Ray(CameraHolder.position, CameraHolder.forward);
        if (Physics.Raycast(ray, out var hit, InteractionMaxDistance, 7))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                var interactable = hit.collider.gameObject.GetComponent<Pickupable>();
                if (_lastInteractable != null && _lastInteractable != interactable)
                {
                    _lastInteractable.Hovered = false;
                    HoverMessageManager.Instance.Message = String.Empty;
                }
                interactable.Hovered = true;
                HoverMessageManager.Instance.Message = interactable.ActionString;
                _lastInteractable = interactable;
                // Interaction
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else
            {
                if (_lastInteractable != null)
                {
                    _lastInteractable.Hovered = false;
                }
                HoverMessageManager.Instance.Message = String.Empty;
            }
        }
        else
        {
            if (_lastInteractable != null)
            {
                _lastInteractable.Hovered = false;
            }
            HoverMessageManager.Instance.Message = String.Empty;
        }
    }
}
