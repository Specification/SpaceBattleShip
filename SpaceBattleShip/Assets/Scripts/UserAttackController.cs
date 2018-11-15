using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserAttackController : MonoBehaviour {

    AttackController m_attackController;

    // 攻撃中判定フラグ
    bool m_isAttacking;

    //連続弱攻撃の何回目かを判断するフラグ
    bool m_attack1;     //1回目
    bool m_attack2;     //2回目

    // 攻撃インターバル用タイマー
    float m_attackIntervalTimer;

    [SerializeField]
    KeyCode m_attack1Key = KeyCode.RightControl;    //攻撃１
    [SerializeField]
    KeyCode m_attack2Key = KeyCode.RightAlt;        //攻撃２

    float m_attack1Timer;                           // 攻撃キーを押した時刻を記録
    float m_attack2Timer;                           // 攻撃キーを押した時刻を記録

    float m_beforeAttack;

    // Use this for initialization
    void Start () {
        m_attackController = GetComponent <AttackController>();
        m_attack1 = false;
        m_attack2 = false;
    }
	
	// Update is called once per frame
	void Update () {
        // 攻撃処理
        if (!m_isAttacking && m_attackIntervalTimer <= 0)
        {

            // 入力を受け付けた時刻を記録
            if (Input.GetKeyDown(m_attack1Key))
            {
                m_attack1Timer = Time.realtimeSinceStartup;
            }
            if (Input.GetKeyDown(m_attack2Key))
            {
                m_attack2Timer = Time.realtimeSinceStartup;
            }

            if (m_attack1Timer - m_beforeAttack > 1f)
            {
                Debug.Log("リセット");
                m_attack1 = false;
                m_attack2 = false;
            }

            if (Time.realtimeSinceStartup - m_attack1Timer < 0.2f)
            {
                UnityAction action = AttackFinished;

                if (!m_attack1)
                {
                    Debug.Log("Attack1");
                    m_attackController.PlayAnimation("Attack1", action);
                    m_attack1 = true;
                    m_beforeAttack = m_attack1Timer;
                }

                else if (m_attack1 && !m_attack2 && m_attack1Timer - m_beforeAttack <= 1f)
                {
                    Debug.Log("Attack2");
                    m_attackController.PlayAnimation("Attack2", action);
                    m_attack2 = true;
                    m_beforeAttack = m_attack1Timer;
                }

                else if (m_attack1 && m_attack2 && m_attack1Timer - m_beforeAttack <= 1f)
                {
                    Debug.Log("Attack3");
                    m_attackController.PlayAnimation("Attack3", action);
                    m_attack1 = false;
                    m_attack2 = false;
                }



                m_isAttacking = true;

            }

            else if (Time.realtimeSinceStartup - m_attack2Timer < 0.3f)
            {
                UnityAction action = AttackFinished;
                Debug.Log("Attack4");
                m_attackController.PlayAnimation("Attack4", action);
                m_isAttacking = true;
                m_attack2Timer = 0f;
            }
        }
        else
        {
            m_attackIntervalTimer -= Time.deltaTime;
        }
    }
    /// <summary>
    /// 攻撃が完了したら呼ばれる関数
    /// </summary>
    public void AttackFinished()
    {
        m_attackIntervalTimer = 0.1f;
        m_isAttacking = false;
    }
}

