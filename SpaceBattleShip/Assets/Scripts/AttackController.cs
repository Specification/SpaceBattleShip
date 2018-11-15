using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour {

    [SerializeField]
    BoxCollider m_swordAttackCollider;  // 剣攻撃用コライダー

    float m_damegebehaviorTimer1;  //ダメージ時移動を制限する時間
    float m_damegebehaviorTimer2;  //ダメージ時攻撃を制限する時間
    bool m_isPlayingAnimation;	// 強制アニメーション中か
    bool m_swordAnimation1;
    bool m_swordAnimation2;
    bool m_shootingAnimation;
    bool m_canAnimation;

    SimpleAnimation m_simpleAnimation;  // アニメーション管理変数

    UnityEvent m_unityEvent = new UnityEvent();	// コールバックイベント用

    // Use this for initialization
    void Start () {
        m_simpleAnimation = GetComponent<SimpleAnimation>();
        m_swordAttackCollider.enabled = false;

        m_swordAnimation1 = false;
        m_swordAnimation2 = false;
        m_damegebehaviorTimer1 = 0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAnimation(string value, UnityAction callbackMethod)
    {

        //強弱どちらのアニメーションをしているのかを判断
        if (value == "Attack1" || value == "Attack2" || value == "Attack3")
        {
            m_swordAnimation1 = true;
        }
        else if (value == "Attack4")
        {
            m_swordAnimation2 = true;
        }


        if (Time.time - m_damegebehaviorTimer2 >= 0.3f)
        {
            m_unityEvent.AddListener(callbackMethod);   // コールバック関数の登録
            m_simpleAnimation.CrossFade(value, 0.2f);
            m_isPlayingAnimation = true;

        }


    }

    /// <summary>
    /// アニメーション終了時のイベントを受け取る.
    /// </summary>
    public void OnAnimationFinished()
    {
        m_unityEvent.Invoke();              // 登録されているコールバック関数の呼び出し
        m_unityEvent.RemoveAllListeners();  // 登録されていた関数を削除
        m_swordAnimation1 = false;
        m_swordAnimation2 = false;
        m_isPlayingAnimation = false;		// フラグをfalseに戻す
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
