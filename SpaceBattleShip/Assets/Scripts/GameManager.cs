using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject[] m_charcterPrefabs;  //キャラクターのプレハブ群
    [SerializeField]
    RectTransform m_1PhpUI;       // HP表示用   
    [SerializeField]
    RectTransform m_1PdamegeUI;   // HPのダメージ表示用    
    [SerializeField]
    RectTransform m_2PhpUI;       // HP表示用    
    [SerializeField]
    RectTransform m_2PdamegeUI;   // HPのダメージ表示用

    [SerializeField]
    Vector3 m_1PPosition;       //キャラの表示位置 
    [SerializeField]
    Vector3 m_2PPosition;       //キャラの表示位置

    [SerializeField]
    KeyCode m_1PforwardKey = KeyCode.D;      //前移動
    [SerializeField]
    KeyCode m_1PbackKey = KeyCode.A;          //後ろ移動    
    [SerializeField]
    KeyCode m_1PrearKey = KeyCode.W;            //奥移動
    [SerializeField]
    KeyCode m_1PfrontKey = KeyCode.S;         //手前移動
    [SerializeField]
    KeyCode m_1PupKey = KeyCode.Q;         //上移動
    [SerializeField]
    KeyCode m_1PdownKey = KeyCode.E;       //下移動
    [SerializeField]
    KeyCode m_1Pattack1Key = KeyCode.R;    //攻撃１
    [SerializeField]
    KeyCode m_1Pattack2Key = KeyCode.F;        //攻撃２

    [SerializeField]
    KeyCode m_2PforwardKey = KeyCode.RightArrow;      //前移動   
    [SerializeField]
    KeyCode m_2PbackKey = KeyCode.LeftArrow;          //後ろ移動
    [SerializeField]
    KeyCode m_2PrearKey = KeyCode.UpArrow;            //奥移動
    [SerializeField]
    KeyCode m_2PfrontKey = KeyCode.DownArrow;         //手前移動
    [SerializeField]
    KeyCode m_2PupKey = KeyCode.RightShift;         //上移動
    [SerializeField]
    KeyCode m_2PdownKey = KeyCode.KeypadEnter;       //下移動
    [SerializeField]
    KeyCode m_2Pattack1Key = KeyCode.RightControl;    //攻撃１
    [SerializeField]
    KeyCode m_2Pattack2Key = KeyCode.RightAlt;        //攻撃２

    [SerializeField]
    Cameracontroller m_cameraController;        //カメラ制御

    PlayerController[] m_playerController = new PlayerController[2];


    // Use this for initialization
    void Start () {
        int selectNo1P = PlayerPrefs.GetInt("1P", 0);
        int selectNo2P = PlayerPrefs.GetInt("2P", 0);

        //1Pキャラの読み込み
        GameObject player1 = Instantiate(m_charcterPrefabs[selectNo1P], m_1PPosition, Quaternion.identity);
        //キーコンフィグ設定
        UserController pad1 = player1.GetComponent<UserController>();
        // pad1.m_forwardKey = m_1PforwardKey;
        // pad1.m_backKey = m_1PbackKey;
        pad1.m_upKey = m_1PupKey;
        pad1.m_downKey = m_1PdownKey;
        //pad1.m_rearKey = m_1PrearKey;
        //pad1.m_frontKey = m_1PfrontKey;
        pad1.m_attack1Key = m_1Pattack1Key;
        pad1.m_attack2Key = m_1Pattack2Key;
        pad1.m_attack2Key = m_1Pattack2Key;

        // HPController hp1 = player1.GetComponent<HPController>();
        // hp1.m_hpUI = m_1PhpUI;
        // hp1.m_damegeUI = m_1PdamegeUI;

        //2Pキャラの読み込み
        GameObject player2 = Instantiate(m_charcterPrefabs[selectNo2P], m_2PPosition, Quaternion.identity);
        //キーコンフィグ設定
        UserController pad2 = player2.GetComponent<UserController>();
        // pad2.m_forwardKey = m_2PforwardKey;
        // pad2.m_backKey = m_2PbackKey;
        pad2.m_upKey = m_2PupKey;
        pad2.m_downKey = m_2PdownKey;
        //pad2.m_rearKey = m_2PrearKey;
        //pad2.m_frontKey = m_2PfrontKey;
        pad2.m_attack1Key = m_2Pattack1Key;
        pad2.m_attack2Key = m_2Pattack2Key;
        pad2.m_attack2Key = m_2Pattack2Key;

        // HPController hp2 = player2.GetComponent<HPController>();
        // hp2.m_hpUI = m_2PhpUI;
        // hp2.m_damegeUI = m_2PdamegeUI;

        //1Pと2Pを互いにターゲットにする
        m_playerController[0] = player1.GetComponent<PlayerController>();
        m_playerController[0].Target = player2;
        m_playerController[1] = player2.GetComponent<PlayerController>();
        m_playerController[1].Target = player1;

        //カメラ設定
        m_cameraController.m_target1 = player1;
        m_cameraController.m_target2 = player2;

        //UnityAction callbackMethod = OnTransitionFinished;

        // トランジション
        //TransitionManager.Instance.Fade(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), 2f, callbackMethod);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
