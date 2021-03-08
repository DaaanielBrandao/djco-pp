using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSemiAuto : Weapon
{
    // Update is called once per frame
    protected void Update()
    {  
        if(Input.GetKeyDown(KeyCode.L)) {     
            OnDetectShoot();
        }
    }
}
