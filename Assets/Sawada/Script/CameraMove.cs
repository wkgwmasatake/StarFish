using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    //背景のオブジェクト格納
    [SerializeField] private GameObject BGPre;

    [SerializeField] private GameObject player;

    [SerializeField] private string TagName;

    //背景オブジェクトの情報格納
    private BackGround BG;

    //カメラの余白分
    private int MARGIN = 5;

    //コリジョンのサイズ
    private Vector2 SIZE = new Vector2(1, 10);

    // Use this for initialization
    void Start()
    {
        //背景オブジェクトの情報参照
        BG = BGPre.GetComponent<BackGround>();

        CreateCollision();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーを追従
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        Vector3 player_pos = transform.position;
        player_pos.y = Mathf.Clamp(player_pos.y, -BG.height / 2 + MARGIN, BG.height / 2 + MARGIN);
        transform.position = new Vector3(player_pos.x, player_pos.y, player_pos.z);
    }

    Vector3 getScreenTopLeft()
    {
        //画面の左上を取得
        Vector3 topleft = GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero);

        //上下反転
        topleft.Scale(new Vector3(1, -1, 1));
        return topleft;
    }

    Vector3 getScreenBottomRight()
    {
        //画面の右下を取得
        Vector3 bottomRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        //上下反転
        bottomRight.Scale(new Vector3(1, -1, 1));
        return bottomRight;
    }

    void CreateCollision()
    {
        ////空のオブジェクト生成
        GameObject childR = new GameObject("RightWall");
        GameObject childL = new GameObject("LeftWall");

        //カメラの子オブジェクトに
        childR.transform.parent = this.transform;
        childL.transform.parent = this.transform;

        //初期位置設定
        childR.transform.position = transform.position;
        childL.transform.position = transform.position;

        //子オブジェクトの位置設定
        float posR = getScreenBottomRight().x;
        childR.transform.position = new Vector3(posR + 0.5f, childR.transform.position.y, childR.transform.position.z);

        float posL = getScreenTopLeft().x;
        childL.transform.position = new Vector3(posL - 0.5f, childL.transform.position.y, childL.transform.position.z);

        //コリジョン追加
        childR.AddComponent<BoxCollider2D>();
        childL.AddComponent<BoxCollider2D>();

        //コリジョンの大きさ
        childR.GetComponent<BoxCollider2D>().size = SIZE;
        childL.GetComponent<BoxCollider2D>().size = SIZE;

        //タグを追加
        childR.tag = TagName;
        childL.tag = TagName;
    }
}