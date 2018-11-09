﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject m_syagekitarget;
    /// <summary>
    /// 右にいるのか左にいるのか
    /// </summary>
    public enum SideState
    {
        left,
        Right
    }
    SideState m_sideState = SideState.left;

    [SerializeField]
    GameObject m_target;    //対戦相手

    float m_forward = 0f;   // 前進用変数
    float m_back = 0f;   // 後退用変数
    float m_left = 0f;   // 左移動用変数
    float m_right = 0f;   // 右移動用変数
    float m_up = 0f;       // 上移動
    float m_down = 0f;      //下移動

    float m_damegebehaviorTimer;  //ダメージ時移動を制限する時間

    bool m_isPlayingAnimation;	// 強制アニメーション中か
    bool m_swordAnimation1;
    bool m_swordAnimation2;
    bool m_shootingAnimation;
    bool m_canAnimation;

    SimpleAnimation m_simpleAnimation;  // アニメーション管理変数

    Rigidbody m_rigidBody;            //リジッドボディ


    UnityEvent m_unityEvent = new UnityEvent();	// コールバックイベント用

    // Use this for initialization
    void Start()
    {
        m_swordAnimation1 = false;
        m_swordAnimation2 = false;
        m_shootingAnimation = false;
        m_canAnimation = true;
        m_damegebehaviorTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 移動処理

        // 右にいるのか左にいるのかを判定
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // 相手のスクリーン座標
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(m_target.transform.position);
        // X座標を比較して判定
        if (screenPos.x < targetScreenPos.x)
        {
            m_sideState = SideState.left;
        }
        else
        {
            m_sideState = SideState.Right;
        }
    }
    void Move()
    {
        if (m_damegebehaviorTimer <= 0) {
            // 前移動
            transform.position += transform.forward * m_forward * Time.deltaTime;

            // 後移動
            transform.position -= transform.forward * m_back * Time.deltaTime;
            // SPECIFICATION--
            // 右移動
            transform.position += transform.right * m_right * Time.deltaTime;

            // 左移動
            transform.position -= transform.right * m_left * Time.deltaTime;

            //上移動
            transform.position += Vector3.up * m_up * Time.deltaTime;

            //下移動
            transform.position -= Vector3.up * m_down * Time.deltaTime;

            // 相手のほうを向く
            transform.LookAt(m_target.transform);

            // Y軸以外の回転をなくす
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);



            // 移動用変数を0に戻す
            m_forward = 0f;
            m_back = 0f;
            m_left = 0f;
            m_right = 0f;
            m_up = 0f;
            m_down = 0f;
        }
        else
        {
            m_damegebehaviorTimer -= Time.deltaTime; 
        }
    }
    /// <summary>
    /// 前移動命令.
    /// </summary>
    public float Forward
    {
        set
        {
            m_forward = value;
        }
    }


    /// <summary>
    /// 後移動命令.
    /// </summary>
    public float Back
    {
        set
        {
            m_back = value;
        }
    }


    /// <summary>
    /// 左移動命令.
    /// </summary>
    public float Left
    {
        set
        {
            m_left = value;
        }
    }


    /// <summary>
    /// 右移動命令.
    /// </summary>
    public float Right
    {
        set
        {
            m_right = value;
        }
    }

    //上移動命令
    public float Up
    {
        set
        {
            m_up = value;
        }
    }

    public float Down
    {
        set
        {
            m_down = value;
        }
    }

    /// <summary>
    /// 右にいるのか左にいるのか
    /// </summary>
    public SideState Side
    {
        get
        {
            return m_sideState;
        }
    }
    /// <summary>
    /// アニメーションを再生する.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="callbackMethod">Callback method.</param>
    public void PlayAnimation(string value, UnityAction callbackMethod)
    {
        m_unityEvent.AddListener(callbackMethod);   // コールバック関数の登録
     //   m_simpleAnimation.CrossFade(value, 0.2f);
        m_isPlayingAnimation = true;
    }

    /// <summary>
    /// アニメーション終了時のイベントを受け取る.
    /// </summary>
    public void OnAnimationFinished()
    {
        m_unityEvent.Invoke();              // 登録されているコールバック関数の呼び出し
        m_unityEvent.RemoveAllListeners();  // 登録されていた関数を削除
        m_isPlayingAnimation = false;       // フラグをfalseに戻す
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Sword")
        {
            if (m_swordAnimation1)
            {
                m_rigidBody.AddForce(transform.forward * -5f,
                                        ForceMode.VelocityChange);
                m_simpleAnimation.CrossFade("Default", 0.2f);
                m_swordAnimation1 = false;
                m_damegebehaviorTimer = 0.2f;

            }
            if (m_swordAnimation2)
            {
                m_rigidBody.AddForce(transform.forward * -10f,
                                        ForceMode.VelocityChange);
                m_simpleAnimation.CrossFade("Default", 0.2f);
                m_swordAnimation2 = false;
                m_damegebehaviorTimer = 0.2f;
            }
        }

        if (col.gameObject.tag == "Shooting1")
        {
            //m_simpleAnimation.CrossFade("Default", 0.2f);
            m_shootingAnimation = false;
            m_damegebehaviorTimer = 0.2f;
        }
    }
}