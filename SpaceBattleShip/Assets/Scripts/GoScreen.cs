using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoScreen : MonoBehaviour
{
    [SerializeField]
    public float m_Time;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            transform.position += new Vector3(0, 1080*m_Time, 0);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            transform.position += new Vector3(0, -1080 * m_Time, 0);
        }
    }
}
