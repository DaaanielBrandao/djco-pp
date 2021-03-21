using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject healthImage;
    public GameObject healthText;
    public GameObject damageImage;
    
    public Gradient hpBarGradient;
    public float decreaseRate = 1f;
    
    private Image healthImageComponent;
    private Image damageImageComponent;
    private Text healthTextComponent;
    private HealthBar healthBar;
    void Start()
    {
        healthImageComponent = healthImage.transform.GetComponent<Image>();
        damageImageComponent = damageImage.transform.GetComponent<Image>();
        healthTextComponent = healthText.transform.GetComponent<Text>();
        healthBar = transform.parent.parent.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        healthTextComponent.text = healthBar.currentHP + " / " + healthBar.maxHP;
        healthImageComponent.fillAmount = healthBar.PercentageOfMax();
        healthImageComponent.color = hpBarGradient.Evaluate(healthBar.PercentageOfMax());

        if (healthImageComponent.fillAmount < damageImageComponent.fillAmount)
            damageImageComponent.fillAmount = Mathf.Max(damageImageComponent.fillAmount - decreaseRate * Time.deltaTime,
                healthImageComponent.fillAmount);
        if (healthImageComponent.fillAmount > damageImageComponent.fillAmount)
            damageImageComponent.fillAmount = healthImageComponent.fillAmount;
    }
}

