using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public static float matchTime;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        text.text = Mathf.RoundToInt(matchTime).ToString();
		
	}
}
