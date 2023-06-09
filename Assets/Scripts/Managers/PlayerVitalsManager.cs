using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerVitalsManager : MonoBehaviour
{
    public TMPro.TMP_Text HealthValueText;
    public TMPro.TMP_Text AmmoValueText;
    public TMPro.TMP_Text MoneyValueText;

    public Animator HealthAnimator;
    public Animator AmmoAnimator;

    public Canvas GameoverScreen;
    public Canvas GameOverlay;
    public Volume BlackAndWhitePostProcessing;

    public int StarterHealth = 100;
    public int StarterAmmo = 60;
    public int StarterMoney = 0;

    // public float RocketProjectileSpeed = 100000f;
    
    public int Health { get; set; }
    public int Ammo { get; set; }
    public int Money { get; set; }

    [NonSerialized]
    public bool PlayerDead = false;

    public static PlayerVitalsManager Instance;
    
    void Start()
    {
        Instance = this;
        Health = StarterHealth;
        Ammo = StarterAmmo;
        Money = StarterMoney;
    }

    void Update()
    {
        HealthValueText.text = Health.ToString();
        AmmoValueText.text = Ammo.ToString();
        MoneyValueText.text = Money.ToString();
    }

    public void HealthDecrease(int amount)
    {
        Health -= amount;
        AmmoAnimator.Play("Idle");
        HealthAnimator.SetTrigger("Pulse");
        HitmarkerManager.Instance.PlayerDamage();
        SoundManager.Instance.DamageTakenSound.Play();
        if (Health < 0)
        {
            // player death
            ScoreManager.Instance.UpdateScoreText();
            Health = 0;
            GameoverScreen.gameObject.SetActive(true);
            GameOverlay.gameObject.SetActive(false);
            BlackAndWhitePostProcessing.weight = 1;
            PlayerDead = true;
            SoundManager.Instance.PlayerDie.Play();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void AmmoDecrease(int amount)
    {
        Ammo -= amount;
        AmmoAnimator.Play("Idle");
        AmmoAnimator.SetTrigger("Pulse");
    }
}
