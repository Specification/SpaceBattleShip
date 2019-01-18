using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SelectManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] m_charcterPrefabs;               //キャラクターのプレハブ群
    [SerializeField]
    GameObject[] m_weaponPrefabs;                 //武器のプレファブ群
    GameObject[] m_charcter = new GameObject[2];  //インスタンス化したキャラを保持する
    GameObject[] m_weapon = new GameObject[2];    //インスタンス化した武器を保持する
    int[] m_loadedCharcter = new int[2];          //ロードしたキャラの保存
    bool[] m_selectFinished = new bool[2];        //選択が完了したか

    [SerializeField]
    Vector3 m_1PPosition;                         //キャラの表示位置 

    [SerializeField]
    Vector3 m_2PPosition;                         //キャラの表示位置

    [SerializeField]
    RectTransform m_1PCursor;                     //1Pカーソル

    [SerializeField]
    RectTransform m_2PCursor;                     //2Pカーソル

    [SerializeField]
    Text m_startText;                             //スタート用テキスト

    [SerializeField]
    int m_charcterNum;                            //キャラの数

    [SerializeField]
    float m_cursorDistance;                       //カーソルの移動量

    int m_1PSelect;                               //1Pが選択しているキャラ番号
    int m_2PSelect;                               //2Pが選択しているキャラ番号

    Vector3 m_1PCursorDefaultPosition;            //1Pカーソルのデフォルト位置
    Vector3 m_2PCursorDefaultPosition;            //2Pカーソルのデフォルト位置

    enum State
    {
        Doing,              // キャラ選択中
        Selected,           // キャラ選択完了
    }
    State m_state = State.Doing;

    // Use this for initialization
    void Start()
    {
        //1Pと2Pのカーソル初期位置を記録
        m_1PCursorDefaultPosition = m_1PCursor.anchoredPosition;
        m_2PCursorDefaultPosition = m_2PCursor.anchoredPosition;

        //2Pは一番最後のキャラを選択している
        m_2PSelect = m_charcterNum - 2;
        //2Pカーソルの位置を初期化
        m_2PCursor.anchoredPosition = m_2PCursorDefaultPosition + new Vector3(m_2PSelect * m_cursorDistance, 0, 0);
        //スタート用テキストの非表示
        m_startText.enabled = false;

        m_loadedCharcter[0] = -1;
        m_loadedCharcter[1] = -1;
    }

    // Update is called once per frame
    void Update()
    {
        bool is1PCanceled = false;
        bool is2PCanceled = false;

        // 選択が完了しているなら抜ける
        if (m_state == State.Selected)
        {
            return;
        }

        //1P2P
        if (m_selectFinished[0] && m_selectFinished[1])
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                //どのキャラクターを選択したかを記録
                PlayerPrefs.SetInt("1P", m_1PSelect);
                PlayerPrefs.SetInt("2P", m_2PSelect);
                PlayerPrefs.Save();

                UnityAction callbackMethod = OnTransitionFinished;

                // トランジション
                TransitionManager.Instance.Fade(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), 2f, callbackMethod);

                Destroy(m_charcter[0]); //破棄
                Destroy(m_charcter[1]); //破棄
                m_state = State.Selected;
                return;
            }
        }

        //1Pのカーソル選択について
        if (!m_selectFinished[0])
        {
            // 1Pカーソル右移動
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_1PSelect++;
            }

            // 1Pカーソル左移動
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_1PSelect--;
            }
            //決定の処理
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_selectFinished[0] = true;
                if (m_selectFinished[1])
                {
                    m_startText.enabled = true;
                }
                PlayerController pc = m_charcter[0].GetComponent<PlayerController>();
                pc.m_target = Camera.main.gameObject;
                is1PCanceled = true;
                Debug.Log("1Pキャラクター選択");
            }
        }
        else
        {
            //キャンセルの処理
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_selectFinished[0] = false;
                m_startText.enabled = false;
                PlayerController pc = m_charcter[0].GetComponent<PlayerController>();
                pc.m_target = gameObject;
                Debug.Log("1P選択のキャンセル");
            }
        }

        //上限下限チェックを一括でできる
        //Mathf.Clamp(設定したい変数 , 下限 , 上限)
        m_1PSelect = Mathf.Clamp(m_1PSelect, 0, m_charcterNum - 1);
        m_1PCursor.anchoredPosition = m_1PCursorDefaultPosition + new Vector3(m_1PSelect * m_cursorDistance, 0, 0);


        //2Pのカーソル選択について
        if (!m_selectFinished[1])
        {
            // 2Pカーソル右移動
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_2PSelect++;
                /*//上限チェック
                if (m_2PSelect >= m_charcterNum)
                {
                    m_2PSelect = m_charcterNum - 1;
                }
                */
            }

            // 2Pカーソル左移動
            else if (Input.GetKeyDown(KeyCode.A))
            {
                m_2PSelect--;
                /*下限チェック
                if (m_2PSelect < 0)
                {
                    m_2PSelect = 0;
                }
                */
            }
            //決定の処理
            if (Input.GetKeyDown(KeyCode.W))
            {
                m_selectFinished[1] = true;
                if (m_selectFinished[0])
                {
                    m_startText.enabled = true;
                    PlayerController pc = m_charcter[1].GetComponent<PlayerController>();
                    pc.m_target = Camera.main.gameObject;
                    is2PCanceled = true;
                    Debug.Log("2Pキャラクター選択");
                }
            }
        }
        else
        {
            //キャンセルの処理
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_selectFinished[1] = false;
                m_startText.enabled = false;
                PlayerController pc = m_charcter[1].GetComponent<PlayerController>();
                pc.m_target = gameObject;
                Debug.Log("2P選択のキャンセル");
            }
        }
        //上限下限チェックを一括でできる
        //Mathf.Clamp(設定したい変数 , 下限 , 上限)
        m_2PSelect = Mathf.Clamp(m_2PSelect, 0, m_charcterNum - 1);
        m_2PCursor.anchoredPosition = m_2PCursorDefaultPosition + new Vector3(m_2PSelect * m_cursorDistance, 0, 0);

        // キャラクターのロード
        //1Pの選択処理
        if (m_loadedCharcter[0] != m_1PSelect)   //違うキャラなら読み込み
        {
            m_loadedCharcter[0] = m_1PSelect;
            Destroy(m_charcter[0]); //破棄
            m_charcter[0] = Instantiate(m_charcterPrefabs[m_1PSelect], m_1PPosition, Quaternion.Euler(Vector3.right));
            m_charcter[0].GetComponent<PlayerController>().m_target = gameObject;
            
        }
        //2Pの選択処理
        if (m_loadedCharcter[1] != m_2PSelect)  //違うキャラなら読み込み
        {
            m_loadedCharcter[1] = m_2PSelect;
            Destroy(m_charcter[1]); //破棄
            m_charcter[1] = Instantiate(m_charcterPrefabs[m_2PSelect], m_2PPosition, Quaternion.Euler(Vector3.left));
            m_charcter[1].GetComponent<PlayerController>().m_target = gameObject;
        }
        //1Pと2Pがどちらも選択されていなかったら
        if (!m_selectFinished[0] && !m_selectFinished[1]&& is1PCanceled==false&&is2PCanceled==false)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    /// <summary>
    /// トランジション処理完了コールバック関数
    /// </summary>
    void OnTransitionFinished()
    {
        //データの保存
        PlayerPrefs.SetInt("Character1", m_1PSelect);
        PlayerPrefs.SetInt("Character2", m_2PSelect);
        PlayerPrefs.Save();
        //データのロード
        m_1PSelect = PlayerPrefs.GetInt("Character1");
        m_2PSelect = PlayerPrefs.GetInt("Character2");

        //シーンの切り替え
        SceneManager.LoadScene("Game");
    }

    public void RemoveCharacters()
    {
        Destroy(m_charcter[0]); //破棄
        Destroy(m_charcter[1]); //破棄
        m_loadedCharcter[0] = -1;
        m_loadedCharcter[1] = -1;
    }
}