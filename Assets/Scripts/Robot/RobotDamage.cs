using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDamage : MonoBehaviour
{
    public RobotHealth RobotHealth;
    
    [NonSerialized]
    public Transform player;
    public float damageDistance = 2f;
    public float damageFrequency = 2f;
    public int damageAmount = 25;

    private float _timeSinceLastDamage;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        _timeSinceLastDamage = 0f;
    }

    void Update()
    {
        _timeSinceLastDamage += Time.deltaTime;

        if (_timeSinceLastDamage >= 1f / damageFrequency && RobotHealth.Heath > 0)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= damageDistance)
            {
                PlayerVitalsManager.Instance.HealthDecrease(damageAmount);
            }

            _timeSinceLastDamage = 0f;
        }
    }
}
