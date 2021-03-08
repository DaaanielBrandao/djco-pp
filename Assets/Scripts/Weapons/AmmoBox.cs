using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public AudioClip soundEffect;
    
    private Type weaponType;

    public void SetWeaponType(Type newWeaponType)
    {
        weaponType = newWeaponType;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (weaponType == null) {
            Debug.LogWarning("Weapon Type is null on " + name);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            //other.gameObject.transform.Find("Shotgun").GetComponent<Shotgun>().addAmmo();
            //float test = other.gameObject.transform.Find("Shotgun").GetComponent<Shotgun>().spreadAngle;
            //var instance = Activator.CreateInstance(weaponType,className);
            WeaponSwitch weaponSwitch = other.gameObject.GetComponentInChildren<WeaponSwitch>();
            if(weaponSwitch.GetWeapon(weaponType).refillAmmo()) {
                Destroy(gameObject);
                SoundManager.Instance.Play(soundEffect);
            }
        }
    }
}
