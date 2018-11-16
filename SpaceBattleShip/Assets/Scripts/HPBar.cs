using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar: MonoBehaviour
{

    public RectTransform m_hpUI;       // HP表示用

    [SerializeField]
    RectTransform m_damegeUI;   // HPのダメージ表示用

    // ダメージアニメーション用の変数群
    float m_damegeStartScale = 1f;    // 初期値
    float m_damegeTargetScale;        // 目標値
    float m_damegeAnimationTimer;     // タイマー
    [SerializeField]
    float m_damegeAnimationTime = 0.5f;         // 

    // Use this for initialization
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        if (m_damegeAnimationTimer > 0)
        {
            //タイマーのカウントダウン
            m_damegeAnimationTimer -= Time.deltaTime;
            // タイマーの下限チェック
            if (m_damegeAnimationTimer < 0)
            {
                m_damegeAnimationTimer = 0f;
                m_damegeStartScale = m_damegeTargetScale;
            }
            // タイマーのセット
            float scale = Mathf.Lerp(m_damegeStartScale, m_damegeTargetScale, 1f - m_damegeAnimationTimer / m_damegeAnimationTime);

            //UIに反映
            m_damegeUI.localScale = new Vector3(scale, 1f, 1f);
        }
    }

    /// <summary>
    /// HPの割合をセットすることで自動アニメーション 
    /// </summary>
    /// <param name="value"></param>
    public void Set(float value)
    {
        // ダメージアニメーションの初期化
        // HPAreaと同じか小さい場合のみHPAreaのスケールを初期値とする
        if (m_damegeStartScale <= m_hpUI.localScale.x)
        {
            m_damegeStartScale = m_hpUI.localScale.x;
        }
        // タイマーをセット
        m_damegeAnimationTimer = m_damegeAnimationTime;
        // 目標地をセット
        m_damegeTargetScale = value;

        // UIに反映
        m_hpUI.localScale = new Vector3(value, 1f, 1f);

    }
}