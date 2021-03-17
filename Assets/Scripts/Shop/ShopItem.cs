using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public abstract class ShopItemPurchase : MonoBehaviour
    {
        public abstract void PurchaseItem(GameObject player);
    }
    
    // Start is called before the first frame update


    public int itemCost;
    public AudioClip kaching;
    public AudioClip error;
    
    
    private TextMeshProUGUI itemCostText;
    private Transform costUI;
    private GameObject tooltip;
    
    private Vector2 originalScale;
    private Animator animator;
    private ShopItemPurchase purchase;
    
    private GameObject player;

    private void Start()
    {
        costUI = transform.Find("Cost");
        itemCostText = GetComponentInChildren<TextMeshProUGUI>();
        purchase = GetComponent<ShopItemPurchase>();
        animator = GetComponent<Animator>();
        tooltip = transform.Find("Tooltip").gameObject;
        tooltip.SetActive(false);


        originalScale = costUI.localScale;
    }

    // Update is called once per frame
    private void Update()
    {

        UpdateUI();
        HandleBuy();



    }

    private void UpdateUI()
    {
        itemCostText.text = itemCost.ToString();
        costUI.localScale = originalScale * (player ? 1.5f : 1); // eu queria fazer um coiso tipo e tal

        bool isHovered = player;
        bool cantAfford = player && !player.GetComponent<Wallet>().CanAfford(itemCost);
        
        animator.SetBool("Hover", isHovered);
        animator.SetBool("NoMoney", cantAfford);
        
        tooltip.SetActive(player);

    }

    private void HandleBuy()
    {
        if (player && Input.GetKeyDown(KeyCode.K)) {

            Wallet wallet = player.GetComponent<Wallet>();
            if (wallet.CanAfford(itemCost)) {
                SoundManager.Instance.Play(kaching);
                wallet.Withdraw(itemCost);
                purchase.PurchaseItem(player);
                Destroy(gameObject); // coin boom?
            }
            else SoundManager.Instance.Play(error);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            player = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            player = null;
        }
    }
}
