using UnityEngine;

public class HPPurchase : ShopItem.ShopItemPurchase
{
	public float bonus = 0.05f;
	
	public override void PurchaseItem(GameObject player)
	{
		player.GetComponent<PlayerHP>().AddMaxHP(bonus);
	}
}