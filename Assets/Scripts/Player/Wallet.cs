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
        lifetimeCoins = 0;
    }

    public void Deposit(int numCoins)
    {
        coins += numCoins;
        lifetimeCoins += numCoins;
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
