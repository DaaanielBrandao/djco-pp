using UnityEngine;
public class WeaponPurchase : ShopItem.ShopItemPurchase
{
	private GameObject weapon;
	
	public override void PurchaseItem(GameObject player) {
		Debug.Log("Added weapon!");
	}
}