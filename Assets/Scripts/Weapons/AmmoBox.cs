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
            WeaponSwitch weaponSwitch = other.gameObject.GetComponentInChildren<WeaponSwitch>();
            weaponSwitch.GetWeapon(weaponName).RefillAmmo();
            Destroy(gameObject);
            SoundManager.Instance.Play(soundEffect);
        }
    }
}
