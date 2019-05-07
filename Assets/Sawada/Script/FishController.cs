using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    /// <summary>
    ///    パラメータ
    /// </summary>
    private float Speed;
    private int vector;

	// Use this for initialization
	void Start ()
    {

		// 生成された場所によってベクトル変更
        if(transform.position.x < 0)
        {
            vector = 1;
        }
        else if(transform.position.x >= 0)
        {
            vector = -1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Speed * vector * Time.deltaTime, 0, 0);

        if(vector == 1)
        {
            if (transform.position.x > getScreenBottomRight().x + 1f) Destroy(this.gameObject);
        }
        else if(vector == -1)
        {
            if (transform.position.x < getScreenTopLeft().x - 1f) Destroy(this.gameObject);
        }
	}

    Vector3 getScreenTopLeft()
    {
        //画面の左上を取得
        Vector3 topleft = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero);

        //上下反転
        topleft.Scale(new Vector3(1, -1, 1));
        return topleft;
    }


    Vector3 getScreenBottomRight()
    {
        //画面の右下を取得
        Vector3 bottomRight = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        //上下反転
        bottomRight.Scale(new Vector3(1, -1, 1));
        return bottomRight;
    }



    /// <summary>
    ///   
    ///    速さ設定
    ///   
    /// </summary>
    public float SetSpeed { set { Speed = value; } }
}
