using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject bullets;
    public GameObject hole;

    public float cooldown;
    public bool canShoot = true;

    public AudioClip shootSound;

    public int magSize;
    public int numOfBullets;
    
    void OnEnable() {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {  
        if (!canShoot)
            return;

        if(Input.GetKeyDown(KeyCode.L) && numOfBullets > 0) {     
            StartCoroutine(StartCooldown());
            this.OnShoot();
            numOfBullets--;
            
            SoundManager.Instance.Play(shootSound);
            GetComponent<Animator>().SetTrigger("pew");
        }
    }

    IEnumerator StartCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;        
    }

    protected void SpawnBullet(Vector3 position, Quaternion rotation) {
        GameObject obj = Instantiate(bullets, position, rotation);
        obj.GetComponent<Bullet>().shooter = gameObject.transform.parent.parent.gameObject;
    }

    protected abstract void OnShoot();

    public abstract bool addAmmo();
}
