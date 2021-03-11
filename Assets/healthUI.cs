﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthImage;
    public GameObject healthText;
    public GameObject player;
    
    private Image healthImageComponent;
    private Text healthTextComponent;
    private PlayerHP playerHP;
    private TrailRenderer gradientHelper;
    void Start()
    {
        healthImageComponent = healthImage.transform.GetComponent<Image>();
        gradientHelper = healthImage.transform.GetComponent<TrailRenderer>();
        healthTextComponent = healthText.transform.GetComponent<Text>();
        playerHP = player.transform.GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        healthTextComponent.text = playerHP.currentHP.ToString(CultureInfo.CurrentCulture);
        healthImageComponent.fillAmount = (playerHP.currentHP / playerHP.maxHP);
        healthImageComponent.color = gradientHelper.colorGradient.Evaluate((playerHP.currentHP / playerHP.maxHP));
    }
}
