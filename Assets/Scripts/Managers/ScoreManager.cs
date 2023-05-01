using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMPro.TMP_Text RobotsKilledText;
    public TMPro.TMP_Text PizzasDeliveredText;
    public TMPro.TMP_Text WaveText;
    public TMPro.TMP_Text MoneyText;

    private void Start()
    {
        Instance = this;
    }

    public int RobotsKilled = 0;
    public int PizzasDelivered = 0;

    public void UpdateScoreText()
    {
        RobotsKilledText.text = $"You killed <color=\"yellow\">{RobotsKilled}</color> robots";
        PizzasDeliveredText.text = $"By delivering <color=\"yellow\">{PizzasDelivered}</color> pizzas";
        WaveText.text = $"You made it to wave <color=\"yellow\">{GamemodeManager.Instance.WaveNumber}</color>";;
        MoneyText.text = $"And earned <color=\"yellow\">{PlayerVitalsManager.Instance.Money}</color> coins";
    }
}
