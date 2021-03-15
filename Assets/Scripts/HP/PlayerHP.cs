using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HealthBar
{    
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    public int dashKillHeal = 5;
    

    public override void Die() {
        SoundManager.Instance.Play(explosionSound);
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Camera.main.transform.parent=null;
        Destroy(gameObject); 
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
        Camera.main.GetComponent<cameraShake>().Shake(0.05f,0.03f);
        timeStop.Freeze(0.05f);
    }

    public void healDashKill()
    {
        ChangeHp(dashKillHeal);
    }
}
