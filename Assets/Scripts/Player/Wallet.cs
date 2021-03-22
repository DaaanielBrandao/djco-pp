using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{

    public int coins;
    public int lifetimeCoins { get; private set; }

    public AudioClip ding;
    
    // Start is called before the first frame update
    private void Start()
    {
        OnResetGame();
    }

    private void Update()
    {
        
    }

    private void OnResetGame() {
        coins = 0;
        if ((PlayerPrefs.GetInt("GMEMode", 0) == 1))
        {
            Debug.Log("stonks");
            coins = 9999;
        }
        lifetimeCoins = 0;
        GameInfo.score = lifetimeCoins;
    }

    public void Deposit(int numCoins)
    {
        coins += numCoins;
        lifetimeCoins += numCoins;
        GameInfo.score = lifetimeCoins;
    }

    public void Withdraw(int numCoins)
    {
        coins -= numCoins;
    }


    public bool CanAfford(int itemCost)
    {
        return coins >= itemCost;
    }
}
