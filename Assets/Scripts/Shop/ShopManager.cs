using System;
using System.Collections;
using System.Linq;
using UnityEngine;


public class ShopManager : MonoBehaviour
{

	public GameObject[] shopWeapons;
	public GameObject hpPurchase;
	public GameObject ammoPurchase;

	private Transform[] weaponSlots;
	private Transform hpSlot;
	private Transform ammoSlot;

	
	private void Start()
	{
		int numSlots = transform.childCount;
		weaponSlots = new Transform[numSlots - 2];
		
		for (int i = 0; i < numSlots - 2; i++)
			weaponSlots[i] = transform.GetChild(i);

		hpSlot = transform.GetChild(numSlots - 2);
		ammoSlot = transform.GetChild(numSlots - 1);
		
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			RefreshShop();
	}

	public void RefreshShop()
	{
		ClearSlots();
		RefreshHpAmmo();
		RefreshWeapons();
	}

    private void ClearSlots() {
	    foreach (Transform shopSlot in weaponSlots) {
		    foreach (Transform child in shopSlot) {
			    Destroy(child.gameObject);
		    }
	    }
	    
	    foreach (Transform child in ammoSlot) {
		    Destroy(child.gameObject);
	    }
	    
	    foreach (Transform child in hpSlot) {
		    Destroy(child.gameObject);
	    }
	}
    
    private void RefreshHpAmmo() {
	    Instantiate(hpPurchase, hpSlot);
	    Instantiate(ammoPurchase, ammoSlot);
    }

	private void RefreshWeapons()
	{
		if (shopWeapons.Length == 0 || weaponSlots.Length == 0) {
			Debug.LogWarning("No Items or Slots! " + shopWeapons.Length + " " + weaponSlots.Length);
			return;
		}

		// nums will be the shop weapons indexes shuffled
		System.Random randomGenerator = new System.Random();
		int[] nums = Enumerable.Range(0, shopWeapons.Length).OrderBy(x => randomGenerator.Next()).ToArray();

		
		for (int i = 0; i < weaponSlots.Length && i < shopWeapons.Length; i++)
		{
			Instantiate(shopWeapons[nums[i]], weaponSlots[i]);
		}
	}

	public void OnWeaponPurchase(GameObject purchasedWeapon)
	{
		
		Debug.Log(shopWeapons.Length);
		Debug.Log(purchasedWeapon.name);
		shopWeapons = shopWeapons.Where(shopItem => shopItem.GetComponent<WeaponPurchase>().weapon != purchasedWeapon).ToArray();
		Debug.Log(shopWeapons.Length);
	}
}