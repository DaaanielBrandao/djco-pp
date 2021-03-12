using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{

    public int coins { get; private set; }
    public int uiCoins { get; private set; }
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

        StartCoroutine(DepositAnimation(numCoins));
    }

    public void Withdraw(int numCoins)
    {
        coins -= numCoins;
        uiCoins -= numCoins;
        // kaching?
    }

    IEnumerator DepositAnimation(int numCoins) {
        for (int i = 0; i < numCoins; i++)
        {
            SoundManager.Instance.Play(ding);
            uiCoins++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    
    
    
}
