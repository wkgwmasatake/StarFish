using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    [SerializeField] private SceneObject GameOverScene;
    [SerializeField] private SceneObject GameResultScene;

    [SerializeField] private GameObject player;

    [SerializeField] private Text disTex;

    public int StageStatus;     // ステージのクリア状況
    public int AreaStatus;      // エリアの制覇状況
    public int PearlStatus;     // 真珠の取得状況

    private Vector2 position;
    private int armNumber;

    private int _distance;

    private bool pauseFlg;

    private bool _particleFlg;

    private Camera cam;          // メインカメラ
    private GameObject goalLine;
    private Text DistanceText;

    // Use this for initialization
    void Start () {

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        goalLine = GameObject.Find("GoalLine");
        DistanceText = disTex.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(goalLine == null)
        {
            Debug.Log("not goalline");
        }

        if (player != null)
        {
            _distance = ((int)cam.WorldToScreenPoint(goalLine.transform.position).y - (int)cam.WorldToScreenPoint(player.transform.position).y);
            DistanceText.text = "地上まで \n" + GetDistance.ToString() + "m";
        }

        //残りの足の本数がなくなり、パーティクルが削除されたら
        if (armNumber == 1 && _particleFlg == true)
        {
            //位置判定メソッド
            SelectLoadScene(_distance);
        }
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

    //ポーズフラグのゲッター・セッター
    public bool SetPauseFlg { set { pauseFlg = value; } }
    public bool GetPauseFlg { get { return pauseFlg;  } }

    //パーティクルの削除判定
    public bool ParticleFlg
    {
        get { return _particleFlg; }
        set { _particleFlg = value; }
    }

    #endregion

}