using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerManager : MonoBehaviour
{
    public static HitmarkerManager Instance;
    public Animator HitmarkerAnimator;
    public Animator RobotKillAnimator;
    public Animator DamageAnimator;
    
    private void Start()
    {
        Instance = this;
    }

    public void Hitmark()
    {
        HitmarkerAnimator.SetTrigger("Hit");
    }
    
    public void RobotKill()
    {
        RobotKillAnimator.SetTrigger("Hit");
    }
    
    public void PlayerDamage()
    {
        DamageAnimator.SetTrigger("Hit");
    }
}
