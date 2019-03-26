using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarFishBehavior : MonoBehaviour {

    const byte _MAX_TAP = 5;        // タップできる最大数
    byte armNum = 0;                // 現在の腕

	// Use this for initialization
	void Start () {
        GameDirector.Instance.SetArmNumber(_MAX_TAP + 1);                              // 腕の本数を初期化(最初のタップは腕を消費しないため6に設定)
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && GameDirector.Instance.GetArmNumber() > 0)        // 左クリックしたとき、かつタップの最大数以下の時
        {
            if(GameDirector.Instance.GetArmNumber() <= _MAX_TAP)        // 最初のタップは消費しない
            {
                Vector2 armPos = transform.GetChild(armNum).position;   // ヒエラルキービューの上から子オブジェクトを取得
                float ForceX = transform.position.x - armPos.x;         // 本体と腕のx座標の差を求める(力を加えるx方向)
                float ForceY = transform.position.y - armPos.y;         // 本体と腕のy座標の差を求める(力を加えるy方向)

                Rigidbody2D rb = GetComponent<Rigidbody2D>();           // Rigidbodyを取得
                Vector2 force = new Vector2(ForceX * 30, ForceY * 30);  // 本体と腕の座標の差から力を設定
                rb.AddTorque(1.0f, ForceMode2D.Impulse);                // 一瞬のみ回転を加える
                rb.AddForce(force, ForceMode2D.Impulse);                // 一瞬のみ力を加える

                armNum++;                                               // 次の腕へ
            }
            else                                                        // 最初のタップ
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();           // Rigidbodyを取得
                Vector2 force = new Vector2(-5.0f, 5.0f);                // 力を設定
                rb.AddTorque(1.0f, ForceMode2D.Impulse);                // 一瞬のみ回転を加える
                rb.AddForce(force, ForceMode2D.Impulse);                // 一瞬のみ力を加える

            }
            GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算

            Debug.Log(armNum);
        }

        //---------- デバッグ用 ----------//
        if(Input.GetKeyDown(KeyCode.R))
        {
            GameDirector.Instance.SetArmNumber(_MAX_TAP);
            armNum = 0;
        }
        //--------------------------------//
    }
}
