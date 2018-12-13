using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickedTitle()
    {
        SceneManager.LoadScene("");
    }

    public void ClickedCharacterSelect()
    {
        SceneManager.LoadScene("");
    }

    public void ClickedOneMore()
    {
        SceneManager.LoadScene("");
    }
}
