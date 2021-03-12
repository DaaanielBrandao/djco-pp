using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    
    public Gradient ammoTextGradient;

    public GameObject weaponSwitcher;
    public GameObject currentAmmoText;
    public GameObject maxAmmoText;

    private WeaponSwitch weaponSwitch;
    private Text currentAmmoTextComponent;
    private Text maxAmmoTextComponent;
    // Start is called before the first frame update
    void Start()
    {
        weaponSwitch = weaponSwitcher.GetComponent<WeaponSwitch>();
        currentAmmoTextComponent = currentAmmoText.GetComponent<Text>();
        maxAmmoTextComponent = maxAmmoText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        maxAmmoTextComponent.text = weaponSwitch.getMaxAmmo().ToString();
        currentAmmoTextComponent.text = weaponSwitch.getCurrentAmmo().ToString();
        Debug.Log((weaponSwitch.getCurrentAmmo() / weaponSwitch.getMaxAmmo()));
        currentAmmoTextComponent.color = ammoTextGradient.Evaluate(((float)weaponSwitch.getCurrentAmmo() / (float)weaponSwitch.getMaxAmmo()));
    }
}
