using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHP <= 0) {
            Die();
        }
    }
    
    public abstract void Die();

    public void refillHP() {
        currentHP = maxHP;
    }

    public void setHPPercentage(float perc) {
        currentHP = maxHP * perc;
    }

    protected void changeHP(float amount) {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
    }

    public float percentageOfMax() {
        return currentHP / (float)maxHP;
    }
}
