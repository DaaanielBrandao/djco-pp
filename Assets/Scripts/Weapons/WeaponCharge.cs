using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCharge : Weapon
{
    protected float chargabilitament;

    // Update is called once per frame
    void Update()
    {
        // Usar 9 milhoes de QI para o charge
        if(Input.GetKey(KeyCode.L)) {     
            OnDetectShoot();
        }  
    }
}
