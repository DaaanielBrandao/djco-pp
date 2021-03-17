using UnityEngine;
public class WeaponPurchase : ShopItem.ShopItemPurchase
{
	public GameObject weapon;
	
	public override void PurchaseItem(GameObject player) {
		
		player.GetComponentInChildren<WeaponSwitch>().AddWeapon(weapon);

		transform.parent.parent.GetComponent<ShopManager>().OnWeaponPurchase(weapon);
	}
}