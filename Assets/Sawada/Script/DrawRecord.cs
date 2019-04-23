using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRecord : MonoBehaviour
{


    /// <summary>
    /// LoadCSV用変数
    /// </summary>
    [SerializeField] private GameObject LoadCSV_Obj;
    private LoadCSV loadcsv;



    /// <summary>
    /// Time用変数
    /// </summary>
    [SerializeField] private float span;
    private float time = 0;


    /// <summary>
    /// 描画する位置を格納する変数
    /// </summary>
    [SerializeField] private Sprite pos;



    /// <summary>
    /// その他変数
    /// </summary>
    private int roupNmber;
    private Transform GhostParent;
    private int i = 0;
    [SerializeField] private GameObject ghost;



    // Use this for initialization
    void Start ()
    {
        loadcsv = LoadCSV_Obj.GetComponent<LoadCSV>();

        roupNmber = loadcsv.BinaryLoad();

        // Trajectoryがなければ生成
        if (GameObject.Find("Trajectory") != null)
        {
            //軌跡のオブジェクトを格納する親オブジェクト
            GhostParent = GameObject.Find("Trajectory").transform;
        }
        else
        {
            //軌跡のオブジェクトを格納する親オブジェクト
            GameObject Trajectory = new GameObject();
            Trajectory.name = "Trajectory";
            GhostParent = Trajectory.transform;
        }
    }
	



	// Update is called once per frame
	void Update ()
    {
        Draw();　　　　　　//描画メソッドの呼び出し
    }




    /// <summary>
    /// 
    ///     描画メソッド
    /// 
    /// </summary>
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




    /// <summary>
    /// 
    ///       オブジェクト生成メソッド
    /// 
    /// </summary>
    /// <param name="i"></param>
    void CreateObj(int i)
    {
        //Ghost生成
        Instantiate(ghost, new Vector3(loadcsv.LoadPos[i, 0], loadcsv.LoadPos[i, 1], 1), new Quaternion(0, 0, loadcsv.LoadAngle[i], 1)).transform.parent = GhostParent;
    }
}
