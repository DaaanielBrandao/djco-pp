using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 5;
    public float currentHP;

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.Lerp(Color.gray, Color.white, currentHP / (float)maxHP);
    }

    public void changeHP(float amount) {
        currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
    }

    public float percentageOfMax() {
        return currentHP / (float)maxHP;
    }    
}
