using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullets;
    public GameObject hole;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) {        
            this.OnShoot();
            SoundManager.Instance.OnShoot();
        }
    }

    protected abstract void OnShoot();
}
