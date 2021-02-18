using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullets;
    public GameObject hole;
    public float cooldown;
    public float currentCd = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(currentCd > 0){
            currentCd -= Time.deltaTime;
        } else if(Input.GetKeyDown(KeyCode.L)) {        
            this.OnShoot();
            currentCd = cooldown;

            SoundManager.Instance.OnShoot();
            //GameObject.FindObjectOfType<Camera>().GetComponent<Animator>().SetTrigger("shake"); é capaz de ficar melhor qnd se mata alguem
        }
    }

    protected abstract void OnShoot();
}
