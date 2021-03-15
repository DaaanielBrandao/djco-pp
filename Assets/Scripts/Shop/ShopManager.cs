using System;
using System.Collections;
using System.Linq;
using UnityEngine;


public class ShopManager : MonoBehaviour
{

	public GameObject[] shopWeapons;

	private Transform[] shopSlots;

	private void Start()
	{
		int numSlots = transform.childCount;
		shopSlots = new Transform[numSlots];
		for (int i = 0; i < numSlots; i++) {
			shopSlots[i] = transform.GetChild(i);
		}
		StartCoroutine(Test());

	}

	private void Update()
	{
	}

	IEnumerator Test()
	{
		while (true)
		{
			ClearSlots();
			RefreshWeapons();
			yield return new WaitForSeconds(5.0f);
		}
	}

	private void ClearSlots() {
		foreach (Transform shopSlot in shopSlots) {
			foreach (Transform child in shopSlot) {
				Destroy(child.gameObject);
			}
		}
	}

	private void RefreshWeapons()
	{
		if (shopWeapons.Length == 0 || shopSlots.Length == 0) {
			Debug.LogWarning("No Items or Slots! " + shopWeapons.Length + " " + shopSlots.Length);
			return;
		}



		// nums will be the shop weapons indexes shuffled
		System.Random randomGenerator = new System.Random();
		int[] nums = Enumerable.Range(0, shopWeapons.Length).OrderBy(x => randomGenerator.Next()).ToArray();

		
		for (int i = 0; i < shopSlots.Length; i++)
		{
			// i % nums.Length in case there are more slots than weapons
			Instantiate(shopWeapons[nums[i % nums.Length]], shopSlots[i]);
		}
	}
}