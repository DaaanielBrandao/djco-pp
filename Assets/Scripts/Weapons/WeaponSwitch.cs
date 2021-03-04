using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public float swapCooldown = 0.2f;
    public int pressedNumber = 1;
    
    private GameObject[] weapons = new GameObject[9];
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
        if (pressedNumber != newPressedNumber && newPressedNumber > 0 && newPressedNumber <= weapons.Length) {
            SelectWeapon(newPressedNumber);
            StartCoroutine(StartCooldown());
        }
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

    int GetPressedNumber() {
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

    public Weapon GetWeapon(string name) {
        foreach (GameObject obj in weapons) {
            if (obj.name == name)
                return obj.GetComponent<Weapon>();
        }
        return null;        
    }

    public void AddWeapon(GameObject weapon) { // Escolher slot? 
        if (IsAtMaxWeapons()) {
            Debug.LogWarning("Already at max weapons!");
            return;
        }

        weapons[numWeapons] = weapon;
        numWeapons++;
    }

    public bool IsAtMaxWeapons() {
        return numWeapons == 9;
    }
 
 
}
