using UnityEngine;
public class WeaponPurchase : ShopItem.ShopItemPurchase
{
	public GameObject weapon;
	
	public override void PurchaseItem(GameObject player) {
		Debug.Log("Added weapon!");
		
		player.GetComponentInChildren<WeaponSwitch>().AddWeapon(weapon);
	}
}