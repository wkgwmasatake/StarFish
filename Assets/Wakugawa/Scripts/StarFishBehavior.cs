using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarFishBehavior : MonoBehaviour {

    enum PARTICLE
    {
        ARM,
        BOMB,
        FIREWORK
    }

    const byte _MAX_TAP = 5;        // タップできる最大数
    const byte _MAX_LEG = 5;        // 腕の最大数

    byte armNum = 0;                // 現在の腕
    float Presstime = 0;            // 画面を長押ししている時間

    float ForceX, ForceY;           // 力を加える方向

    SpriteRenderer[] LegSpriteRenderer; // 腕のスプライトレンダラー

    [SerializeField] ParticleSystem[] ParticleList;     // パーティクルリスト(0.. 腕のパーティクル、1.. 爆発のパーティクル、2.. 花火のパーティクル)
    [SerializeField] GameObject ArrowObject;            // 矢印のゲームオブジェクト
    [SerializeField] float bombPower;                   // 爆発の大きさ
    [SerializeField] float ArrowDisplayTime;            // 矢印を表示させるまでの時間

    public Sprite[] LegImages;      // 腕の画像(0.. 通常時、1.. 選択時、2、3.. 爆発後)

	// Use this for initialization
	void Start () {
        GameDirector.Instance.SetArmNumber(_MAX_TAP + 2);               // 腕の本数を初期化(最初のタップは腕を消費しないため6に設定)

        LegSpriteRenderer = new SpriteRenderer[_MAX_LEG];               // 腕の本数分配列を確保
        
        for(int i = 0; i < _MAX_LEG; i++)                               // それぞれの腕のスプライトレンダラーを取得
        {
            LegSpriteRenderer[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        LegSpriteRenderer[0].sprite = LegImages[1];                     // 最初の腕を選択時の腕に画像を変更

        ArrowObject.SetActive(false);                                   // 非アクティブに設定
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0) && GameDirector.Instance.GetArmNumber() > 1 && GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1)     // 最初のタップと最後のタップ以外
        {
            Presstime += Time.deltaTime;        // 長押ししている時間を計測
            if (Presstime > ArrowDisplayTime)   // 一定時間長押ししたら
                ArrowObject.SetActive(true);    // アクティブに設定
        }

		if(Input.GetMouseButtonUp(0) && GameDirector.Instance.GetArmNumber() > 0)        // 左クリックしたとき、かつタップの最大数以下の時
        {
            Presstime = 0;                      // 長押しの時間を初期化

            if (GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1 && GameDirector.Instance.GetArmNumber() > 1)        // 最初のタップと最後のタップ以外の時
            {
                if (!ArrowObject.activeSelf)         // 非アクティブ状態なら
                {
                    ArrowObject.SetActive(true);     // アクティブ状態に設定
                }

                ArrowObject.GetComponent<ArrowDirector>().SetArrowPos(transform.GetChild(armNum + 1));        // 次の腕に応じた矢印の位置に設定
                ArrowObject.SetActive(false);   // 非アクティブに設定

                Instantiate(ParticleList[(int)PARTICLE.ARM],transform); // 海星の子に設定して泡のパーティクルを生成
                Vector2 armPos = transform.GetChild(armNum).position;   // ヒエラルキービューの上から子オブジェクトのワールド座標を取得
                ForceX = transform.position.x - armPos.x;               // 本体と腕のx座標の差を求める(力を加えるx方向)
                ForceY = transform.position.y - armPos.y;               // 本体と腕のy座標の差を求める(力を加えるy方向)
                
                Rigidbody2D rb = GetComponent<Rigidbody2D>();           // Rigidbodyを取得
                Vector2 force = new Vector2(ForceX * bombPower, ForceY * bombPower);  // 本体と腕の座標の差から力を設定

                rb.angularVelocity = 0;                                 // 回転の力を0に戻す

                if (transform.position.x < armPos.x)                    // 腕が本体の右側にあれば
                {
                    rb.AddTorque(1.5f, ForceMode2D.Impulse);            // 時計回りに回転
                }
                else                                                    // そうでなければ
                {
                    rb.AddTorque(-1.5f, ForceMode2D.Impulse);           // 反時計回りに回転
                }
                rb.AddForce(force, ForceMode2D.Impulse);                // 一瞬のみ力を加える

                LegSpriteRenderer[armNum].sprite = LegImages[2];        // 現在の腕を爆発後の腕の画像に変更
                if (armNum < _MAX_LEG - 1)                              // 現在の腕が最後の腕じゃなかったら
                {
                    LegSpriteRenderer[armNum + 1].sprite = LegImages[1];// 次の腕を選択時の腕の画像に変更
                }
                armNum++;                                               // 次の腕へ
                GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算
            }
            else if(GameDirector.Instance.GetArmNumber() > _MAX_TAP)    // 最初のタップ
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();           // Rigidbodyを取得
                Vector2 force = new Vector2(-5.0f * bombPower / 20, 5.0f * bombPower / 20);                // 力を設定
                rb.AddTorque(0.8f, ForceMode2D.Impulse);                // 一瞬のみ回転を加える
                rb.AddForce(force, ForceMode2D.Impulse);                // 一瞬のみ力を加える
                GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算
            }
            else                // 最後の花火
            {
                Transform StarFishTransform = transform;
                Instantiate(ParticleList[(int)PARTICLE.FIREWORK], StarFishTransform);   // 海星の子に設定して花火のパーティクルを設定
                StartCoroutine("DestroyObject");                                        //1フレーム後に自分自身を破棄
            }
        }
    }

    private IEnumerator DestroyObject()
    {
        // 1フレーム後に自分自身を破棄
        yield return null;
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();           // Rigidbodyを取得
        if (col.collider.tag == "Rock")
        {
            if (transform.position.x < 0)        // 画面の左側で岩にあたった場合
            {
                rb.AddTorque(0.6f, ForceMode2D.Impulse);                    // 一瞬のみ時計回りに回転を加える
                rb.AddForce(new Vector2(0.07f * bombPower, 0.07f * bombPower), ForceMode2D.Impulse);  // 一瞬のみ力を加える
            }
            else
            {
                rb.AddTorque(-0.6f, ForceMode2D.Impulse);                   // 一瞬のみ反時計回りに回転を加える
                rb.AddForce(new Vector2(-0.07f * bombPower, 0.07f * bombPower), ForceMode2D.Impulse); // 一瞬のみ力を加える
            }
        }
    }
}
