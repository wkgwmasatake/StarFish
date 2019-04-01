using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    //背景のオブジェクト格納
    [SerializeField] private GameObject BGPre;

    [SerializeField] private GameObject player;

    //背景オブジェクトの情報格納
    private BackGround BG;

    //カメラの余白分
    private int MARGIN = 5;

    // Use this for initialization
    void Start () {

        //背景オブジェクトの情報参照
        BG = BGPre.GetComponent<BackGround>();
	}
	
	// Update is called once per frame
	void Update () {

        //プレイヤーを追従
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        if (BG.height / 2 - MARGIN < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if(-(BG.height / 2) + MARGIN >= transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
