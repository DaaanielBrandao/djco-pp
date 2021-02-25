using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;

    public ParticleSystem explosionEffect;

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("HP " + currentHP);
        if (currentHP <= 0) {
            // boom
            SoundManager.Instance.OnEnemyExplosion();
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        spriteRenderer.color = Color.Lerp(Color.gray, Color.white, currentHP / (float)maxHP);
    }

    public void Kill() {
        currentHP = 0;
    }

    public void changeHP(float amount) {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
    }

    public float percentageOfMax() {
        return currentHP / (float)maxHP;
    }    
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player Bullet")) {  
            float damage = other.gameObject.GetComponent<Bullet>().damage;
            this.changeHP(-damage);
        }
    }
}
