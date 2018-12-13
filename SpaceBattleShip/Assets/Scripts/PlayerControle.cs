using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControle : MonoBehaviour
{
    [SerializeField]
    float m_maxHP = 100f;      //最大HP
    float m_hp;             //HP

    HPBar m_Hpbar;

    [SerializeField]
    GameObject m_target;

    // Use this for initialization
    void Start()
    {
        m_Hpbar = GetComponent<HPBar>();

        m_hp = 100f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            m_hp -= 5f;
            if (m_hp < 0)
            {
                m_hp = 0f;
            }

            m_Hpbar.Set(m_hp / m_maxHP);
        }

    }
}