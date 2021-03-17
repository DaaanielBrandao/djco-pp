using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform canvas;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.parent.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rectTransform.position);

        //rectTransform.localPosition = new Vector2(200, 0);

        rectTransform.localPosition = new Vector2(canvas.rect.xMax, canvas.rect.yMax);
    }
}
