using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.L)){
            Debug.Log("pew");
            Instantiate(bullets,hole.transform.position,transform.rotation);
        }
    }
}
