using System;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
	private float time = 0;

	private Text timerText;

	private void Start()
	{
		timerText = GetComponentInChildren<Text>();
	}

	private void Update()
	{
		time += Time.deltaTime;

		int milliseconds = (int)(time * 1000) % 1000;
		int seconds = (int) time % 60;
		int minutes = (int) time / 60;
		
		//	timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
		timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";

	}
}