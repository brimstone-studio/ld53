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

    [NonSerialized]
    public List<RobotSpawner> Spawners = new List<RobotSpawner>();

    public List<Wave> WaveList = new List<Wave>()
{
    // Single colored pizzas
    new Wave(new[] {PizzaType.Red}, 20),
    new Wave(new[] {PizzaType.Blue}, 20),

    // Two different colored pizzas
    new Wave(new[] {PizzaType.Red, PizzaType.Blue}, 30),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green}, 30),

    // Two different colored pizzas with one duplicate
    new Wave(new[] {PizzaType.Red, PizzaType.Blue, PizzaType.Red}, 30),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green, PizzaType.Blue}, 30),

    // Three different colored pizzas
    new Wave(new[] {PizzaType.Red, PizzaType.Green, PizzaType.Yellow}, 40),
    new Wave(new[] {PizzaType.Yellow, PizzaType.Purple, PizzaType.Blue}, 40),

    // Three different colored pizzas with one duplicate
    new Wave(new[] {PizzaType.Red, PizzaType.Green, PizzaType.Yellow, PizzaType.Green}, 40),
    new Wave(new[] {PizzaType.Yellow, PizzaType.Purple, PizzaType.Blue, PizzaType.Purple}, 40),

    // Four different colored pizzas
    new Wave(new[] {PizzaType.Red, PizzaType.Blue, PizzaType.Green, PizzaType.Yellow}, 40),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple}, 40),

    // Four different colored pizzas with one duplicate
    new Wave(new[] {PizzaType.Red, PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Blue}, 50),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple, PizzaType.Yellow}, 50),

    // Five different colored pizzas
    new Wave(new[] {PizzaType.Red, PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple}, 50),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple, PizzaType.Red}, 50),

    // Five different colored pizzas with one duplicate
    new Wave(new[] {PizzaType.Red, PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple, PizzaType.Red}, 50),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple, PizzaType.Red, PizzaType.Blue}, 50),

    // Six different colored pizzas (with two duplicates)
    new Wave(new[] {PizzaType.Red, PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple, PizzaType.Red, PizzaType.Yellow}, 50),
    new Wave(new[] {PizzaType.Blue, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple, PizzaType.Red, PizzaType.Blue, PizzaType.Purple}, 50),

    // Randomized orders with increasing difficulty
    new Wave(new[] {PizzaType.Red, PizzaType.Yellow, PizzaType.Red, PizzaType.Green, PizzaType.Yellow, PizzaType.Purple}, 360),
    new Wave(new[] {PizzaType.Blue, PizzaType.Purple, PizzaType.Yellow, PizzaType.Green, PizzaType.Yellow, PizzaType.Red, PizzaType.Green}, 50),
    };

    public Wave CurrentWave => WaveNumber <= WaveList.Count ? WaveList[WaveNumber - 1] : WaveList.Last();

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

    public int WaveNumber
    {
        get
        {
            return _waveNumber;
        }
        set
        {
            _waveNumber = value;
            WaveText.text = $"Wave <color=\"yellow\">{_waveNumber}</color>";
        }
    }

    private int _waveNumber = 1;

    public Transform PizzaSpawnpoint;

    public List<GameObject> PizzaPrefabs = new List<GameObject>();

    private float _stopwatch;

    private int _deliveryIndex = 0;
    
    public void StartGame()
    {
        if (!_gameIsCurrentlyHappening)
        {
            _gameIsCurrentlyHappening = true;
            NextWaveLabel.enabled = false;
            TimeText.enabled = true;
            _stopwatch = CurrentWave.Time;
            _deliveryIndex = 0;

            _updateUiList();
            
            int pizzaSpawnpointOffset = 0;
            foreach (var pizzaType in CurrentWave.Delivery)
            {
                // Spawn pizza prefab
                Instantiate(PizzaPrefabs[(int)pizzaType.Type], PizzaSpawnpoint.position, Quaternion.identity);
                pizzaSpawnpointOffset++;
            }
            
            // Spawn robots
            foreach (var robotSpawner in Spawners)
            {
                robotSpawner.Spawn(WaveNumber * 2);
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

        if (!this._gameIsCurrentlyHappening)
        {
            return;
        }

        int index = 0;
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
            
            // Is current
            if (index == _deliveryIndex)
            {
                p.transform.Find("Highlight").gameObject.SetActive(true);
            }
            else
            {
                p.transform.Find("Highlight").gameObject.SetActive(false);
            }
            
            // Delivered
            if (pizzaType.Delivered)
            {
                p.transform.Find("Check").gameObject.SetActive(true);
            }
            else
            {
                p.transform.Find("Check").gameObject.SetActive(false);
            }

            p.enabled = true;
            instantiated.gameObject.SetActive(true);

            index++;
        }
    }

    private float _getAlphaBasedOnStatus(DeliveryPizza pizza)
    {
        return pizza.PickedUp ? 1f : 0.25f;
    }

    public void DeliverPizza(PizzaType pt)
    {
        if (CurrentWave.Delivery[_deliveryIndex].Type == pt)
        {
            CurrentWave.Delivery[_deliveryIndex].Delivered = true;
            SoundManager.Instance.PizzaGiven.Play();
            _deliveryIndex++;
            ScoreManager.Instance.PizzasDelivered += 1;
            _updateUiList();
            if (_stopwatch > 0f)
            {
                PlayerVitalsManager.Instance.Money += 25;
                HitmarkerManager.Instance.Tip();
            }
            else
            {
                PlayerVitalsManager.Instance.Money += 0;
            }
            
            if (_deliveryIndex >= CurrentWave.Delivery.Length)
            {
                FinishWave();
            }
        }
    }

    public void FinishWave()
    {
        _gameIsCurrentlyHappening = false;
        NextWaveLabel.enabled = true;
        TimeText.enabled = false;
        WaveNumber++;
        _updateUiList();
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
