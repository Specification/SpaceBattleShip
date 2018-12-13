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

    public string JoyStick1LeftHorizontal = "JoyStick1LeftHorizontal";

    public string JoyStick1LeftVertical = "JoyStick1LeftVertical";


    
    public KeyCode m_upKey = KeyCode.Joystick1Button7;         //上移動

    public KeyCode m_downKey = KeyCode.Joystick1Button6;       //下移動
    
    public KeyCode m_attack1Key = KeyCode.Joystick1Button1;    //攻撃１

    public KeyCode m_attack2Key = KeyCode.Joystick1Button2;        //攻撃２
    
    // コントローラで入力された値を代入
    float horizontal;
    float vertical;


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
       // KeyCode forwardKey = m_forwardKey;
       // KeyCode backKey = m_backKey;



        
        if (m_playerController.Side == PlayerController.SideState.Right)
        {
            horizontal = -horizontal;
        }

        horizontal = Input.GetAxis(JoyStick1LeftHorizontal);
        m_playerController.horizontal = horizontal;

        vertical = Input.GetAxis(JoyStick1LeftVertical);
        m_playerController.vertical = vertical;

        
        // 上下移動
        if (Input.GetKey(m_upKey))
        {
            m_playerController.Up = 5.0f;
        }
        else if (Input.GetKey(m_downKey))
        {
            m_playerController.Down = 5.0f;
        }
       

        // 攻撃処理--------------------------------------------
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
    //-----------------------------------------------------------------------------

    /// <summary>
    /// 攻撃が完了したら呼ばれる関数
    /// </summary>
    public void AttackFinished()
    {
        m_attackIntervalTimer = 0.3f;
        m_isAttacking = false;
    }
}
