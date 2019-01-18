using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMortion : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_characterPrefabs;
	// Use this for initialization
	void Start ()
    {
        int m_winner = PlayerPrefs.GetInt("win", 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
