using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class matchFinished : MonoBehaviour
{
    public PlayerController PL1;
    public PlayerController PL2;

    public Text FinishedText;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        PL1.enabled = false;
        PL2.enabled = false;
        FinishedText.text="Finished";
	}
}
