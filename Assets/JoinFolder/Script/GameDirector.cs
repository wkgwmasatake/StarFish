using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{

    /// <summary>
    /// 定数
    /// </summary>
    private const int STAGE_MAX = 15;     //各エリア最大ステージ数
    private const int AREA_MAX = 5;      //最大エリア数
    



    /// <summary>
    /// 情報格納用変数
    /// </summary>
    [SerializeField] private SceneObject GameOverScene;    //ゲームオーバーの情報格納
    [SerializeField] private SceneObject GameResultScene;  //ゲームリザルトの情報格納
    [SerializeField] private GameObject player;            //プレイヤーの情報格納
    [SerializeField] private string[] StageSceneName;      //各メインシーンの名前格納



    /// <summary>
    /// 各ステータス
    /// </summary>
    public int StageStatus;     // ステージのクリア状況
    public int AreaStatus;      // エリアの制覇状況
    public int PearlStatus;     // 真珠の取得状況



    /// <summary>
    /// 距離変数
    /// </summary>
    private int _distance;
    private int _startDistance;




    /// <summary>
    /// フラグ
    /// </summary>
    private int StageClear_Flg = 1;     //各ステージのクリアフラグ
    private int AreaClear_Flg = 1;      //各エリアのクリアフラグ
    private bool pauseFlg = false;               //ポーズフラグ
    private bool _particleFlg;           //パーティクルフラグ
    private bool _chaceFlg = true;      //カメラの追跡フラグ
    private static int _Pearl_Flag;           // パール取得フラグ




    /// <summary>
    ///　その他変数
    /// </summary>
    private Camera cam;          // メインカメラ
    private Vector2 position;　　// 位置
    private GameObject goalLine; //ゴールライン
    private static int _armNumber;
    private static int _SceneNumber;      //シーンナンバー
    private static string nowScene;       //現在のシーンの情報格納


    private void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        goalLine = GameObject.Find("GoalLine");

        // STAGE_MAX分回す
        for (int i = 0; i < STAGE_MAX; i++)
        {
            //現在のシーンがメインなら
            if (SceneManager.GetActiveScene().name == StageSceneName[i])
            {
                // nowSceneに現在のステージシーンの名前を保存
                SetSceneName = SceneManager.GetActiveScene().name;

                _SceneNumber = i + 2;

                Debug.Log("_sceneNumber : " + _SceneNumber);

                // 最初のゴールまでの距離
                _startDistance = ((int)cam.WorldToScreenPoint(goalLine.transform.position).y - (int)cam.WorldToScreenPoint(player.transform.position).y);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Playerがシーン上にいたら
        if (player != null)
        {
            //ゴールからPlayerの位置を取得
            _distance = ((int)cam.WorldToScreenPoint(goalLine.transform.position).y - (int)cam.WorldToScreenPoint(player.transform.position).y);
        }
    }




    /// <summary>
    /// シーン遷移用メソッド
    /// </summary>
    public AsyncOperation LoadResult()
    {
        _chaceFlg = false;
        return SceneManager.LoadSceneAsync(GameResultScene);
    }
    public AsyncOperation LoadGameOrver()
    {
        _chaceFlg = false;
        return SceneManager.LoadSceneAsync(GameOverScene);
    }


    
    #region Getter/Setter

    //ポジションのゲッター・セッター
    public void SetPosition(Vector2 posi) { position = posi; }
    public Vector2 GetPosition() { return position; }

    //足の本数
    public int GetArmNumber() { return _armNumber; }
    public void SetArmNumber(int n) { _armNumber = n; }

    //distanceのゲッター・セッター
    public int GetDistance { get { return _distance; } }
    public int SetDistance { set { _distance = value; } }
    public int GetStartDistance { get { return _startDistance; } }

    //ポーズフラグのゲッター・セッター
    public bool SetPauseFlg { set { pauseFlg = value; } }
    public bool GetPauseFlg { get { return pauseFlg; } }

    //パーティクルの削除判定
    public bool ParticleFlg
    {
        get { return _particleFlg; }
        set { _particleFlg = value; }
    }

    // 現在のステージシーン
    public string GetSceneName { get { return nowScene; } }
    public string SetSceneName { set { nowScene = value; } }

    //各エリアの最大ステージ数
    public int GetSTAGE_MAX { get { return STAGE_MAX; } }

    //各エリアのクリア情報
    public int GetAreaClear_Flg { get { return AreaClear_Flg; } }
    public int SetAreaClear_Flg { set { AreaClear_Flg = value; } }

    //各ステージのクリア情報
    public int GetStageClear_Flg { get { return StageClear_Flg; } }
    public int SetStageClear_Flg { set { StageClear_Flg = value; } }

    //シーンナンバー
    public int GetSceneNumber { get { return _SceneNumber; } }
    public int SetSceneNumber { set { _SceneNumber = value; } }

    //カメラ追跡フラグゲッター・セッター
    public bool GetChaceFlg { get { return _chaceFlg; } }

    // 最大エリア数ゲッター
    public int GetAreaMax { get { return AREA_MAX; } }

    // パールの取得フラグゲッター・セッター
    public int GetPearlFlag { get { return _Pearl_Flag; } }
    public int SetPearlFlag { set { _Pearl_Flag = value; } }

    #endregion

}