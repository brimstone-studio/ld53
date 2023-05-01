using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RobotHealth : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    
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
        NavMeshAgent.enabled = false;
        HitmarkerManager.Instance.RobotKill();
        Destroy(this.gameObject, 2f);
    }
}
