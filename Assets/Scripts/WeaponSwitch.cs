using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Weapons;
    public int pressedNumber = 1;
    void Start()
    {
        SelectWeapon(pressedNumber);
    }

    // Update is called once per frame
    void Update()
    {
        pressedNumber = GetPressedNumber();
        if(pressedNumber > 0 && pressedNumber <= Weapons.Length)
            SelectWeapon(pressedNumber);
    }
    public void SelectWeapon(int index){
        for (int i=0; i<Weapons.Length; i++)
        {
            if (i == (index - 1))
                Weapons[i].SetActive(true);
            else
                Weapons[i].SetActive(false);
        }
    }

    public int GetPressedNumber() {
    for (int number = 0; number <= 9; number++) {
        if (Input.GetKeyDown(number.ToString()))
            return number;
    }
 
    return -1;
}
 
}
