using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SkyController : MonoBehaviour
{
    public Gradient gradient;
    
    public int test = 0;
    private float currentTransitionTime = 0f;
    public float totalWaves = 9;

    public GameObject globalLight;
    public Gradient lightGradint;
    private Light2D skyLight;


    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        skyLight = globalLight.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (test > 0)
        {
            changeColor(test);
            test = 0;
        }
    }

    public void changeColor(int wave)
    {
        currentTransitionTime = 0;
        StartCoroutine(ColorChangeAnimation(wave));
    }
    
    IEnumerator ColorChangeAnimation(int wave)
    {
        
        for (int i = 0; i < 100; i++)
        {
            sprite.color = Color.Lerp(gradient.Evaluate((float)(wave - 1) / totalWaves), gradient.Evaluate((float)wave / totalWaves),
                currentTransitionTime);
            skyLight.color = Color.Lerp(lightGradint.Evaluate((float) (wave - 1) / totalWaves),
                lightGradint.Evaluate((float) wave / totalWaves),
                currentTransitionTime);
            yield return new WaitForSeconds(0.01f);
            currentTransitionTime += 0.01f;
        }
    }
}
