using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarFishBehavior : MonoBehaviour {

    const byte _MAX_TAP = 5;        // タップできる最大数
    const byte _MAX_LEG = 5;        // 腕の最大数

    byte armNum = 0;                // 現在の腕

    SpriteRenderer[] LegSpriteRenderer; // 腕のスプライトレンダラー

    public Sprite[] LegImages;      // 腕の画像(0.. 通常時、1.. 選択時、2、3.. 爆発後)

	// Use this for initialization
	void Start () {
        GameDirector.Instance.SetArmNumber(_MAX_TAP + 1);               // 腕の本数を初期化(最初のタップは腕を消費しないため6に設定)

        LegSpriteRenderer = new SpriteRenderer[_MAX_LEG];               // 腕の本数分配列を確保
        
        for(int i = 0; i < _MAX_LEG; i++)                               // それぞれの腕のスプライトレンダラーを取得
        {
            LegSpriteRenderer[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        LegSpriteRenderer[0].sprite = LegImages[1];                     // 最初の腕を選択時の腕に画像を変更

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

                LegSpriteRenderer[armNum].sprite = LegImages[2];        // 現在の腕を爆発後の腕の画像に変更
                if (armNum < _MAX_LEG - 1)                              // 現在の腕が最後の腕じゃなかったら
                {
                    LegSpriteRenderer[armNum + 1].sprite = LegImages[1];// 次の腕を選択時の腕の画像に変更
                }
                armNum++;                                               // 次の腕へ
            }
            else                                                        // 最初のタップ
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();           // Rigidbodyを取得
                Vector2 force = new Vector2(-5.0f, 5.0f);                // 力を設定
                rb.AddTorque(0.8f, ForceMode2D.Impulse);                // 一瞬のみ回転を加える
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
