using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] Weapons;
    public int pressedNumber = 1;

    public float cooldown = 0.2f;
    public bool canSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon(pressedNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSwitch)
            return;

        int newPressedNumber = GetPressedNumber();
        if (pressedNumber != newPressedNumber && newPressedNumber > 0 && newPressedNumber <= Weapons.Length) {
            SelectWeapon(newPressedNumber);
            StartCoroutine(StartCooldown());
        }
    }
    
    IEnumerator StartCooldown() {
        canSwitch = false;
        yield return new WaitForSeconds(cooldown);
        canSwitch = true;        
    }

    public void SelectWeapon(int index){
        Weapons[pressedNumber - 1].SetActive(false);
        Weapons[index - 1].SetActive(true);
        pressedNumber = index;
    }

    public int GetPressedNumber() {
        for (int number = 1; number <= 9; number++) {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }
        
        return -1;
    }
 
}
