﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFishOriginal : MonoBehaviour {

    enum PARTICLE
    {
        ARM,
        BOMB,
        FIREWORK,
        WALLTOUTCH
    }

    const byte _MAX_TAP = 5;        // タップできる最大数
    const byte _MAX_LEG = 5;        // 腕の最大数

    byte selectArm = 0;             // 現在の腕
    float Presstime = 0;            // 画面を長押ししている時間

    float ForceX, ForceY;           // 力を加える方向

    float TimeCount = 0;            // タイムカウンタ

    byte i = 0;                     // 海星の座標を取得する際の要素番号
    Vector2[] position;             // 一定時間経過後に海星の座標を一時保存しておく変数
    float[] angle;                  // 一定時間経過後に海星の角度を一時保存しておく変数
    float rotatePower;              // 海星本体の回転量

    SpriteRenderer[] LegSpriteRenderer; // 腕のスプライトレンダラー

    [SerializeField] ParticleSystem[] ParticleList;     // パーティクルリスト(0.. 腕のパーティクル、1.. 爆発のパーティクル、2.. 花火のパーティクル)
    [SerializeField] GameObject ArrowObject;            // 矢印のゲームオブジェクト
    [SerializeField] float bombPower;                   // 爆発の大きさ
    [SerializeField] float ArrowDisplayTime;            // 矢印を表示させるまでの時間
    [SerializeField] float SavePosTime;                 // 座標を保存する間隔

    public Sprite[] LegImages;      // 腕の画像(0.. 通常時、1.. 選択時、2、3.. 爆発後)

    // Use this for initialization
    void Start () {
        GameDirector.Instance.SetArmNumber(_MAX_TAP + 2);       // 腕の本数を初期化(最初のタップと最後のタップは腕を消費しないため7に設定)

        LegSpriteRenderer = new SpriteRenderer[_MAX_LEG];       // 腕の本数分配列を確保

        for(int i = 0; i < _MAX_LEG; i++)                       // それぞれの腕のスプライトレンダラーを取得
        {
            LegSpriteRenderer[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        LegSpriteRenderer[0].sprite = LegImages[1];             // 最初の腕を選択時の腕に画像を変更

        ArrowObject.SetActive(false);                           // 非アクティブに設定

        position = new Vector2[100];        // 100個分の配列を確保
        angle = new float[100];             // 100個分の配列を確保
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameDirector.Instance.GetPauseFlg)      // ポーズ中でなければ通常通り実行
        {
            TimeCount += Time.deltaTime;                // 1フレーム間の時間を加算
            if (TimeCount > SavePosTime && i < 100)      // 一定時間経過後
            {
                TimeCount = 0;                                  // タイムカウンタをリセット
                position[i] = this.transform.position;          // 現在の座標を取得
                angle[i] = this.transform.localEulerAngles.z;   // 現在の角度を取得
                i++;                                            // 保存する配列の要素番号を1つ加算
            }
            if (GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1)
            {
                rotatePower *= 0.985f;                               // 回転力を減衰
                if (rotatePower < 1.5f)
                    rotatePower = 1.5f;
            }

            if (Input.GetMouseButton(0) && GameDirector.Instance.GetArmNumber() > 1 && GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1)
            {
                Presstime += Time.deltaTime;        // 長押ししている時間を計測
                if (Presstime > ArrowDisplayTime)   // 一定時間長押ししたら
                    ArrowObject.SetActive(true);    // アクティブに設定
            }

            if (Input.GetMouseButtonUp(0) && GameDirector.Instance.GetArmNumber() > 0)   // 左クリックしたとき、かつタップの最大数以下の時
            {
                Presstime = 0;          // 長押しの時間を初期化
                rotatePower = 0.3f * bombPower;

                if (GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1 && GameDirector.Instance.GetArmNumber() > 1)        // 最初のタップと最後のタップ以外の時
                {
                    if (i < 100)
                    {
                        TimeCount = 0;                                  // タイムカウンタをリセット
                        position[i] = this.transform.position;          // 爆発したときも座標を取得
                        angle[i] = this.transform.localEulerAngles.z;   // 現在の角度を取得
                        i++;                                            // 保存する配列の要素番号を1つ加算
                    }

                    if (!ArrowObject.activeSelf)         // 非アクティブ状態なら
                    {
                        ArrowObject.SetActive(true);    // アクティブ状態に設定
                    }

                    ArrowObject.GetComponent<ArrowDirector>().SetArrowPos(transform.GetChild(selectArm + 1));      // 次の腕に応じた矢印の位置に設定
                    ArrowObject.SetActive(false);       // 非アクティブに設定

                    Instantiate(ParticleList[(int)PARTICLE.ARM], transform);    // 海星の子に設定して泡のパーティクルを生成
                    Vector2 armPos = transform.GetChild(selectArm).position;       // ヒエラルキービューの上から子オブジェクトのワールド座標を取得
                    ForceX = transform.position.x - armPos.x;                   // 本体と腕のx座標の差を求める(力を加えるx方向)
                    ForceY = transform.position.y - armPos.y;                   // 本体と腕のy座標の差を求める(力を加えるy方向)

                    Vector2 force = new Vector2(ForceX * bombPower, ForceY * bombPower);  // 本体と腕の座標の差から力を設定

                    LegSpriteRenderer[selectArm].sprite = LegImages[2];        // 現在の腕を爆発後の腕の画像に変更
                    if (selectArm < _MAX_LEG - 1)                              // 現在の腕が最後の腕じゃなかったら
                    {
                        LegSpriteRenderer[selectArm + 1].sprite = LegImages[1];// 次の腕を選択時の腕の画像に変更
                    }
                    selectArm++;                                               // 次の腕へ
                    GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算

                }
                else if(GameDirector.Instance.GetArmNumber() > _MAX_TAP)        // 最初のタップ
                {
                    GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算
                }
                else                                                            // 最後の花火
                {
                    Instantiate(ParticleList[(int)PARTICLE.FIREWORK], transform);   // 海星の子に設定して花火のパーティクルを生成
                    //SaveCSV SavePos = this.GetComponent<SaveCSV>();               // スクリプトを取得
                    //SavePos.SavePos(position, angle, i);                          // 取得した座標をCSVファイルに書き込み
                    StartCoroutine("DestroyObject");                                // 1フレーム後に自分自身を非アクティブに設定
                }
            }

            transform.Rotate(new Vector3(0, 0, rotatePower));   // 海星を回転
        }
    }

    private IEnumerator DestroyObject()
    {
        // 1フレーム後に自分自身を非アクティブに設定
        yield return null;
        gameObject.SetActive(false);
    }
}
