﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HealthBar
{    
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    public AudioClip getHitSound;
    public int dashKillHeal = 5;
    

    public override void Die() {
        SoundManager.Instance.Play(explosionSound);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        

        //Camera.main.transform.parent=null;
        Destroy(gameObject);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy Bullet")) {  
            float damage = other.gameObject.GetComponent<EnemyBullet>().damage;
            OnHit(damage);
            Destroy(other.gameObject);
        }
    }
    
    public void OnHit(float amount) {
        if (GetComponent<PowerupList>().HasPowerup("Invincibility"))
            return;
        
        ChangeHp(-amount);
        SoundManager.Instance.Play(getHitSound);
        Camera.main.GetComponent<CameraShake>().Shake(0.05f,0.03f);
        TimeStop.Freeze(0.05f);
    }

    public void healDashKill()
    {
        ChangeHp(dashKillHeal);
    }
}
