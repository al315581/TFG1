using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreP2Script : MonoBehaviour {

    public static int scoreP2;
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

        text.text = scoreP2.ToString();

    }
}
