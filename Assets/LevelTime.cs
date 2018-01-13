using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : MonoBehaviour {

    [SerializeField] float levelSeconds = 180f;
    [SerializeField] float secondsLeft;
    Text levelTimeText;

	// Use this for initialization
	void Start ()
    {
        secondsLeft = levelSeconds;
        levelTimeText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (secondsLeft >= 0)
        {
            secondsLeft -= Time.deltaTime;
            levelTimeText.text = "Time remaining: " + FormatLevelTime();
        }

	}

    public float GetSecondsLeft()
    {
        return secondsLeft;
    }


    public string FormatLevelTime()
    {
        string levelTime = "";

            TimeSpan t = TimeSpan.FromSeconds(Double.Parse(secondsLeft.ToString()));

            levelTime = string.Format("{0:D2}:{1:D2}",
                t.Minutes,
                t.Seconds
                );
        return levelTime;
    }
}
