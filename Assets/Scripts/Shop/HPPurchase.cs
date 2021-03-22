using UnityEngine;

public class HPPurchase : ShopItem.ShopItemPurchase
{
	public float bonus = 5f;
	
	public override void PurchaseItem(GameObject player)
	{
		player.GetComponent<PlayerHP>().AddMaxHP(bonus);
	}
}