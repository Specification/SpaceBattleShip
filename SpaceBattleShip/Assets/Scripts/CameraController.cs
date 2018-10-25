using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject m_taraget;

    [SerializeField]
    GameObject m_taraget2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float height = 1.5f;   //cameraの高さ
        float minDistance = 3f;  // cameraの距離

        //1pと2pの距離を求める
        float distance = Vector3.Distance(m_taraget.transform.position,
                                            m_taraget2.transform.position);
        //1pと2pのベクトルを求める
        Vector3 axis = m_taraget2.transform.position - m_taraget.transform.position;

        Vector3 angle = new Vector3(axis.z, axis.y, -axis.x);

        //中心を求める
        Vector3 center = m_taraget.transform.position + (axis.normalized * (distance / 2));

        //cameraの高さ調整
        if (center.y < height)
        {
            center.y = height;
        }
        //cameraが近すぎないように調整
        if (Mathf.Abs(distance) < minDistance)
        {
            distance = minDistance * Mathf.Abs(distance) / distance;
        }

        //cameraの位置を求める
        Vector3 targetPosition = center + (angle.normalized * distance);
        if (targetPosition.y < height / 3f)
        {
            targetPosition.y = height / 3f;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);

        //cameraの注目点
        transform.LookAt(center);




    }
}
