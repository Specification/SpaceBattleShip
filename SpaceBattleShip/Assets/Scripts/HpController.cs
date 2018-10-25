using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpController : MonoBehaviour
{
    [SerializeField]
    RectTransform m_hpUi;     //HP表示用
    [SerializeField]
    RectTransform m_damageUI; //damage表示用

    float m_damageStartScale = 1f;
    float m_damageTargetStartScale;
    float m_damageAnimationTimer;

    [SerializeField]
    float m_damageAnimationTime = 0.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_damageAnimationTimer > 0)
        {

            //タイマーのカウントダウン

            m_damageAnimationTimer -= Time.deltaTime;
            //タイマー加減チェック
            if (m_damageAnimationTimer < 0)
            {
                m_damageAnimationTimer = 0f;
                m_damageStartScale = m_damageTargetStartScale;
            }
            //割合の算出
            float scale = Mathf.Lerp(m_damageStartScale, m_damageTargetStartScale, 1f - m_damageAnimationTimer / m_damageAnimationTime);
            m_damageUI.localScale = new Vector3(scale, 1f, 1f);
        }
    }

    /// <summary>
    /// HPの割合をセットすることで自動animation
    /// </summary>
    /// <param name="value"></param>
    public void Set(float value)
    {
        if (m_damageStartScale <= m_hpUi.localScale.x)
        {
            m_damageStartScale = m_hpUi.localScale.x;
            m_damageStartScale = m_hpUi.localScale.x;
        }
        //タイマーをセット
        m_damageAnimationTimer = m_damageAnimationTime;
        //目標値をセット
        m_damageTargetStartScale = value;

        m_hpUi.localScale = new Vector3(value, 1f, 1f);
    }

}
