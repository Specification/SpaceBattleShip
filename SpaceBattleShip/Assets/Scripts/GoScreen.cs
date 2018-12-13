using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoScreen : MonoBehaviour
{
    [SerializeField]
    public float m_Time;

    GoScreen m_goScreen;


    [SerializeField]
    SelectManager m_selectManager;

	// Use this for initialization
	void Start ()
    {
        m_selectManager.enabled = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            transform.position += new Vector3(0, 1080 * m_Time, 0);
            m_selectManager.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            transform.position += new Vector3(0, -1080 * m_Time, 0);
            m_selectManager.RemoveCharacters();
            m_selectManager.enabled = false;
        }
    }
}
