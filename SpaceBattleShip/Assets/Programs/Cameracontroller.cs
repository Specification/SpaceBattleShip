using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour {
    [SerializeField]
    GameObject m_target1;

    [SerializeField]
    GameObject m_target2;

    // Use this for initialization
    void Start()
    {

    }

	
	// Update is called once per frame
	void Update ()
    {
        float height = 1.5f;        // カメラの高さ
        float minDistance = 3f;     // カメラの最短距離

        // 1Pと2Pの距離を調べて中心を求める
        float distance = Vector3.Distance(m_target1.transform.position,
                                            m_target2.transform.position);

        // 1Pと2Pのベクトルを求める
        Vector3 axis = m_target2.transform.position - m_target1.transform.position;

        // axisに対して90度のベクトルを作る（xとz軸を入れ替えるだけ）
        Vector3 angle = new Vector3(axis.z, axis.y, -axis.x);

        // 中心を求める
        Vector3 center = m_target1.transform.position + (axis.normalized * (distance / 2));

        //  注視点の高さ調整        
        if (center.y < height)
        {
            center.y = height;
        }

        // カメラが近すぎないように調整
        if (Mathf.Abs(distance) < minDistance)
        {
            distance = minDistance * Mathf.Abs(distance) / distance;
        }

        // カメラの位置を求める
        Vector3 targetPosition = center + (angle.normalized * distance);

        // カメラの高さ調整
        if (targetPosition.y < height / 3f)
        {
            targetPosition.y = height / 3f;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);

        // カメラの注視点
        transform.LookAt(center);

    }
}
