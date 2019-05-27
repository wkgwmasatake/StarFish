using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieScript : MonoBehaviour
{
    private VideoPlayer movie;    // ビデオプレイヤー

    private AsyncOperation area_select;

	// Use this for initialization
	void Start ()
    {
        movie = GetComponent<VideoPlayer>();

        // エリアセレクト読み込み
        area_select = GameDirector.Instance.LoadAreaSelect();
        area_select.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 現在のフレームがビデオのフレーム総数を超えたら終了と判定
		if((ulong)movie.frame >= movie.frameCount)
        {
            Debug.Log("終了");
            area_select.allowSceneActivation = true;
        }
	}
}
