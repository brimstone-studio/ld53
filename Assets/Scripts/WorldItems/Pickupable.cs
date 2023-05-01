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
    
    // Non applicable if not pizza
    public PizzaType PizzaType;

    private readonly  float GLOWING_AMOUNT = 1.2f;
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
            case PickupableType.Door:
                _doorInteraction();
                break;
            case PickupableType.BuyHealth:
                _healthBuy();
                break;
            case PickupableType.BuyAmmo:
                _ammoBuy();
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
        if (PizzaType != null)
        {
            SoundManager.Instance.Pickup.Play();
            GamemodeManager.Instance.PickedUpPizza(PizzaType);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogError("Picked up a pizza without a pizza type");
        }
        
    }

    private void _doorInteraction()
    {
        GamemodeManager.Instance.DeliverPizza(PizzaType);
    }

    private void _healthBuy()
    {
        if (PlayerVitalsManager.Instance.Health < 75 && PlayerVitalsManager.Instance.Money >= 100)
        {
            PlayerVitalsManager.Instance.Money -= 100;
            SoundManager.Instance.Pickup.Play();
            PlayerVitalsManager.Instance.Health += 25;
            if (PlayerVitalsManager.Instance.Health > 100)
            {
                PlayerVitalsManager.Instance.Health = 100;
            }
        }
    }

    private void _ammoBuy()
    {
        if (PlayerVitalsManager.Instance.Money >= 50)
        {
            PlayerVitalsManager.Instance.Money -= 50;
            SoundManager.Instance.Pickup.Play();
            PlayerVitalsManager.Instance.Ammo += 10;
        }
    }
}

public enum PickupableType
{
    Pizza,
    StartButton,
    Door,
    BuyHealth,
    BuyAmmo
}
