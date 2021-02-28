using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject green;
    public GameObject yellow;
    public GameObject red;

    private SpriteRenderer greenRenderer;
    private SpriteRenderer yellowRenderer;
    private SpriteRenderer redRenderer;

    private GameObject  lightGreen;
    private GameObject  lightYellow;
    private GameObject  lightRed;
    public int switchertester3000 = 0;
    // Start is called before the first frame update
    void Start()
    {
        greenRenderer = green.GetComponent<SpriteRenderer>();
        yellowRenderer = yellow.GetComponent<SpriteRenderer>();
        redRenderer = red.GetComponent<SpriteRenderer>();
        lightGreen = green.transform.GetChild (0).gameObject;
        lightYellow = yellow.transform.GetChild (0).gameObject;
        lightRed = red.transform.GetChild (0).gameObject;
        turnGreen();
    }

    // Update is called once per frame
    void Update()
    {
        //testing stuff
        if(Input.GetKeyDown(KeyCode.O)){
            switchertester3000 = (switchertester3000 + 1) % 3;
            Debug.Log(switchertester3000);
        }
        if(switchertester3000 == 0)
            turnGreen();
        if(switchertester3000 == 1)
            turnYellow();
        if(switchertester3000 == 2)
            turnRed();
    }
    private void turnOffAll(){
        greenRenderer.color = UnityEngine.Color.grey;
        yellowRenderer.color = UnityEngine.Color.grey;
        redRenderer.color = UnityEngine.Color.grey;
        lightGreen.SetActive(false);
        lightYellow.SetActive(false);
        lightRed.SetActive(false);
    }

    public void turnGreen(){
        turnOffAll();
        greenRenderer.color = UnityEngine.Color.white;
        lightGreen.SetActive(true);
    }
    public void turnYellow(){
        turnOffAll();
        yellowRenderer.color = UnityEngine.Color.white;
        lightYellow.SetActive(true);
    }
    public void turnRed(){
        turnOffAll();
        redRenderer.color = UnityEngine.Color.white;
        lightRed.SetActive(true);
    }

}
