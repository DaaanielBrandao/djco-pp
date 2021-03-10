using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupList : MonoBehaviour
{
    public class PowerupItem
    {
        public float timeLeft;
        public Powerup original;

        public PowerupItem(Powerup powerup)
        {
            original = powerup;
            timeLeft = powerup.duration;
        }
    }
    
    private Dictionary<string, PowerupItem> powerUps = new Dictionary<string, PowerupItem>();

    private SpriteRenderer powerUpHat;
    private ParticleSystem powerUpHatPS;
    
    void Start()
    {
        powerUpHat = transform.Find("PowerupHat").GetComponent<SpriteRenderer>();
        powerUpHatPS = transform.Find("PowerupHat").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdatePowerupList();
        UpdateSprite();

    }

    private void UpdatePowerupList()
    {
       // Debug.Log("Got " + powerUps.Count + "  powerups");
        foreach (PowerupItem powerup in powerUps.Values) {
            powerup.timeLeft -= Time.deltaTime;
        }
        powerUps = powerUps.Where(powerup => powerup.Value.timeLeft > 0)
            .ToDictionary(pair => pair.Key,pair => pair.Value);
    }

    private void UpdateSprite()
    {
        if (powerUps.Count > 0)
        {
            Powerup powerup = powerUps.First().Value.original;
            powerUpHat.sprite = powerup.hatSprite;
            
            var psMain = powerUpHatPS.main;
            psMain.startColor = powerup.particleColor;
            

            if (!powerUpHatPS.isPlaying) {
                powerUpHatPS.Play();
            }
        }
        else {
            powerUpHat.sprite = null;

            if (powerUpHatPS.isPlaying)
                powerUpHatPS.Stop();
        }

    }
    
    public bool HasPowerup(string powerup)
    {
        return powerUps.ContainsKey(powerup);
    }

    public PowerupItem GetPowerup(string powerup)
    {
        return powerUps[powerup];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Powerup"))
        {
            Powerup powerup = other.GetComponent<Powerup>();
            if (HasPowerup(powerup.powerUpName))
            {
                PowerupItem item = GetPowerup(powerup.powerUpName);
                item.timeLeft = Math.Max(item.timeLeft, powerup.duration);
            }
            else powerUps.Add(powerup.powerUpName, new PowerupItem(powerup));

            powerup.Explode();
        }
    }
}
