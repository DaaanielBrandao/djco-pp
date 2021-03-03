using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start () {
        Animator anim = GetComponent<Animator>();
 
        int pickAnumber = Random.Range(1,4);//exclusive never prints the last only goes 1 to 2
        //Debug.Log (pickAnumber);
 
//randJumpInt is the parameter in animator
//pickAnumber random number from 1 to 2 from above
        anim.SetInteger ("random", pickAnumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
