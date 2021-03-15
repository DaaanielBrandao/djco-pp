using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCharge : Weapon
{
    
    public bool isCharging;
    public float totalCharge = 0f;
    public float chargeRate = 1f;
    
    public AudioClip chargingSound;

    // Update is called once per frame
    protected virtual void Update()
    {
        // Usar 9 milhoes de QI para o charge
        if (PauseMenu.isPaused) return;
        if(Input.GetKey(KeyCode.L))
        {
            if (!isCharging) {
                SoundManager.Instance.Play(chargingSound);
            }
            
            isCharging = true;
            totalCharge = Mathf.Min(1, totalCharge + Time.deltaTime * chargeRate);
            GetComponent<Animator>().SetTrigger("charge");
            
        } else if (isCharging) {
            OnDetectShoot();
            isCharging = false;
            totalCharge = 0;
            GetComponent<Animator>().ResetTrigger("charge");
        }
            
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        totalCharge = 0;
        isCharging = false;
        GetComponent<Animator>().ResetTrigger("charge");
    }
}
