using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

<<<<<<< HEAD
=======

>>>>>>> 柴田秋専用
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
<<<<<<< HEAD
    GameObject m_target;    //対戦相手

    //[SerializeField]
    //BoxCollider m_swordAttackCollider;  // 剣攻撃用コライダー

    [SerializeField]
    AttackController m_attackController;

    [SerializeField]
    float m_speed = 10f;

    [SerializeField]
    GameObject m_hitEffect; //ヒットエフェクト


    float m_up = 0f;       // 上移動
    float m_down = 0f;      //下移動
    
    float m_horizontal;     //前後移動
    float m_vertical;       //左右移動

    float m_damegebehaviorTimer1;  //ダメージ時移動を制限する時間
    float m_damegebehaviorTimer2;  //ダメージ時攻撃を制限する時間
    bool m_isPlayingAnimation;	// 強制アニメーション中か
    bool m_swordAnimation1;
    bool m_swordAnimation2;
    bool m_shootingAnimation;
    bool m_canAnimation;

    SimpleAnimation m_simpleAnimation;  // アニメーション管理変数

    Rigidbody m_rigidBody;            //リジッドボディ


    UnityEvent m_unityEvent = new UnityEvent();	// コールバックイベント用

=======
    public GameObject m_Target;    //対戦相手


    [SerializeField]
    bool m_animationOnly; // アニメーションだけ行い移動処理は行わない

    bool m_isPlayingAnimation; // 強制アニメーション中か

    UnityEvent m_unityEvent = new UnityEvent(); // コールバックイベント用

    SimpleAnimation m_simpleAnimation;  // アニメーション管理変数

>>>>>>> 柴田秋専用
    [SerializeField]
    float m_maxHP = 100f;        // 最大HP
    float m_hp;           // HP

<<<<<<< HEAD
    HpController m_hpController;    //HPUI制御用
=======
    HPBar m_HPbar;    //HPUI制御用
>>>>>>> 柴田秋専用

    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        m_rigidBody = GetComponent<Rigidbody>();
        m_simpleAnimation = GetComponent<SimpleAnimation>();
        //m_swordAttackCollider.enabled = false;

        m_isPlayingAnimation = false;
        m_swordAnimation1 = false;
        m_swordAnimation2 = false;
        m_damegebehaviorTimer1 = 0f;

        m_hpController = GetComponent<HpController>();
=======
        m_simpleAnimation = GetComponent<SimpleAnimation>();

        m_HPbar = GetComponent<HPBar>();
>>>>>>> 柴田秋専用

        m_hp = 100f;    // HP初期値
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD

=======
>>>>>>> 柴田秋専用
        Move(); // 移動処理

        // 右にいるのか左にいるのかを判定
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // 相手のスクリーン座標
<<<<<<< HEAD
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(m_target.transform.position);
=======
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(m_Target.transform.position);

>>>>>>> 柴田秋専用
        // X座標を比較して判定
        if (screenPos.x < targetScreenPos.x)
        {
            m_sideState = SideState.left;
        }
        else
        {
            m_sideState = SideState.Right;
        }
<<<<<<< HEAD

        if (!m_isPlayingAnimation)
        {
            m_simpleAnimation.CrossFade("Default", 0.2f);

        }
    }
    void Move()
    {
        

        if (Time.time - m_damegebehaviorTimer1 >= 0.3f)
        {
            //前後移動
            transform.position += transform.forward * m_horizontal* m_speed * Time.deltaTime;

            //左右移動
            transform.position += transform.right * m_vertical * m_speed * Time.deltaTime;

            //上移動
            transform.position += Vector3.up * m_up * Time.deltaTime;

            //下移動
            transform.position -= Vector3.up * m_down * Time.deltaTime;

            m_up = 0f;
            m_down = 0f;
        }


    }

    public float horizontal
    {
        set
        {
            m_horizontal = value;
        }
    }

    public float vertical
    {
        set
        {
            m_vertical = value;
        }
    }

    /*
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
    */

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
    /// ターゲットをセット
    /// </summary>
    public GameObject Target
    {
        set
        {
            m_target = value;
        }
    }

    /// <summary>
    /// 右にいるのか左にいるのか
    /// </summary>
=======
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

