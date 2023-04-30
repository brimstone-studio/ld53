using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;

public class GamemodeManager : MonoBehaviour
{
    public TMPro.TMP_Text NextWaveLabel;
    public TMPro.TMP_Text WaveText;
    public TMPro.TMP_Text TimeText;
    public Image ListOfItemsToDeliver;
    public RectTransform ItemListPrototype;
    
    public static GamemodeManager Instance;

    public List<Wave> WaveList = new List<Wave>()
    {
        new Wave(
            new[] {PizzaType.Red},
    70),
        new Wave(
            new[] {PizzaType.Blue},
    70),
        new Wave(
            new[] {PizzaType.Yellow},
    70),
        new Wave(
            new[] {PizzaType.Green},
    70),
        new Wave(
            new[] {PizzaType.Purple},
    70),
        new Wave(
            new[] {PizzaType.Red, PizzaType.Green},
    120),
        new Wave(
            new[] {PizzaType.Red, PizzaType.Blue},
    120),
        new Wave(
            new[] {PizzaType.Red, PizzaType.Yellow},
    120),
        new Wave(
            new[] {PizzaType.Red, PizzaType.Purple},
    120),
        new Wave(
            new[] {PizzaType.Red, PizzaType.Green},
    120),
        new Wave(
            new[] {PizzaType.Green, PizzaType.Blue},
    120),
        new Wave(
            new[] {PizzaType.Green, PizzaType.Yellow},
    120),
        new Wave(
            new[] {PizzaType.Green, PizzaType.Purple},
    120),
        new Wave(
            new[] {PizzaType.Blue, PizzaType.Yellow},
    120),
        new Wave(
            new[] {PizzaType.Blue, PizzaType.Purple},
    120),
        new Wave(
            new[] {PizzaType.Yellow, PizzaType.Purple},
    120),
    };

    public Wave CurrentWave => WaveList[WaveNumber - 1];

    void Start()
    {
        Instance = this;
    }

    public bool GameIsCurrentlyHappening
    {
        get
        {
            return _gameIsCurrentlyHappening;
        }
    }

    private bool _gameIsCurrentlyHappening = false;

    public int WaveNumber = 1;

    public Transform PizzaSpawnpoint;

    public List<GameObject> PizzaPrefabs = new List<GameObject>();

    private float _stopwatch;
    
    public void StartGame()
    {
        if (!_gameIsCurrentlyHappening)
        {
            _gameIsCurrentlyHappening = true;
            NextWaveLabel.enabled = false;
            TimeText.enabled = true;
            _stopwatch = CurrentWave.Time;

            _updateUiList();
            
            int pizzaSpawnpointOffset = 0;
            foreach (var pizzaType in CurrentWave.Delivery)
            {
                // Spawn pizza prefab
                Instantiate(PizzaPrefabs[(int)pizzaType.Type], PizzaSpawnpoint.position, Quaternion.identity);
                pizzaSpawnpointOffset++;
            }
        }
    }

    public void PickedUpPizza(PizzaType type)
    {
        var pizza = this.CurrentWave.Delivery.First(p => p.Type == type && p.PickedUp != true);
        pizza.PickedUp = true;
        _updateUiList();
    }

    private void _updateUiList()
    {
        // Read through the wave and display the pizzas that need to be delivered
        foreach (Transform child in ListOfItemsToDeliver.transform) {
            if (child != ItemListPrototype.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        foreach (var pizzaType in CurrentWave.Delivery)
        {
            var instantiated = Instantiate(ItemListPrototype, ListOfItemsToDeliver.transform);
            var p = instantiated.GetComponent<Image>();
            switch (pizzaType.Type)
            {
                case PizzaType.Blue:
                    p.color = new Color(0, 0, 1, _getAlphaBasedOnStatus(pizzaType));
                    break;
                case PizzaType.Green:
                    p.color = new Color(0, 1, 0, _getAlphaBasedOnStatus(pizzaType));
                    break;
                case PizzaType.Red:
                    p.color = new Color(1, 0, 0, _getAlphaBasedOnStatus(pizzaType));
                    break;
                case PizzaType.Yellow:
                    p.color = new Color(1, 0.92f, 0, _getAlphaBasedOnStatus(pizzaType));;
                    break;
                case PizzaType.Purple:
                    p.color = new Color(1, 0, 1, _getAlphaBasedOnStatus(pizzaType));;
                    break;
                default:
                    break;
            }

            p.enabled = true;
            instantiated.gameObject.SetActive(true);
        }
    }

    private float _getAlphaBasedOnStatus(DeliveryPizza pizza)
    {
        return pizza.PickedUp ? 1f : 0.25f;
    }

    public void FinishWave()
    {
        _gameIsCurrentlyHappening = false;
        NextWaveLabel.enabled = true;
        TimeText.enabled = false;
    }

    void Update()
    {
        if (_gameIsCurrentlyHappening)
        {
            _stopwatch -= Time.deltaTime;

            if (_stopwatch >= 0)
            {
                float minutes = Mathf.FloorToInt(_stopwatch / 60);
                float seconds = Mathf.FloorToInt(_stopwatch % 60);

                TimeText.text = $"{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}";
            }
            else
            {
                TimeText.text = "<color=\"red\">00:00</color>";
            }
        }
        
    }
}
