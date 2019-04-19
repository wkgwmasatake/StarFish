using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRecord : MonoBehaviour {

    //LoadCSV変数
    [SerializeField] private GameObject LoadCSV_Obj;
    private LoadCSV loadcsv;

    [SerializeField] private GameObject ghost;

    //time変数
    [SerializeField] private float span;
    private float time;

    //描画する画像
    [SerializeField] private Sprite pos;

    private int roupNmber;

    private Transform GhostParent;

    private int i;

    // Use this for initialization
    void Start ()
    {
        i = 0;
        time = 0;

        loadcsv = LoadCSV_Obj.GetComponent<LoadCSV>();

        roupNmber = loadcsv.BinaryLoad();

        //軌跡のオブジェクトを格納する親オブジェクト
        GhostParent = GameObject.Find("Trajectory").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //LoadCSVのLoadメソッド呼び出し
        Draw();
    }

    //描画メソッド
    void Draw()
    {
        time += Time.deltaTime;
        if (time >= span && i < roupNmber)
        {
            time = 0;
            CreateObj(i);

            i++;
        }
    }

    //オブジェクト生成メソッド
    void CreateObj(int i)
    {
        //Ghost生成
        Instantiate(ghost, new Vector3(loadcsv.LoadPos[i, 0], loadcsv.LoadPos[i, 1], 1), new Quaternion(0, 0, loadcsv.LoadAngle[i], 1)).transform.parent = GhostParent;
    }
}
