using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WalletUI : MonoBehaviour {
	
	public GameObject player;
	public GameObject walletText;
	
	private Text walletTextComponent;
	private Wallet playerWallet;

	private int coinsOld;
	
	void Start()
	{
		walletTextComponent = walletText.GetComponent<Text>();
		playerWallet = player.GetComponent<Wallet>();
		coinsOld = 0;
	}

	
	void Update()
	{
		if (coinsOld != playerWallet.coins) {

			StartCoroutine(CoinChangeAnimation(playerWallet.coins - coinsOld));
		}

		coinsOld = playerWallet.coins;
	}
	
	IEnumerator CoinChangeAnimation(int difference)
	{
		int numSteps = Math.Min(Math.Abs(difference), 10); // We want 1s animation, 0.1s steps
		int increment = difference / numSteps;
		int remainder = difference % numSteps;
		for (int i = 0; i < numSteps; i++)
		{
			walletTextComponent.text = (int.Parse(walletTextComponent.text) + increment).ToString();
			yield return new WaitForSeconds(0.1f);
		}

		walletTextComponent.text = (int.Parse(walletTextComponent.text) + remainder).ToString();
	}
}