>>>>>>> 柴田秋専用
    public SideState Side
    {
        get
        {
            return m_sideState;
        }
    }
<<<<<<< HEAD
=======

>>>>>>> 柴田秋専用
    /// <summary>
    /// アニメーションを再生する.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="callbackMethod">Callback method.</param>
    public void PlayAnimation(string value, UnityAction callbackMethod)
    {
<<<<<<< HEAD

        //強弱どちらのアニメーションをしているのかを判断
        if (value == "Attack1" || value == "Attack2" || value == "Attack3")
        { 
            m_swordAnimation1 = true;
        }
        else if(value == "Attack4")
        {   
            m_swordAnimation2 = true;
        }
        

        if (Time.time - m_damegebehaviorTimer2 >= 0.3f)
        {
            m_unityEvent.AddListener(callbackMethod);   // コールバック関数の登録
            m_simpleAnimation.CrossFade(value, 0.2f);
            m_attackController.Attack(value, callbackMethod);
            m_isPlayingAnimation = true;
        }
        


=======
        m_unityEvent.AddListener(callbackMethod); // コールバック関数の登録
        m_simpleAnimation.CrossFade(value, 0.2f);
        m_isPlayingAnimation = true;
>>>>>>> 柴田秋専用
    }

    /// <summary>
    /// アニメーション終了時のイベントを受け取る.
    /// </summary>
    public void OnAnimationFinished()
    {
<<<<<<< HEAD
        m_unityEvent.Invoke();              // 登録されているコールバック関数の呼び出し
        m_unityEvent.RemoveAllListeners();  // 登録されていた関数を削除
        m_swordAnimation1 = false;
        m_swordAnimation2 = false;
        m_isPlayingAnimation = false;       // フラグをfalseに戻す
        m_simpleAnimation.CrossFade("Default", 0.2f);
    }
    /// <summary>
    /// 当たり判定開始
    /// </summary>
    /*public void StartAttack()
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
    */

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
                m_damegebehaviorTimer1 = Time.time;
                m_damegebehaviorTimer2 = Time.time;

                m_hp -= 10f;
                if (m_hp < 0)
                {
                    m_hp = 0f;
                }

                //UIに反映
                m_hpController.Set(m_hp / m_maxHP);

                // エフェクト再生
                GameObject effect = Instantiate(m_hitEffect,
                                                col.ClosestPointOnBounds(transform.position),
                                                transform.rotation);

                Destroy(effect, 1f);    // 1秒後にエフェクトを消す

            }
            else if (m_swordAnimation2)
            {
                m_rigidBody.AddForce(transform.forward * -10f,
                                        ForceMode.VelocityChange);
                m_simpleAnimation.CrossFade("Default", 0.2f);
                m_swordAnimation2 = false;
                m_damegebehaviorTimer1 = Time.time;
                m_damegebehaviorTimer2 = Time.time;

                m_hp -= 10f;
                if (m_hp < 0)
                {
                    m_hp = 0f;
                }

                //UIに反映
                m_hpController.Set(m_hp / m_maxHP);

                // エフェクト再生
                GameObject effect = Instantiate(m_hitEffect,
                                                col.ClosestPointOnBounds(transform.position),
                                                transform.rotation);

                Destroy(effect, 1f);    // 1秒後にエフェクトを消す
            }
        }

        if (col.gameObject.tag == "Shooting")
        {
            m_simpleAnimation.CrossFade("Default", 0.2f);
            m_damegebehaviorTimer1 = Time.time;
            m_damegebehaviorTimer2 = Time.time;

            m_hp -= 10f;
            if (m_hp < 0)
            {
                m_hp = 0f;
            }

            //UIに反映
            m_hpController.Set(m_hp / m_maxHP);

            // エフェクト再生
            GameObject effect = Instantiate(m_hitEffect,
                                            col.ClosestPointOnBounds(transform.position),
                                            transform.rotation);

            Destroy(effect, 1f);    // 1秒後にエフェクトを消す
        }
    }
}
=======
        m_unityEvent.Invoke();    // 登録されているコールバック関数の呼び出し
        m_unityEvent.RemoveAllListeners(); // 登録されていた関数を削除
        m_isPlayingAnimation = false;  // フラグをfalseに戻す
    }
}
>>>>>>> 柴田秋専用
