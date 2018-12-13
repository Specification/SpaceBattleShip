using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< .merge_file_a09376
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickedTitle()
    {
        SceneManager.LoadScene("");
    }

    public void ClickedCharacterSelect()
    {
        SceneManager.LoadScene("");
    }

    public void ClickedOneMore()
    {
        SceneManager.LoadScene("");
    }
=======
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
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
	void Start ()
    {
        //カーソルの初期位置を記録
        m_CursorDefaultPosition = m_cursor.anchoredPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //選択の上移動
		if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_selectNumber++;
        }
        //選択の下移動
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_selectNumber--;
        }
        //選択の決定とその項目の挙動
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("決定");
        }
        m_selectNumber = Mathf.Clamp(m_selectNumber, 0, m_buttonNum-1);
        m_cursor.anchoredPosition = m_CursorDefaultPosition + new Vector3( 0, m_selectNumber * m_cursorDistance, 0);
	}
>>>>>>> .merge_file_a06372
}
