﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreP1Script : MonoBehaviour {

    public static float scoreP1;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        text.text = Mathf.RoundToInt(scoreP1).ToString();

    }
}
