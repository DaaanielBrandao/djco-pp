using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    public float speed = 5.0f;
    public float minRange = 10;
    public float maxRange = 12;

    public float circleSpeed = 10.0f;

    private GameObject follow;

    private Vector2 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("Player");
        originalScale = transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = follow.transform.position - gameObject.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float distance = dir.magnitude;

        if (distance == 0 && minRange > 0)
            dir = new Vector2(0, 1);

        // Move
        if (distance > maxRange)
            dir = dir.normalized;
        else if (distance < minRange)
            dir = -dir.normalized;
        else {
            dir = Vector2.zero;
        }
        
        transform.Translate(dir * speed * Time.deltaTime, Space.World);


        // Rotate
        if (angle > 90 || angle < -90) {
            transform.localScale = new Vector2(-originalScale.x, originalScale.y);
            transform.rotation = Quaternion.Euler(0, 0, 180 + angle);
        }
        else {
            transform.localScale = originalScale;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Circle
        transform.RotateAround(follow.transform.position, Vector3.forward, circleSpeed / distance * Time.deltaTime);
    }
}
