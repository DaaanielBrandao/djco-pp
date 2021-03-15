using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeStop : MonoBehaviour
{
    private static float pendingFreezeDuration = 0f;
    private static float timeDuration;

    private bool isfrozen = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingFreezeDuration > 0 && !isfrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public static void Freeze(float duration = 1f)
    {
        timeDuration = duration;
        pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        isfrozen = true;
        float original = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(timeDuration);
        Time.timeScale = original;
        pendingFreezeDuration = 0;
        isfrozen = false;
    }
}
