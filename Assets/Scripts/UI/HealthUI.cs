using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthImage;
    public GameObject healthText;
    public GameObject damageImage;
    public GameObject player;
    public Gradient hpBarGradient;
    public float decreaseRate = 1f;
    
    private Image healthImageComponent;
    private Image damageImageComponent;
    private Text healthTextComponent;
    private PlayerHP playerHP;
    void Start()
    {
        healthImageComponent = healthImage.transform.GetComponent<Image>();
        damageImageComponent = damageImage.transform.GetComponent<Image>();
        healthTextComponent = healthText.transform.GetComponent<Text>();
        playerHP = player.transform.GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        healthTextComponent.text = playerHP.currentHP.ToString(CultureInfo.CurrentCulture);
        healthImageComponent.fillAmount = (playerHP.currentHP / playerHP.maxHP);
        healthImageComponent.color = hpBarGradient.Evaluate((playerHP.currentHP / playerHP.maxHP));

        if (healthImageComponent.fillAmount < damageImageComponent.fillAmount)
            damageImageComponent.fillAmount = Mathf.Max(damageImageComponent.fillAmount - decreaseRate * Time.deltaTime,
                healthImageComponent.fillAmount);
        if (healthImageComponent.fillAmount > damageImageComponent.fillAmount)
            damageImageComponent.fillAmount = healthImageComponent.fillAmount;
    }
}
