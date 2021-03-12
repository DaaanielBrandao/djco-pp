using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class WalletUI : MonoBehaviour {
	
	public GameObject player;
	public GameObject walletText;
	
	private Text walletTextComponent;
	private Wallet playerWallet;
	void Start()
	{
		walletTextComponent = walletText.GetComponent<Text>();
		playerWallet = player.GetComponent<Wallet>();
	}

	
	void Update()
	{
		walletTextComponent.text = playerWallet.uiCoins.ToString(CultureInfo.CurrentCulture);
	}
}