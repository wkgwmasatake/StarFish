using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFishBehavior : MonoBehaviour {

    const byte _MAX_TAP_COUNT = 6;  // タップできる最大数(スタートのタップ + 足の数)

    byte TapCount = 0;              // タップした回数
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && TapCount <= _MAX_TAP_COUNT)         // 右クリックしたとき、かつタップの最大数以下の時
        {
            TapCount++;                                         // タップカウンタを1加算
            Rigidbody2D rb = GetComponent<Rigidbody2D>();       // Rigidbodyを取得
            Vector2 force = new Vector2(0.0f, 10.0f);           // 力を設定
            rb.AddTorque(1.0f, ForceMode2D.Impulse);            // 一瞬のみ回転を加える
            rb.AddForce(force, ForceMode2D.Impulse);            // 一瞬のみ力を加える
        }

        //---------- デバッグ用 ----------//
        if(Input.GetKeyDown(KeyCode.R))
        {
            TapCount = 0;
        }
        //--------------------------------//
	}
}
