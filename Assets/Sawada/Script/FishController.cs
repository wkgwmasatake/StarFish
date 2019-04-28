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
	}



    /// <summary>
    ///   
    ///    速さ設定
    ///   
    /// </summary>
    public float SetSpeed { set { Speed = value; } }
}
