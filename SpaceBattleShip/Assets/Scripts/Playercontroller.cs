using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    public enum SideState
    {
        Left, Right
    }
    SideState m_sideState = SideState.Left;

    [SerializeField]
    BoxCollider m_swordattacCollider;

    [SerializeField]
    GameObject m_target;

    [SerializeField]
    bool m_animationOnly;	// アニメーションだけ行い移動処理は行わない

    [SerializeField]
    GameObject m_hitEf;

    float m_forward = 0f;   // 前進用変数
    float m_back = 0f;   // 後退用変数
    float m_left = 0f;   // 左移動用変数
    float m_right = 0f;   // 右移動用変数
    float m_up = 0f;       // 上移動
    float m_down = 0f;      //下移動

    bool m_isPlayingAnimation;	// 強制アニメーション中か

    Rigidbody m_rigidbody;

    UnityEvent m_unityEvent = new UnityEvent();	// コールバックイベント用

   // SimpleAnimation m_simpleAnimation;  // アニメーション管理変数


    [SerializeField]
    float m_maxhp = 100f;       //最大HP
    float m_hp;          //HP

    HpController m_hpController;

    // Use this for initialization
    void Start()
    {
       // m_simpleAnimation = GetComponent<SimpleAnimation>();
        m_swordattacCollider.enabled = false;
        m_rigidbody = GetComponent<Rigidbody>();
        m_hpController = GetComponent<HpController>();
        m_hp = m_maxhp;    //hp初期値
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 移動処理
        //自分のスクリーン
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //相手のスクリーン
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(m_target.transform.position);
        if (screenPos.x < targetScreenPos.x)
        {
            m_sideState = SideState.Left;
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
        if (!m_animationOnly)
        {
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

            // She-muC
            //	        Vector3 rot = transform.rotation.eulerAngles;
            //	        rot.y += m_right * Time.deltaTime;  // 右回転
            //	        rot.y -= m_left * Time.deltaTime;   // 左回転
            //	        transform.rotation = Quaternion.Euler(rot);
            //
        }

        if (!m_isPlayingAnimation)
        {	// 強制アニメーション中でなければ

            // 相手のほうを向く
            transform.LookAt(m_target.transform);

            // Y軸以外の回転をなくす
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);

            // 「歩く」と「アイドル」のアニメーション切り替え
          /*  if (m_forward > 0 || m_back > 0 || m_left > 0 || m_right > 0)
            {
                if (m_forward > 3f)
                {
                    m_simpleAnimation.CrossFade("Run", 0.2f);
                }
                else
                {
                    m_simpleAnimation.CrossFade("Walk", 0.2f);
                }
            }
            else
            {
                m_simpleAnimation.CrossFade("Default", 0.2f);
            }
      */  }



        // 移動用変数を0に戻す
        m_forward = 0f;
        m_back = 0f;
        m_left = 0f;
        m_right = 0f;
        m_up = 0f;
        m_down = 0f;
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
       // m_simpleAnimation.CrossFade(value, 0.2f);
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
        Debug.Log("StartAttack");
        m_swordattacCollider.enabled = true;
    }

    /// <summary>
    /// 当たり判定終了
    /// </summary>
    public void EndAttack()
    {
        Debug.Log("EndAttack");
        m_swordattacCollider.enabled = false;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Attack")
        {
            m_rigidbody.AddForce(transform.forward * -10f, ForceMode.VelocityChange);

            GameObject effect = Instantiate(m_hitEf, col.ClosestPointOnBounds(transform.position), transform.rotation);

            Destroy(effect, 1f);

            //damage処理
            m_hp -= 6f;
            if (m_hp < 0)
            {
                m_hp = 0f;
            }

            m_hpController.Set(m_hp / m_maxhp);

        }
    }
}
