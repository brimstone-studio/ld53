using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RobotHealth : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public Renderer RobotEyes;
    public AudioSource RobotSound;
    
    [ColorUsage(true, true)]
    public Color EmissionRed;
    
    public int Heath
    {
        get
        {
            return _health;
        }
        set
        {
            if (_health > 0)
            {
                _health = value;
                HitmarkerManager.Instance.Hitmark();
                if (_health <= 0)
                {
                    _die();
                }
            }
        }
    }

    [SerializeField]
    private int _health = 50;

    private void _die()
    {
        RobotEyes.material.SetColor("_EmissionColor", EmissionRed);
        RobotSound.Stop();
        NavMeshAgent.enabled = false;
        HitmarkerManager.Instance.RobotKill();
        Destroy(this.gameObject, 2f);
    }

    private void FixedUpdate()
    {
        if (!GamemodeManager.Instance.GameIsCurrentlyHappening)
        {
            _die();
        }
    }
}
