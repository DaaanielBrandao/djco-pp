using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : HealthBar
{
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;

    public override void Die() {
        SoundManager.Instance.Play(explosionSound);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player Bullet")) {  
            float damage = other.gameObject.GetComponent<Bullet>().damage;
            this.changeHP(-damage);
        }
    }
}
