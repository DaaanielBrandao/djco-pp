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
    
    public void OnHit(float amount) {
        this.changeHP(-amount);
    }
}
