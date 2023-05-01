using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource DamageTakenSound;
    public AudioSource RocketShot;
    public AudioSource PizzaGiven;
    public AudioSource Pickup;
    public AudioSource PlayerDie;

    public static SoundManager Instance;

    private void Start()
    {
        Instance = this;
    }
}
