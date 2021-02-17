using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject bullets;

    public GameObject hole;

    public float spreadAngle = 40;
    public int numBullets = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            Debug.Log("pow");
            for(int i = 0; i < numBullets; i++){
                float angle = Random.Range(-spreadAngle/2,spreadAngle/2);
                Instantiate(bullets,hole.transform.position, new Quaternion(90,angle,0,0));
            }
        }
    }
}
