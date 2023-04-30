using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Should be refactored to "Interactable"
// Having this component implies that the object also has the "outline" material
public class Pickupable : MonoBehaviour
{
    // In the format "Press [key] to.."
    public string ActionString = "do something";
    public PickupableType Type;

    private readonly  float GLOWING_AMOUNT = 3f;
    private readonly Color GLOWING_COLOR = new Color(0.50293231f, 2, 0, 1);

    public bool Hovered
    {
        get
        {
            return _hovered;
        }
        set
        {
            _hovered = value;
            if (_hovered)
            {
                _makeGlow();
            }
            else
            {
                _unglow();
            }
        }
    }

    private bool _hovered = false;
    private Renderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        switch (Type)
        {
            case PickupableType.StartButton:
                _startButtonInteraction();
                break;
            case PickupableType.Pizza:
                _pizzaInteraction();
                break;
        }
    }

    private void _makeGlow()
    {
        _renderer.materials[1].SetFloat("_Glow", GLOWING_AMOUNT);
        _renderer.materials[1].SetColor("_Color", GLOWING_COLOR);
    }

    private void _unglow()
    {
        _renderer.materials[1].SetFloat("_Glow", 0f);
    }
    
    // Different actions
    private void _startButtonInteraction()
    {
        GamemodeManager.Instance.StartGame();
    }

    private void _pizzaInteraction()
    {
        
    }
}

public enum PickupableType
{
    Pizza,
    StartButton
}
