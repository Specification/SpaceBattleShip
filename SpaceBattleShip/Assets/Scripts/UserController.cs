using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserController : MonoBehaviour
{

    Playercontroller m_playerController;

    // 走っているかフラグ
    bool m_isRunning;

    // 上キーが離された時間を記録
    float m_timeOfKeyUp;

    //攻撃インターバル用タイマー
    float m_attackIntervalTimer;
    //攻撃中判定フラグ
    bool m_isAttacking;
    [SerializeField]
    KeyCode m_forwardkey = KeyCode.W;   //前
    [SerializeField]
    KeyCode m_backkey = KeyCode.S;   //前
    [SerializeField]
    KeyCode m_rearkey = KeyCode.D;   //前
    [SerializeField]
    KeyCode m_frontkey = KeyCode.A;   //前
    [SerializeField]
    KeyCode m_upkey = KeyCode.C;   //前
    [SerializeField]
    KeyCode m_downkey = KeyCode.V;   //前
    [SerializeField]
    KeyCode m_attack1key = KeyCode.Q;   //前
    [SerializeField]
    KeyCode m_attack2key = KeyCode.E;   //前

    float m_attack1Timer;
    float m_attack2Timer;
    float m_forwardTimer;
    float m_backTimer;

    // Use this for initialization
    void Start()
    {
        m_playerController = GetComponent<Playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        //画面の右左いるどちらかで方向キーを切り替える
        KeyCode forwerdkey = m_forwardkey;
        KeyCode backey = m_backkey;
        if (m_playerController.Side == Playercontroller.SideState.Right)
        {
            forwerdkey = m_backkey;
            backey = m_forwardkey;

        }
        // 前後移動
        if (Input.GetKey(m_forwardkey))
        {
            if (!m_isRunning)   // 走っていない時
            {
                if (Time.time - m_timeOfKeyUp < 0.3f)
                {
                    // 前回キーが押されてから0.3秒未満にもう一度押されたなら走る
                    m_isRunning = true;
                }
            }

            if (m_isRunning)
            {
                m_playerController.Forward = 5.0f;  // 走っているなら
            }
            else
            {
                m_playerController.Forward = 1.0f;  // 歩いているなら
            }

            //            m_playerController.Forward = 1.0f * 5f; // アナログ値 * 移動量(m/s)←最大値
        }
        else if (Input.GetKey(m_backkey))
        {
            m_playerController.Back = 1.0f;
            m_isRunning = false;
        }
        else
        {
            m_isRunning = false;
        }

        // 右キーが離された
        if (Input.GetKeyUp(m_rearkey))
        {
            m_timeOfKeyUp = Time.time;
        }


        // 左右移動(もしくは回転)
        if (Input.GetKey(m_rearkey))
        {
            m_playerController.Left = 2.0f;
        }
        else if (Input.GetKey(m_frontkey))
        {
            m_playerController.Right = 2.0f;
        }

        if (Input.GetKey(m_upkey))
        {
            m_playerController.Up = 2.0f;
        }
        else if (Input.GetKey(m_downkey))
        {
            m_playerController.Down = 2.0f;
        }

        if (!m_isAttacking && m_attackIntervalTimer <= 0)
        {
            if (Input.GetKeyDown(m_attack1key))
            {
                m_attack1Timer = Time.time;
            }
            if (Input.GetKeyDown(m_attack2key))
            {
                m_attack2Timer = Time.time;
            }
            if (Input.GetKeyDown(m_forwardkey))
            {
                m_forwardTimer = Time.time;
            }
            if (Input.GetKeyDown(m_backkey))
            {
                m_backTimer = Time.time;
            }

            if (Time.time - m_attack1Timer < 0.3f &&
                Time.time - m_forwardTimer < 0.3f)
            {
                UnityAction action = AttackFinished;
                m_playerController.PlayAnimation("Attack", action);
                m_isAttacking = true;
                m_attack1Timer = 0f;
                m_forwardTimer = 0f;
            }
            else if (Time.time - m_attack2Timer < 0.3f &&
            Time.time - m_backTimer < 0.3)
            {
                UnityAction action = AttackFinished;
                m_playerController.PlayAnimation("Attack2", action);
                m_isAttacking = true;
                m_attack2Timer = 0f;
                m_backTimer = 0f;
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
        m_attackIntervalTimer = 0.2f;
        m_isAttacking = false;
    }
}

