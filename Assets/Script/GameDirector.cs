using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public int StageStatus;     // ステージのクリア状況
    public int AreaStatus;      // エリアの制覇状況
    public int PearlStatus;     // 真珠の取得状況

    private Vector2 position;
    private int armNumber;

    private bool pauseFlg;

    private GameObject player;
    private Camera cam;          // メインカメラ
    private GameObject goalLine;

    public Text disTex;         // 空までの距離(UI)
    public Text armTex;         // 腕の残り本数(UI)

    //public Button retry;
    

    // Use this for initialization
    void Start () {

        player = GameObject.Find("starfish");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        goalLine = GameObject.Find("GoalLine");

        
        //disTex = GameObject.Find("ToSky").GetComponent<Text>();
        //armTex = GameObject.Find("RemainArm").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        if(goalLine == null)
        {
            Debug.Log("not goalline");
        }

        if (player != null)
        {
            var distance = ((int)cam.WorldToScreenPoint(goalLine.transform.position).y - (int)cam.WorldToScreenPoint(player.transform.position).y);
            disTex.text = /*"水面まで\n"　+*/
                distance.ToString() + "m";
            //if(distance < 0)
            //{
            //    disTex.gameObject.SetActive(false);
            //    armTex.gameObject.SetActive(false);
            //}
        }

        if (armNumber < 7)
        {
            armTex.text = /*"残り " +*/
                (armNumber-1).ToString();
        }
    }

    #region Getter/Setter

    public void SetPosition(Vector2 posi)
    {
        position = posi;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public void SetArmNumber(int num)
    {
        armNumber = num;
    }

    public int GetArmNumber()
    {
        return armNumber;
    }

    public bool SetPauseFlg { set { pauseFlg = value; } }
    public bool GetPauseFlg { get { return pauseFlg;  } }

    #endregion

}