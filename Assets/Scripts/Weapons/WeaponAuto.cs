using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAuto : Weapon
{
    // Update is called once per frame
    protected void Update()
    {  
        if(IsShooting()) {     
            OnDetectShoot();
        }
    }

    public bool IsShooting() {
        return Input.GetKey(KeyCode.L);
    }
}
