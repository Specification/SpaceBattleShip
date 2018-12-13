using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    GameObject m_hitEffect;

    public float m_efectTimer;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        GameObject effect = Instantiate(m_hitEffect, col.ClosestPointOnBounds(transform.position), transform.rotation);
        Destroy(effect, 1f);
    }
}
