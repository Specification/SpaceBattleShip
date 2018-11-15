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

    // 攻撃中判定フラグ
    bool m_isAttacking;

    //連続弱攻撃の何回目かを判断するフラグ
    bool m_attack1;     //1回目
    bool m_attack2;     //2回目

    // 攻撃インターバル用タイマー
    float m_attackIntervalTimer;

    // 前キーが離された時間を記録
    float m_timeOfKeyUp;

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
    KeyCode m_attack1Key = KeyCode.RightControl;    //攻撃１
    [SerializeField]
    KeyCode m_attack2Key = KeyCode.RightAlt;        //攻撃２

    [SerializeField]
    KeyCode AttackKey;  //なんの攻撃を繰り出したか

    float m_attack1Timer;                           // 攻撃キーを押した時刻を記録
    float m_attack2Timer;                           // 攻撃キーを押した時刻を記録

    float m_beforeAttack;

    // Use this for initialization
    void Start () {
        m_playerController = GetComponent<PlayerController>();
        m_attack1 = false;     
        m_attack2 = false;     
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
                if (Time.realtimeSinceStartup - m_timeOfKeyUp < 0.3f)
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
                if(Time.realtimeSinceStartup - m_timeOfKeyUp2 < 0.3f)
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
            m_timeOfKeyUp = Time.realtimeSinceStartup;
            AttackKey = forwardKey;
        }
        // 後ろキーが離されたとき
        if (Input.GetKeyUp(backKey))
        {
            m_timeOfKeyUp2 = Time.realtimeSinceStartup;
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
                    m_playerController.PlayAnimation("Attack1", action);
                    m_attack1 = true;
                    m_beforeAttack = m_attack1Timer;
                }

                else if (m_attack1 && !m_attack2 && m_attack1Timer - m_beforeAttack <= 1f)
                {
                    Debug.Log("Attack2");
                    m_playerController.PlayAnimation("Attack2", action);
                    m_attack2 = true;
                    m_beforeAttack = m_attack1Timer;
                }
                
                else if (m_attack1 && m_attack2 && m_attack1Timer - m_beforeAttack <= 1f)
                {
                    Debug.Log("Attack3");
                    m_playerController.PlayAnimation("Attack3", action);
                    m_attack1 = false;
                    m_attack2 = false;
                }

                

                m_isAttacking = true;
                
            }

            else if (Time.realtimeSinceStartup - m_attack2Timer < 0.3f )
            {
                UnityAction action = AttackFinished;
                Debug.Log("Attack4");
                m_playerController.PlayAnimation("Attack4", action);
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
