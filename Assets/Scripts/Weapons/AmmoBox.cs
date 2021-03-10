using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public AudioClip soundEffect;
    
    private string weaponName;

    public void SetWeaponName(string newWeaponName)
    {
        weaponName = newWeaponName;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (weaponName == null) {
            Debug.LogWarning("Weapon Name is null on " + name);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            //other.gameObject.transform.Find("Shotgun").GetComponent<Shotgun>().addAmmo();
            //float test = other.gameObject.transform.Find("Shotgun").GetComponent<Shotgun>().spreadAngle;
            //var instance = Activator.CreateInstance(weaponType,className);
            WeaponSwitch weaponSwitch = other.gameObject.GetComponentInChildren<WeaponSwitch>();
            if(weaponSwitch.GetWeapon(weaponName).RefillAmmo()) {
                Destroy(gameObject);
                SoundManager.Instance.Play(soundEffect);
            }
        }
    }
}
