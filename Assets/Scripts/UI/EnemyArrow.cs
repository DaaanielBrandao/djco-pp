using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyArrow : MonoBehaviour
{
    public float minRange = 10.0f;
    
    
    private RectTransform rectTransform;
    private GameObject arrow;

    public GameObject player;
    public EnemySpawnManager spawner;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        arrow = GetComponentInChildren<Image>(true).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rectTransform.position);

        //rectTransform.localPosition = new Vector2(200, 0);


        arrow.SetActive(false);

        if (spawner) {
            //Debug.Log("hi");
            GameObject enemy = GetFarEnemy(spawner.GetAliveObjects());

            if (enemy) {
                arrow.SetActive(true);
                PointTo(enemy);
            }
        }
    }

    private void PointTo(GameObject enemy) {
        Vector2 dir = (enemy.transform.position - player.transform.position).normalized;
        rectTransform.localPosition = dir * 300; // DONI
        rectTransform.rotation = Quaternion.Euler(0, 0,  Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private GameObject GetFarEnemy(List<GameObject> enemies)
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies) {
            if (!enemy)
                continue;
            float distance = Vector2.Distance(enemy.transform.position, player.transform.position);
            if (distance < minRange)
                return null;
            if (distance < minDistance) {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
