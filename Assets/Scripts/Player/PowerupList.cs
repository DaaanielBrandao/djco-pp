using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupList : MonoBehaviour
{
    public class PowerupItem
    {
        public readonly string name;
        public float timeLeft;
        public Sprite hatSprite;

        public PowerupItem(string name, float duration, Sprite hatSprite)
        {
            this.name = name;
            this.timeLeft = duration;
            this.hatSprite = hatSprite;
        }
        
        public PowerupItem(Powerup powerup) : this(powerup.powerUpName, powerup.duration,powerup.hatSprite) { }
    }
    
    private Dictionary<string, PowerupItem> powerUps = new Dictionary<string, PowerupItem>();

    private SpriteRenderer powerUpHat;
    private ParticleSystem powerUpHatPS;
    
    void Start()
    {
        powerUpHat = transform.Find("PowerupHat").GetComponent<SpriteRenderer>();
        powerUpHatPS = powerUpHat.transform.Find("PowerupHatPS").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Got " + powerUps.Count + "  powerups");
        foreach (PowerupItem powerup in powerUps.Values) {
            powerup.timeLeft -= Time.deltaTime;
        }
        powerUps = powerUps.Where(powerup => powerup.Value.timeLeft > 0).ToDictionary(pair => pair.Key,
            pair => pair.Value);

        powerUpHat.sprite = powerUps.Count > 0 ? powerUps.First().Value.hatSprite : null;
        if (powerUps.Count > 0)
        {
            Debug.Log("olas");
            if(!powerUpHatPS.isPlaying)
                powerUpHatPS.Play();
        }
        else if(powerUpHatPS.isPlaying)
            powerUpHatPS.Stop();
    }
    
    public bool HasPowerup(string powerup)
    {
        return powerUps.ContainsKey(powerup);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Powerup"))
        {
            Powerup powerup = other.GetComponent<Powerup>();
            if (HasPowerup(powerup.powerUpName))
            {
                PowerupItem item = powerUps[powerup.powerUpName];
                item.timeLeft = Math.Max(item.timeLeft, powerup.duration);
            }
            else powerUps.Add(powerup.powerUpName, new PowerupItem(powerup));
        }
    }
}
