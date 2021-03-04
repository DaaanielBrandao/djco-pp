using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public float cooldown;
    public AudioClip shootSound;
    public AudioClip noAmmoSound;
   
    public int maxAmmo;
    public int currentAmmo;

    protected bool canShoot = true;
    protected GameObject hole;

    private void Start() {
        hole = transform.Find("WeaponHole").gameObject;
    }

    void OnEnable() {
        canShoot = true;
    }

    protected abstract void Shoot();

    protected void OnDetectShoot() {
        if (currentAmmo <= 0) {
            // Sound effect
            return;
        }
        if (!canShoot)
            return;

        StartCoroutine(StartCooldown());
        Shoot();
        currentAmmo--;
            
        SoundManager.Instance.Play(shootSound);
        GetComponent<Animator>().SetTrigger("pew");
    }

    IEnumerator StartCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;        
    }

    public bool refillAmmo() {
        if(maxAmmo == currentAmmo)
            return false;
        currentAmmo = maxAmmo;
        
        Debug.Log("AMMO " + gameObject.name);
        return true;
    }
}
