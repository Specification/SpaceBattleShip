using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownSe : MonoBehaviour
{
    bool  Sound;

    void Start()
    {
        Sound = GetComponent<AudioSource>();
        Sound = true;
    }

    // Update is called once per frame
    void Update ()
    {
	}


}
