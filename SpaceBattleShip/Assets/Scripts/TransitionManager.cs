using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TransitionManager : MonoBehaviour
{

    /// <summary>
    /// 状態
    /// </summary>
    public enum State
    {
        Standby,        // スタンバイ
        Doing,          // 実行中
    }
    State m_state = State.Standby;

    float m_totalTime;      // アニメーションにかける時間
    float m_timer;          // アニメーション用タイマー

    Color m_startColor;     // アニメーション開始時の色
    Color m_targetColor;    // 目標色

    [SerializeField]
    Image m_image;          // フェード用の画像

    Canvas m_canvas;        // キャンバス

    UnityEvent m_unityEvent = new UnityEvent();     //コールバック用

    // シングルトン
    static TransitionManager instance;

    public static TransitionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (TransitionManager)FindObjectOfType(typeof(TransitionManager));

                if (instance == null)
                {
                    Debug.LogError("TransitionManeger instance is null");
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("TransitionManager");
        if (obj.Length > 1)
        {
            // すでに存在しているなら削除
            Destroy(gameObject);
        }
        else
        {
            // 管理はシーン遷移では破棄させない
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_state == State.Doing)
        {
            // タイマーのカウントアップと上限チェック
            m_timer += Time.deltaTime;
            m_timer = Mathf.Clamp(m_timer, 0f, m_totalTime);

            m_image.color = Color.Lerp(m_startColor, m_targetColor, m_timer / m_totalTime);

            // アニメーション終了
            if (m_timer >= m_totalTime)
            {
                m_state = State.Standby;
                m_unityEvent.Invoke();
                m_unityEvent.RemoveAllListeners();
            }
        }
    }


    public void Fade(Color startColor, Color targetColor, float time, UnityAction callbackMethod)
    {
        if (m_state == State.Standby)
        {

            // 目標色とスタート色をセット
            m_targetColor = targetColor;
            m_startColor = startColor;
            m_image.color = m_startColor;

            m_totalTime = time;
            m_timer = 0f;

            m_canvas.enabled = true;

            m_unityEvent.AddListener(callbackMethod);

            m_state = State.Doing;
        }
    }

    /// <summary>
    /// フェード開始
    /// </summary>
    /// <param name="targetColor"></param>
    /// <param name="time"></param>
    public void Fade(Color targetColor, float time, UnityAction callbackMethod)
    {
        Fade(m_image.color, targetColor, time, callbackMethod);
    }

    /// <summary>
    /// ステートを返す
    /// </summary>
    /// <returns></returns>
    public State GetState()
    {
        return m_state;
    }

    /// <summary>
    /// キャンバスを無効にする
    /// </summary>
    public void CanvasDisabled()
    {
        m_canvas.enabled = false;
    }
}