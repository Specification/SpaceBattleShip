using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float m_maxHP = 100f;      //最大HP
    float m_hp;             //HP

    HPBar m_Hpbar;


    // Use this for initialization
    void Start ()
    {
        m_Hpbar = GetComponent<HPBar>();

        m_hp = 100f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_hp -= 10f;
        if(m_hp<0)
        {
            m_hp = 0f;
        }

        m_Hpbar.Set(m_hp / m_maxHP);
	}
}
