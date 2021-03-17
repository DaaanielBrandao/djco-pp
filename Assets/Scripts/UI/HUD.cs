using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject announcementsWave;
    public GameObject announcementsHour;
    public GameObject announcementEndWave;
    private Text hourText;
    private Text waveText;
    private Text endText;
    public int test = 0;
    void Start()
    {
        hourText = announcementsHour.GetComponent<Text>();
        waveText = announcementsWave.GetComponent<Text>();
        endText = announcementEndWave.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (test > 0)
        {
            announceWaveStart(test);
            test = 0;
        }
    }

    public void announceWaveStart(int waveNum)
    {
        waveText.text = "Wave " + waveNum;
        hourText.text = getHourOnWave(waveNum);
        Color tmp1 = waveText.color;
        Color tmp2 = hourText.color;
        tmp1.a = 1;
        tmp2.a = 1;
        waveText.color = tmp1;
        hourText.color = tmp2;
        StartCoroutine(fadeOutText(waveText));
        StartCoroutine(fadeOutText(hourText));
    }

    public void announceEndWave()
    {
        Color tmp1 = endText.color;
        tmp1.a = 1;
        endText.color = tmp1;
        StartCoroutine(fadeOutText(endText));
    }

    private static string getHourOnWave(int waveNum)
    {
        if (waveNum < 3)
        {
            return (waveNum + 9) + ":00 PM";
        }
        else return "0" + (waveNum - 3) + ":00 AM";
    }

    static IEnumerator fadeOutText(Text text)
    {
        Color tmp = text.color;
        while (tmp.a > 0)
        {
            tmp.a = Math.Max(0, tmp.a - 0.006f);
            text.color = tmp;
            yield return new WaitForSeconds (0.01f);
        }
    }
}
