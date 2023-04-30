using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GamemodeManager : MonoBehaviour
{
    public TMPro.TMP_Text NextWaveLabel;
    public TMPro.TMP_Text WaveText;
    public TMPro.TMP_Text TimeText;
    
    public static GamemodeManager Instance;

    public List<Wave> WaveList = new List<Wave>()
    {
        new Wave(
            new[] {PizzaType.Red},
    60)
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

    private float _stopwatch;
    
    public void StartGame()
    {
        if (!_gameIsCurrentlyHappening)
        {
            _gameIsCurrentlyHappening = true;
            NextWaveLabel.enabled = false;
            TimeText.enabled = true;
            _stopwatch = CurrentWave.Time;
        }
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

            if (_stopwatch < 0)
            {
                float minutes = Mathf.FloorToInt(_stopwatch / 60);
                float seconds = Mathf.FloorToInt(_stopwatch % 60);

                TimeText.text = $"{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(8, '0')}";
            }
            else
            {
                TimeText.text = "<color=\"red\">00:00";
            }
        }
        
    }
}
