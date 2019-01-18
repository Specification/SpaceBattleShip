using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_button;              //ボタン

    [SerializeField]
    RectTransform m_cursor;             //選択する際のカーソル

    [SerializeField]
    float m_cursorDistance;             //カーソルの移動量

    [SerializeField]
    int m_buttonNum;                    //項目の数

    int m_selectNumber;                 //選択している項目の番号

    Vector3 m_CursorDefaultPosition;    //カーソルのデフォルト位置


    // Use this for initialization
    void Start()
    {
        //カーソルの初期位置を記録
        m_CursorDefaultPosition = m_cursor.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //選択の上移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_selectNumber++;
        }
        //選択の下移動
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_selectNumber--;
        }
        //選択の決定とその項目の挙動
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (m_selectNumber)
            {
                case 0:
                    SceneManager.LoadScene("");
                    Debug.Log("case0を通った");
                    break;
                case 1:
                    SceneManager.LoadScene("CharacterSelect");
                    Debug.Log("case1を通った");
                    break;
            }
        }
        m_selectNumber = Mathf.Clamp(m_selectNumber, 0, m_buttonNum - 1);
        m_cursor.anchoredPosition = m_CursorDefaultPosition + new Vector3(0, m_selectNumber * m_cursorDistance, 0);
    }
}