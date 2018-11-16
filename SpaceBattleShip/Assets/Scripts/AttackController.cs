using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{

    [SerializeField]
    BoxCollider m_swordAttackCollider;  // 剣攻撃用コライダー


    bool m_isPlayingAnimation;	// 強制アニメーション中か

    SimpleAnimation m_simpleAnimation;  // アニメーション管理変数

    UnityEvent m_unityEvent = new UnityEvent();	// コールバックイベント用

    // Use this for initialization
    void Start()
    {
        m_simpleAnimation = GetComponent<SimpleAnimation>();
        m_swordAttackCollider.enabled = false;
        m_isPlayingAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isPlayingAnimation)
        {
            m_simpleAnimation.CrossFade("Default", 0.2f);

        }
    }

    public void Attack(string value,UnityAction callbackMethod)
    {
        m_unityEvent.AddListener(callbackMethod);   // コールバック関数の登録
        m_simpleAnimation.CrossFade(value, 0.2f);
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
    /// <summary>
    /// 当たり判定開始
    /// </summary>
    public void StartAttack()
    {

        m_swordAttackCollider.enabled = true;

    }

    /// <summary>
    /// 当たり判定終了
    /// </summary>
    public void EndAttack()
    {
        m_swordAttackCollider.enabled = false;

    }

}
