using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMeasure : MonoBehaviour
{
    //Spriteイメージ
    [SerializeField] private Sprite Memori;

    [SerializeField] private float N;

    private float pos = 0;

    //親のオブジェクト名
    private const string ObjName = "Distance_Pos";
    private Transform DistanceParent;

	// Use this for initialization
	void Start ()
    {
        //100分率

        //DistancePositionオブジェクト参照
        DistanceParent = GameObject.Find("DistancePosition").transform;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    //空のオブジェクト生成
    void CreateObj()
    {
        //空のオブジェクト生成
        GameObject obj = new GameObject();
        //名前設定
        obj.name = ObjName;
        //Sprite追加
        obj.AddComponent<SpriteRenderer>().sprite = Memori;
        //親設定
        obj.transform.parent = DistanceParent;
    }
}