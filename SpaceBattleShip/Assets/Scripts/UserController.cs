using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserController : MonoBehaviour {

    PlayerController m_playerController;

    // 走っているかフラグ
    bool m_isRunning;

    //バックダッシュしてるかフラグ
    bool m_backDash;

    // 前キーが離された時間を記録
    float m_timeOfKeyUp;
    // 攻撃中判定フラグ
    bool m_isAttacking;
    // 攻撃インターバル用タイマー
    float m_attackIntervalTimer;

    //後ろキーが離された時間を記録
    float m_timeOfKeyUp2;

    [SerializeField]
    KeyCode m_forwardKey = KeyCode.RightArrow;      //前移動
    [SerializeField]
    KeyCode m_backKey = KeyCode.LeftArrow;          //後ろ移動
    [SerializeField]
    KeyCode m_rearKey = KeyCode.UpArrow;            //奥移動
    [SerializeField]
    KeyCode m_frontKey = KeyCode.DownArrow;         //手前移動
    [SerializeField]
    KeyCode m_upKey = KeyCode.RightShift;      　　 //上移動
    [SerializeField]
    KeyCode m_downKey = KeyCode.KeypadEnter;       //下移動
    [SerializeField]
    KeyCode m_attack1Key = KeyCode.K;    //攻撃１
    [SerializeField]
    KeyCode m_attack2Key = KeyCode.L;        //攻撃２


    float m_attack1Timer;                           // 接近攻撃キー1を押した時刻を記録
    float m_attack2Timer;                           // 接近攻撃キー2を押した時刻を記録
    float m_shot1Timaer;                            // 射撃攻撃キーを押した時刻を記録
    float m_forwardTimer;                           // 前キーを押した時刻を記録
    float m_backTimer;                              // 後ろキーを押した時刻を記録

    // Use this for initialization
    void Start () {
        m_playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        // 画面の右左のどちらにいるかで方向キーを入れ替える
        KeyCode forwardKey = m_forwardKey;
        KeyCode backKey = m_backKey;

        if (m_playerController.Side == PlayerController.SideState.Right)
        {
            forwardKey = m_backKey;
            backKey = m_forwardKey;
        }

        // 前後移動
        if (Input.GetKey(forwardKey))
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
                m_playerController.Forward = 10.0f;  // 走っているなら
            }
            else
            {
                m_playerController.Forward = 5.0f;  // 歩いているなら
            }

            m_backDash = false;
            //            m_playerController.Forward = 1.0f * 5f; // アナログ値 * 移動量(m/s)←最大値
        }
        else if (Input.GetKey(backKey))
        {
            if(!m_backDash) //バックダッシュしていないとき
            {
                if(Time.time - m_timeOfKeyUp2 < 0.3f)
                {
                    m_backDash = true;
                }
            }

            if (m_backDash)
            {
                m_playerController.Back = 10.0f;
            }
            else
            {
                m_playerController.Back = 5.0f;
            }
        }
        else
        {
            m_isRunning = false;
            m_backDash = false;
        }

        // 前キーが離された
        if (Input.GetKeyUp(forwardKey))
        {
            m_timeOfKeyUp = Time.time;
        }
        // 後ろキーが離されたとき
        if (Input.GetKeyUp(backKey))
        {
            m_timeOfKeyUp2 = Time.time;
        }


        // 左右移動(もしくは回転)
        if (Input.GetKey(m_rearKey))
        {
            m_playerController.Left = 5.0f;
        }
        else if (Input.GetKey(m_frontKey))
        {
            m_playerController.Right = 5.0f;
        }

        if (Input.GetKey(m_upKey))
        {
            m_playerController.Up = 5.0f;
        }
        else if (Input.GetKey(m_downKey))
        {
            m_playerController.Down = 5.0f;
        }
        // 攻撃処理
        if (!m_isAttacking && m_attackIntervalTimer <= 0)
        {
            // 入力を受け付けた時刻を記録
            if (Input.GetKeyDown(m_attack1Key))
            {
                m_attack1Timer = Time.time;
            }
            if (Input.GetKeyDown(m_attack2Key))
            {
                m_attack2Timer = Time.time;
            }
            if (Input.GetKeyDown(forwardKey))
            {
                m_forwardTimer = Time.time;
            }
            if (Input.GetKeyDown(backKey))
            {
                m_backTimer = Time.time;
            }

            if (Time.time - m_attack1Timer < 0.3f && Time.time - m_forwardTimer < 0.3f)
            {
                UnityAction action = AttackFinished;
                m_playerController.PlayAnimation("Attack", action);
                m_isAttacking = true;
                m_attack1Timer = 0f;
                m_forwardTimer = 0f;
            }

            else if (Time.time - m_attack2Timer < 0.3f && Time.time - m_backTimer < 0.3f)
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



