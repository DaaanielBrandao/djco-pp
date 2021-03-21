using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;

    public bool isDead = false;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHP <= 0 && !isDead) {
            Die();
            isDead = true;
        }
    }
    
    public abstract void Die();

    public void RefillHp() {
        currentHP = maxHP;
    }

    public void SetHpPercentage(float percentage) {
        currentHP = maxHP * percentage;
    }

    protected void ChangeHp(float amount) {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
    }

    public float PercentageOfMax() {
        return currentHP / maxHP;
    }

    public void AddMaxHPPercentage(float percentage)
    {
        int increment = (int) Math.Round(maxHP * percentage);
        maxHP += increment;
        currentHP += increment;
    }
    public void AddMaxHP(float increment)
    {
        maxHP += increment;
        currentHP += increment;
    }
}
