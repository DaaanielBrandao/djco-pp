using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSemiAuto : Weapon
{
    // Update is called once per frame
    protected virtual void Update()
    {
        if (PauseMenu.isPaused) return;
        if(Input.GetKeyDown(KeyCode.L)) {     
            OnDetectShoot();
        }
    }
}
