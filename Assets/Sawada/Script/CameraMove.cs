using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    //背景のオブジェクト格納
    [SerializeField] private GameObject BGPre;

    //カメラの追従速度
    [SerializeField] private float CameraSpeed;

    //背景オブジェクトの情報格納
    private BackGround BG;

    //カメラの余白分
    private int MARGIN = 5;

    // Use this for initialization
    void Start () {

        BG = BGPre.GetComponent<BackGround>();
	}
	
	// Update is called once per frame
	void Update () {

        if( BG.height/2 - MARGIN > transform.position.y ) transform.Translate(0, CameraSpeed * Time.deltaTime, 0);
	}
}
