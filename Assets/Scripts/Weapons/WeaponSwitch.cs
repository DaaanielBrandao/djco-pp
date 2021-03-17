using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public float swapCooldown = 0.2f;
    public int pressedNumber = 1;
    
    private GameObject[] weapons = new GameObject[10];
    private int numWeapons;

    private bool canSwap = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Weapon[] list = GetComponentsInChildren<Weapon>(true);

        numWeapons = list.Length;
        for (int i = 0; i < list.Length; i++)
            weapons[i] = list[i].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSwap)
            return;


        int newPressedNumber = GetPressedNumber();
        if (pressedNumber != newPressedNumber && newPressedNumber > 0 && newPressedNumber <= weapons.Length && weapons[newPressedNumber - 1]) {
            SelectWeapon(newPressedNumber);
            StartCoroutine(StartCooldown());
        }
        //le testing
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    Debug.Log(1 + ((pressedNumber ) % weapons.Length) + "  " + weapons.Length);
        //    SelectWeapon( 1 + (pressedNumber) % weapons.Length );
        //    StartCoroutine(StartCooldown());
       // }
            
    }
    
    IEnumerator StartCooldown() {
        canSwap = false;
        yield return new WaitForSeconds(swapCooldown);
        canSwap = true;        
    }

    void SelectWeapon(int number){
        weapons[pressedNumber - 1].SetActive(false);
        weapons[number - 1].SetActive(true);
        pressedNumber = number;
    }

    static int GetPressedNumber() {
        for (int number = 1; number <= 9; number++) {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }
        return -1;
    }

    public GameObject GetSelectedWeapon() {
        return weapons[pressedNumber - 1];        
    }

    public Weapon[] GetWeapons() {
        return weapons.Take(numWeapons).Select(obj => obj.GetComponent<Weapon>()).ToArray();
    }

    public Weapon GetWeapon(string weaponName) {
        foreach (Weapon weapon in GetWeapons()) {
            if (weapon.name == weaponName)
                return weapon;
        }
        return null;        
    }

    public void AddWeapon(GameObject prefab) { // Escolher slot? 
        if (IsAtMaxWeapons()) {
            Debug.LogWarning("Already at max weapons!");
            return;
        }

        GameObject weapon = Instantiate(prefab, transform);
        

        weapons[numWeapons] = weapon;
        numWeapons++;
        SelectWeapon(numWeapons);
    }

    private bool IsAtMaxWeapons() {
        return numWeapons == 9;
    }

    public int getCurrentAmmo()
    {
        return GetSelectedWeapon().GetComponent<Weapon>().currentAmmo;
    }
    public int getMaxAmmo()
    {
        return GetSelectedWeapon().GetComponent<Weapon>().maxAmmo;
    }
    public void RefillAmmo() {
        foreach (GameObject weapon in weapons) { 
            if(weapon != null)
                weapon.GetComponent<Weapon>().RefillAmmo();
        }
    }
    
    public void AddMaxAmmo(float percentage) {
        for (int i = 0; i < numWeapons; i++) {
            weapons[i].GetComponent<Weapon>().AddMaxAmmo(percentage);
        }
    }
 
 
}
