using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCharge : Weapon
{
    protected float chargabilitament;
    public bool isCharging;
    public float totalCharge = 0f;
    public float chargeRate = 1f;

    // Update is called once per frame
    void Update()
    {
        // Usar 9 milhoes de QI para o charge
        if(Input.GetKey(KeyCode.L))
        {
            isCharging = true;
            totalCharge = Mathf.Min(1, totalCharge + Time.deltaTime * chargeRate);
        } else if (isCharging)
        {
            OnDetectShoot();
            isCharging = false;
            totalCharge = 0;
        }
            
    }
}
