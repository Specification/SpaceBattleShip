using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode )
    {
        transform.position += new Vector3(0, 1080f, 0);
    }
}
