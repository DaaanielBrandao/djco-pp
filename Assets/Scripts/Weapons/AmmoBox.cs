using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public string weaponType;
    public AudioClip soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    private void OnTriggerEnter2D(Collider2D other) {        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
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
