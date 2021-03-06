﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Button m_YourButton, m_YourSecondButton;


    // Use this for initialization
    void Start () {
        GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("TitleTheme");
        Button btn = m_YourButton.GetComponent<Button>();
        Button btn2 = m_YourSecondButton.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayButtonPressed()
    {
        print("PLAY");
        GameObject.FindObjectOfType<AudioManager>().StopSound("TitleTheme");
        GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("ButtonPressed");

        GameObject.FindObjectOfType<LevelChanger>().FadeToNextLevel();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
