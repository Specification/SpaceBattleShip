using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float m_maxHP = 100f;      //最大HP
    float m_hp;             //HP

    HPBar m_Hpbar;

    EnergyGauge m_gaugy;

    [SerializeField]
    GameObject m_target;

    [SerializeField]
    float m_maxEG = 100f;
    float m_energy;
    // Use this for initialization
    void Start ()
    {
        m_Hpbar = GetComponent<HPBar>();
        m_hp = 100f;
        
        m_gaugy = GetComponent<EnergyGauge>();
        m_energy = 100f;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(Input.GetKeyDown(KeyCode.S))
        {
            m_hp -= 5f;
            if (m_hp < 0)
            {
                m_hp = 0f;
            }

            m_Hpbar.Set(m_hp / m_maxHP);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            m_energy -= 5f;
            if(m_energy<0)
            {
                m_energy = 0f;
            }
            if(m_energy<m_maxEG)
            {
                Debug.Log("一回目のif文通った");
                m_energy +=1f * Time.deltaTime;
            }

            m_gaugy.Set(m_energy / m_maxEG);
        }
        

    }
}
