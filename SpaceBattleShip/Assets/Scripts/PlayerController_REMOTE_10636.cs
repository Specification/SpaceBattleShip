using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{

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
    public GameObject m_Target;    //対戦相手


    [SerializeField]
    bool m_animationOnly; // アニメーションだけ行い移動処理は行わない

    bool m_isPlayingAnimation; // 強制アニメーション中か

    UnityEvent m_unityEvent = new UnityEvent(); // コールバックイベント用

    SimpleAnimation m_simpleAnimation;  // アニメーション管理変数

    [SerializeField]
    float m_maxHP = 100f;        // 最大HP
    float m_hp;           // HP

    HPBar m_HPbar;    //HPUI制御用

    // Use this for initialization
    void Start()
    {
        m_simpleAnimation = GetComponent<SimpleAnimation>();

        m_HPbar = GetComponent<HPBar>();

        m_hp = 100f;    // HP初期値
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 移動処理

        // 右にいるのか左にいるのかを判定
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // 相手のスクリーン座標
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(m_Target.transform.position);

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

    /// <summary>
    /// 移動.
    /// </summary>
    void Move()
    {

        if (!m_isPlayingAnimation)
        { // 強制アニメーション中でなければ

            // 相手のほうを向く
            transform.LookAt(m_Target.transform);

            // Y軸以外の回転をなくす
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);

        }

    }

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
        m_unityEvent.AddListener(callbackMethod); // コールバック関数の登録
        m_simpleAnimation.CrossFade(value, 0.2f);
        m_isPlayingAnimation = true;
    }

    /// <summary>
    /// アニメーション終了時のイベントを受け取る.
    /// </summary>
    public void OnAnimationFinished()
    {
        m_unityEvent.Invoke();    // 登録されているコールバック関数の呼び出し
        m_unityEvent.RemoveAllListeners(); // 登録されていた関数を削除
        m_isPlayingAnimation = false;  // フラグをfalseに戻す
    }
}