using UnityEngine;

public class AmmoPurchase : ShopItem.ShopItemPurchase
{
	public float bonus = 0.05f;
	
	public override void PurchaseItem(GameObject player)
	{
		player.GetComponentInChildren<WeaponSwitch>().AddMaxAmmo(bonus);
	}
}