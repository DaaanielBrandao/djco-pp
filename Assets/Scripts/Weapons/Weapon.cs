using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float cooldown;
    public AudioClip shootSound;
    public AudioClip noAmmoSound;
    public float shakeMagnitude;
   
    public int maxAmmo;
    public int currentAmmo;
    public GameObject ammoBox;


    protected bool canShoot = true;
    protected GameObject hole;
    protected GameObject shooter;

    protected void Start() {
        shooter = transform.parent.parent.gameObject;
        hole = transform.Find("WeaponHole").gameObject;
    }

    protected virtual void OnEnable() {
        canShoot = true;
    }

    protected abstract void Shoot();

    protected void OnDetectShoot() {
        if (currentAmmo <= 0) {
            SoundManager.Instance.Play(noAmmoSound);
            return;
        }
        if (!canShoot)
            return;
        StartCoroutine(StartCooldown());
        Shoot();
        currentAmmo--;
        
        Camera.main.GetComponent<CameraShake>().Shake(0.06f,shakeMagnitude);
        SoundManager.Instance.Play(shootSound);
        GetComponent<Animator>().SetTrigger("pew");
    }

    IEnumerator StartCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;        
    }

    public bool RefillAmmo() {
        if(maxAmmo <= currentAmmo)
            return false;
        currentAmmo = maxAmmo;
        
        Debug.Log("AMMO " + gameObject.name);
        return true;
    }
    
    public void AddMaxAmmo(float percentage)
    {
        int increment = (int) Math.Round(maxAmmo * percentage);
        maxAmmo += increment;
        currentAmmo += increment;
    }
}
