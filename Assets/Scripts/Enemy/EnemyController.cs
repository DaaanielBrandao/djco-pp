using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private MovementBehavior movementBehavior;
    private AttackBehavior attackBehavior;
    private StaggerBehavior staggerBehavior;

    // Start is called before the first frame update
    void Start()
    {
        movementBehavior = GetComponent<MovementBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
        staggerBehavior = GetComponent<StaggerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        movementBehavior.enabled = !staggerBehavior.IsStaggered();
        attackBehavior.enabled = !staggerBehavior.IsStaggered();
    }
}
