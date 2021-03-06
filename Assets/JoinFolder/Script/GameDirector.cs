﻿using System.Collections;
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
    [SerializeField] private SceneObject AreaSelectScene;  //エリアセレクトの情報格納
    [SerializeField] private SceneObject MovieScene;       //ムービーシーン
    [SerializeField] private GameObject player;            //プレイヤーの情報格納
    [SerializeField] private string[] StageSceneName;      //各メインシーンの名前格納
    [SerializeField] private string[] StageName;              // 各ステージ名



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
    private static int StageClear_Flg = 1;     //各ステージのクリアフラグ
    private static int AreaClear_Flg = 1;      //各エリアのクリアフラグ
    private bool pauseFlg = false;               //ポーズフラグ
    private bool _particleFlg;           //パーティクルフラグ
    private bool _chaceFlg = true;       //カメラの追跡フラグ
    private bool _cameraFlg = false;          // カメラのアニメーションフラグ

    // ボタン用フラグ（２度押し防止用フラグ）
    private bool _retryFlg = false;
    private bool _nextFlg = false;
    private bool _menuFlg = false;
    private bool _menu_YesFlg = false;




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
        // STAGE_MAX分回す
        for (int i = 0; i < STAGE_MAX; i++)
        {
            //現在のシーンがメインなら
            if (SceneManager.GetActiveScene().name == StageSceneName[i])
            {
                cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
                goalLine = GameObject.Find("GoalLine");

                // nowSceneに現在のステージシーンの名前を保存
                SetSceneName = SceneManager.GetActiveScene().name;

                _SceneNumber = i + 2;

                Debug.Log("_sceneNumber : " + _SceneNumber);
                Debug.Log("SceneName" + GetSceneName);

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
    public AsyncOperation LoadAreaSelect()
    {
        _chaceFlg = false;
        return SceneManager.LoadSceneAsync(AreaSelectScene);
    }
    public AsyncOperation LoadMovieScene()
    {
        return SceneManager.LoadSceneAsync(MovieScene);
    }

    // アプリケーションが終了したとき
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetInt("STAGE", GameDirector.Instance.GetStageClear_Flg);       // ステージのクリア状況をPlayerPrefsに保存
            PlayerPrefs.SetInt("AREA", GameDirector.Instance.GetAreaClear_Flg);         // エリアの制覇状況をPlayerPrefsに保存
        }
    }

    // タイトル開始時にPlayerPrefsから情報を取得
    public void GetFlagInfo()
    {
        // PlayerPrefsの情報が最新なら
        if (PlayerPrefs.GetInt("STAGE") > StageClear_Flg)
        {
            StageClear_Flg = PlayerPrefs.GetInt("STAGE");       // ステージの情報を取得
        }

        // PlayerPrefsの情報が最新なら
        if (PlayerPrefs.GetInt("AREA") > AreaClear_Flg)
        {
            AreaClear_Flg = PlayerPrefs.GetInt("AREA");         // エリアの情報を取得
        }
    }


    // フェード
    public void UI_Fade()
    {
        StartCoroutine("PauseUI_Fade");
    }
    IEnumerator PauseUI_Fade()
    {
        GameObject obj = GameObject.Find("Canvas_beta").transform.GetChild(1).transform.GetChild(1).gameObject;
        obj.GetComponent<Button>().enabled = false;
        while(obj.GetComponent<CanvasGroup>().alpha > 0)
        {
            obj.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return null;
        }
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

    // ゲッター・セッター
    public bool GetCameraAnimFlg { get { return _cameraFlg; } }
    public bool SetCameraAnimFlg { set { _cameraFlg = value; } }

    // ボタン用フラグ ゲッター
    public bool GetRetryFlg { get { return _retryFlg; } }
    public bool GetNextFlg { get { return _nextFlg; } }
    public bool GetMenuFlg { get { return _menuFlg; } }
    public bool GetMenu_YesFlg { get { return _menu_YesFlg; } }
    // ボタン用フラグ セッター
    public bool SetRetryFlg { set { _retryFlg = value; } }
    public bool SetNextFlg { set { _nextFlg = value; } }
    public bool SetMenuFlg { set { _menuFlg = value; } }
    public bool SetMenu_YesFlg { set { _menu_YesFlg = value; } }

    #endregion

}