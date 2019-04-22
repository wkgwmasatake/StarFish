using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    // 現在のステージシーンを確保
    [SerializeField] private static string nowScene;

    [SerializeField] private SceneObject GameOverScene;
    [SerializeField] private SceneObject GameResultScene;

    [SerializeField] private GameObject player;

    //各メインシーンの名前格納
    [SerializeField] private string[] StageSceneName;


    public int StageStatus;     // ステージのクリア状況
    public int AreaStatus;      // エリアの制覇状況
    public int PearlStatus;     // 真珠の取得状況

    private Vector2 position;
    private int armNumber;

    private int _distance;
    private int _startDistance;

    private bool pauseFlg;

    private bool _particleFlg;

    private Camera cam;          // メインカメラ
    private GameObject goalLine;
    private Text DistanceText;

    //最大ステージ数
    private const int STAGE_MAX = 2;

    // Use this for initialization
    void Start ()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        goalLine = GameObject.Find("GoalLine");

        for (int i = 0; i < STAGE_MAX; i++)
        {
            //現在のシーンがメインなら
            if (SceneManager.GetActiveScene().name == StageSceneName[i])
            {
                nowScene = SceneManager.GetActiveScene().name;
                _startDistance = ((int)cam.WorldToScreenPoint(goalLine.transform.position).y - (int)cam.WorldToScreenPoint(player.transform.position).y);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (goalLine == null)
        {
            Debug.Log("not goalline");
        }

        if (player != null)
        {
            _distance = ((int)cam.WorldToScreenPoint(goalLine.transform.position).y - (int)cam.WorldToScreenPoint(player.transform.position).y);
        }

        //残りの足の本数がなくなり、パーティクルが削除されたら
        if (armNumber == 1 && _particleFlg == true)
        {
            //位置判定メソッド
            SelectLoadScene(_distance);
        }

        Debug.Log("nowScene" + nowScene);

    }

    void SelectLoadScene(int distance)
    {
        //Playerの位置判定
        if (distance <= 0)
        {
            //GameResultがアタッチされていたら
            if(GameResultScene != null)
            {
                SceneManager.LoadScene(GameResultScene);
            }
            //されていなければ
            else
            {
                Debug.LogError("Not GameResultScene");
            }
        }
        else
        {
            //GameOrverがアタッチされていたら
            if(GameOverScene != null)
            {
                SceneManager.LoadScene(GameOverScene);
            }
            //されていなければ
            else
            {
                Debug.LogError("Not GameOrverScene");
            }
        }
    }

    #region Getter/Setter

    //ポジションのゲッター・セッター
    public void SetPosition(Vector2 posi)
    {
        position = posi;
    }
    public Vector2 GetPosition()
    {
        return position;
    }

    //ArmNumberのゲッター・セッター
    public void SetArmNumber(int num)
    {
        armNumber = num;
    }
    public int GetArmNumber()
    {
        return armNumber;
    }

    //distanceのゲッター・セッター
    public int GetDistance { get { return _distance; } }
    public int SetDistance { set { _distance = value; } }
    public int GetStartDistance { get{ return _startDistance; } }

    //ポーズフラグのゲッター・セッター
    public bool SetPauseFlg { set { pauseFlg = value; } }
    public bool GetPauseFlg { get { return pauseFlg;  } }

    //パーティクルの削除判定
    public bool ParticleFlg
    {
        get { return _particleFlg; }
        set { _particleFlg = value; }
    }

    // 現在のステージシーン
    public string GetSceneName
    {
        get { return nowScene; }
    }
    public string SetSceneName
    {
        set { nowScene = value; }
    }

    #endregion

}