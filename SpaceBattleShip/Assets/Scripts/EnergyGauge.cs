using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGauge : MonoBehaviour
{
    [SerializeField]
    RectTransform m_egUI;
    [SerializeField]
    RectTransform m_weightLossUI;   //減量

    float m_weightStartScale = 1f;       //初期値
    float m_weightTargetScale;      //目標値
    float m_weightAnimationTimer;   //タイマー

    [SerializeField]
    float m_weightAnimationTime = 0.5f;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_weightAnimationTimer > 0)
        {
            //タイマーのカウントダウン
            m_weightAnimationTimer -= Time.deltaTime;
            //タイマーの下限チェック
            if (m_weightAnimationTimer < 0)
            {
                m_weightAnimationTimer = 0f;
                m_weightStartScale = m_weightTargetScale;
            }
            //割合の算出
            float scale = Mathf.Lerp(m_weightStartScale, m_weightTargetScale,
                1f - m_weightAnimationTimer / m_weightAnimationTime);
            //
            m_egUI.localScale = new Vector3(scale, 1f, 1f);
        }
    }
    public void Set(float value)
    {
        if (m_weightStartScale <= m_egUI.localScale.x)
        {
            m_weightStartScale = m_egUI.localScale.x;
            //タイマーをセット
            m_weightAnimationTimer = m_weightAnimationTime;

            //目標値をセット
            m_weightTargetScale = value;

            //UIに反映
            m_egUI.localScale = new Vector3(value, 1f, 1f);
        }
    }
}

