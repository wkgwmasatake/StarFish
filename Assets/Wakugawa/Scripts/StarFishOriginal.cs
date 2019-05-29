using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarFishOriginal : MonoBehaviour {

    enum PARTICLE
    {
        ARM,
        BOMB,
        WALLTOUTCH,
        GOAL
    }

    enum GAME_STATUS
    {
        _PLAY,
        _CREAR_EFFECT,
        _CLEAR,
        _OVER
    }

    const byte _MAX_TAP = 5;        // タップできる最大数
    const byte _MAX_LEG = 5;        // 腕の最大数
    const float START_X = 1.65f;    // 海星のスタートのx座標
    const float START_Y = -23.55f;  // 海星のスタートのy座標

    byte Status = 0;

    byte selectArm = 0;             // 現在の腕
    float Presstime = 0;            // 画面を長押ししている時間

    float ForceX = 0, ForceY = 0;   // 力を加える方向

    byte i = 0;                     // 海星の座標を取得する際の要素番号
    float rotatePower = 0;          // 海星本体の回転量
    Vector2 armPos;
    float ParticleAngle;
    SpriteRenderer[] LegSpriteRenderer; // 腕のスプライトレンダラー
    Rigidbody2D rb;
    bool OceanFlag;                 // 海流に入った際に使うフラグ
    bool FadeFlag = true;           // 足のフェードイン、フェードアウトのフラグ(trueでフェードアウト、falseでフェードイン)
    float FadeAlpha = 1.0f;         // 足のアルファ値(0～1)


    [SerializeField] ParticleSystem[] ParticleList;     // パーティクルリスト(0.. 腕のパーティクル、1.. 爆発のパーティクル、2.. 壁にあたった時のパーティクル、3.. ゴールラインを超えたときのパーティクル)
    [SerializeField] GameObject ArrowObject;            // 矢印のゲームオブジェクト
    [SerializeField] AudioClip[] ClearSound;            // クリア演出の際に流す効果音
    [SerializeField] float bombPower;                   // 爆発の大きさ
    [SerializeField] float ArrowDisplayTime;            // 矢印を表示させるまでの時間
    [SerializeField] float SavePosDistance;             // 座標を保存する間隔
    [SerializeField] Image FadeImage;                   // フェード画像

    //------- デバッグ用 -------//
    [SerializeField] bool flag;
    //--------------------------//

    public Sprite[] LegImages;          // 腕の画像(0.. 通常時、1.. 選択時、2、3.. 爆発後)

    // Use this for initialization
    void Start () {
        GameDirector.Instance.SetArmNumber(_MAX_TAP + 2);       // 腕の本数を初期化(最初のタップと最後のタップは腕を消費しないため7に設定)

        LegSpriteRenderer = new SpriteRenderer[_MAX_LEG];       // 腕の本数分配列を確保

        for(int i = 0; i < _MAX_LEG; i++)                       // それぞれの腕のスプライトレンダラーを取得
        {
            LegSpriteRenderer[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        ArrowObject.SetActive(false);                           // 非アクティブに設定

        rb = GetComponent<Rigidbody2D>();   // Rigidbody2Dを取得

        this.GetComponent<AudioSource>().PlayOneShot(ClearSound[3]);    // 最初のジャンプの音

    }

    // Update is called once per frame
    void Update () {

        switch (Status)
        {
            case (byte)GAME_STATUS._PLAY:   // ゲームプレイ時
                if (!GameDirector.Instance.GetPauseFlg)      // ポーズ中でなければ通常通り実行
                {
                    if (GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1)
                    {
                        rotatePower *= 0.97f;                               // 回転力を減衰

                        // 回転力が一定以下なら最低値を設定
                        if (rotatePower < 4.0f && rotatePower > 0)
                        {
                            rotatePower = 4.0f;
                        }
                        else if (rotatePower > -4.0f && rotatePower < 0)
                        {
                            rotatePower = -4.0f;
                        }
                    }

                    if (GameDirector.Instance.GetArmNumber() > 1 && GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1)   // 最初のタップと最後のタップ以外
                    {
                        Presstime += Time.deltaTime;        // 前回のタップから経過した時間を計測
                        if (Presstime > ArrowDisplayTime)   // 一定時間経過したら
                            ArrowObject.SetActive(true);    // 矢印を表示

                        if(FadeFlag)        // フェードアウト処理
                        {
                            FadeAlpha -= 0.05f;

                            if(FadeAlpha <= 0)
                            {
                                FadeAlpha = 0;
                                FadeFlag = false;
                            }
                        }
                        else                // フェードイン処理
                        {
                            FadeAlpha += 0.05f;

                            if(FadeAlpha >= 1)
                            {
                                FadeAlpha = 1;
                                FadeFlag = true;
                            }
                        }

                        Color fadecolor = gameObject.transform.GetChild(selectArm).GetComponent<SpriteRenderer>().color;
                        gameObject.transform.GetChild(selectArm).GetComponent<SpriteRenderer>().color = new Color(fadecolor.r, FadeAlpha, FadeAlpha);
                    }

                    if (Input.GetMouseButtonUp(0) && GameDirector.Instance.GetArmNumber() > 0)   // 左クリックしたとき、かつタップの最大数以下の時
                    {

                        if (GameDirector.Instance.GetArmNumber() <= _MAX_TAP + 1 && GameDirector.Instance.GetArmNumber() > 1)        // 最初のタップと最後のタップ以外の時
                        {
                            OceanFlag = true;       // 海流にそって加速するように変更

                            Presstime = 0;          // 経過時間を初期化

                            rb.velocity = Vector2.zero;                         // 重力加速度をリセット

                            if (i < 100)
                            {
                                i++;                                            // 保存する配列の要素番号を1つ加算
                            }

                            if (!ArrowObject.activeSelf)         // 非アクティブ状態なら
                            {
                                ArrowObject.SetActive(true);    // アクティブ状態に設定
                            }

                            ArrowObject.GetComponent<ArrowDirector>().SetArrowPos(transform.GetChild(selectArm + 1));      // 次の腕に応じた矢印の位置に設定
                            ArrowObject.SetActive(false);       // 非アクティブに設定

                            armPos = transform.GetChild(selectArm).position;    // ヒエラルキービューの上から子オブジェクトのワールド座標を取得
                            ForceX = transform.position.x - armPos.x;                   // 本体と腕のx座標の差を求める(力を加えるx方向)
                            ForceY = transform.position.y - armPos.y;                   // 本体と腕のy座標の差を求める(力を加えるy方向)

                            var effect = Instantiate(ParticleList[(int)PARTICLE.BOMB]); // 1個目の泡のパーティクルを生成
                            effect.transform.position = armPos;                         // 生成したパーティクルを腕の位置に設定
                            VariousFixer vf = effect.GetComponent<VariousFixer>();      // スクリプトを取得
                            ParticleAngle = GetAngle(transform.position, armPos);       // 角度を取得
                            vf.RotationY(ParticleAngle);                                // 角度を変更

                            var effect02 = Instantiate(ParticleList[(int)PARTICLE.BOMB]);   // 2個目の泡のパーティクルを生成
                            effect02.transform.position = transform.position;
                            VariousFixer vf02 = effect02.GetComponent<VariousFixer>();
                            vf02.RotationY(ParticleAngle + 15.0f);

                            var effect03 = Instantiate(ParticleList[(int)PARTICLE.BOMB]);   // 3個目の泡のパーティクルを生成
                            effect03.transform.position = transform.position;
                            VariousFixer vf03 = effect03.GetComponent<VariousFixer>();
                            vf03.RotationY(ParticleAngle - 15.0f);

                            if (transform.position.x < armPos.x)    // 本体の右側で腕が爆発したら
                            {
                                rotatePower = 20f * bombPower;      // 時計回りに回転
                            }
                            else                                    // 本体の左側で腕が爆発したら
                            {
                                rotatePower = -20f * bombPower;     // 反時計回りに回転
                            }

                            LegSpriteRenderer[selectArm].sprite = LegImages[2];        // 現在の腕を爆発後の腕の画像に変更
                            transform.GetChild(selectArm).GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);     // 現在の腕の表示を標準に変更

                            if (selectArm < _MAX_LEG - 1)                              // 現在の腕が最後の腕じゃなかったら
                            {
                                LegSpriteRenderer[selectArm + 1].sprite = LegImages[1];// 次の腕を選択時の腕の画像に変更
                                transform.GetChild(selectArm + 1).GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);     // 次の腕の表示を1.5倍に拡大
                            }

                            selectArm++;                                               // 次の腕へ
                            GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算

                        }
                        else if (GameDirector.Instance.GetArmNumber() > _MAX_TAP)        // 最初のタップ
                        {
                            GetComponent<Animator>().enabled = false;       // アニメーターをオフにする

                            this.GetComponent<AudioSource>().PlayOneShot(ClearSound[2]);    // 最初のジャンプの音

                            GetComponent<CreateEffect>().InstantiateParticle();

                            GameDirector.Instance.SetArmNumber(GameDirector.Instance.GetArmNumber() - 1);               // 腕の本数を1減算
                            rotatePower = 12f * bombPower;      // 回転を設定
                                                                // 左上に力を加える
                            ForceX = -0.1f;
                            ForceY = 0.15f;

                            transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);     // 最初の腕の表示を1.5倍に拡大
                            LegSpriteRenderer[0].sprite = LegImages[1];             // 最初の腕を選択時の腕に画像を変更

                            FadeAlpha = 1.0f;
                            FadeFlag = true;

                        }
                    }

                    transform.Rotate(new Vector3(0, 0, rotatePower));   // 海星を回転

                    transform.Translate(ForceX * bombPower, ForceY * bombPower, 0, Space.World);         // 爆発の威力に応じて移動
                    if (transform.position.y < START_Y)                      // 海星のY座標がスタートの座標より下にいるなら
                    {
                        Vector2 pos = transform.position;                   // 海星の座標を保存
                        transform.position = new Vector2(pos.x, START_Y);   // x座標はそのままでY座標をスタートの座標に変更
                    }

                    if(rb.velocity.y < -1.0f)                   // 重力加速度がy方向に-1.0以上かかっていたら
                    {
                        rb.velocity = new Vector2(0, -1.0f);    // 重力加速度を-1.0に戻す
                    }

                    // 力を減衰
                    ForceX *= 0.95f;
                    ForceY *= 0.95f;

                    // X方向への力が一定以下になったら0にする
                    if (ForceX < 0.001f && ForceX > -0.001f)
                    {
                        ForceX = 0;
                    }

                    // Y方向への力が一定以下になったら0にして、海流のフラグをfalseにする
                    if (ForceY < 0.001f && ForceY > -0.001f)
                    {
                        OceanFlag = false;
                        ForceY = 0;
                    }

                    // ゴールラインを超えたら
                    if (GameDirector.Instance.GetDistance < 0)
                    {
                        Status = (byte)GAME_STATUS._CREAR_EFFECT;       // クリア演出処理へ

                        // BGMをフェードアウトさせる
                        GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Fade();

                        ForceY = 0.3f;      // Y方向を固定値に変更
                        FadeAlpha = 1.0f;   // フェード値をリセット

                        // 回転スピードを一定値に設定
                        if(rotatePower > 0)
                        {
                            rotatePower = 0.5f;
                        }
                        else
                        {
                            rotatePower = -0.5f;
                        }

                        FadeImage.rectTransform.anchoredPosition =
                            new Vector2(FadeImage.rectTransform.anchoredPosition.x, -(FadeImage.rectTransform.anchoredPosition.y));  // 画像の位置を上に移動

                        FadeImage.transform.localScale = new Vector3(1, 1, 1);  // Yスケールを1に設定することで画像の上下を逆転させる

                        Image FadeChild = FadeImage.transform.GetChild(0).GetComponent<Image>();        // 子のImage情報を取得
                        FadeImage.color = new Color(255, 255, 255);             // 親の色を白に設定
                        FadeChild.color = new Color(255, 255, 255);             // 子の色を白に設定

                        // 矢印を非アクティブに設定
                        if(ArrowObject.activeSelf)
                        {
                            ArrowObject.SetActive(false);
                        }

                        GameDirector.Instance.UI_Fade();        // 一時停止ボタンをフェードアウト
                    }

                    // 残りの可能タップ数が1以下になった時かつ、Yに対する力が 0.001f ～ -0.001f になった時に
                    if (GameDirector.Instance.GetArmNumber() <= 1 && ForceY < 0.001f && ForceY > -0.001f)
                    {
                        GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Fade();
                        Status = (byte)GAME_STATUS._OVER;       // ゲームオーバー処理へ
                        GameDirector.Instance.UI_Fade();        // 一時停止ボタンをフェードアウト
                    }
                }
                else
                {
                    GameObject canvas = GameObject.Find("Canvas_beta");
                    if(canvas.transform.childCount > 2 && canvas.transform.GetChild(2).gameObject.activeSelf)
                    {
                        gameObject.GetComponent<Animator>().SetTrigger("IdleTrigger");
                    }
                }
                break;

            case (byte)GAME_STATUS._CREAR_EFFECT:
                rb.velocity = Vector2.zero;         // 重力を無効化

                if(!this.GetComponent<AudioSource>().isPlaying)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(ClearSound[0]);    // 回転音を再生
                }

                if (rotatePower < 10.0f && rotatePower > -10.0f)
                {
                    rotatePower *= 2.0f;
                }


                if (ForceY > 0.01f)     // Y方向の力が一定以上なら
                {
                    // 力を減衰させる
                    ForceX *= 0.9f;
                    ForceY *= 0.9f;
                }
                else                    // 一定以下になったら
                {
                    if(ForceY != 0)     // Y方向の力が0になっていなかったら
                    {
                        // 力を0に変更
                        ForceX = 0;
                        ForceY = 0;
                    }

                    FadeAlpha -= 0.025f;    // アルファ値を減少

                    Color fadecolor = this.gameObject.GetComponent<SpriteRenderer>().color;         // 自分の色を取得
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(fadecolor.r, fadecolor.g, fadecolor.b, FadeAlpha);      // 減らしたアルファ値を変更

                    for(int i = 0; i < 5; i++)
                    {
                        fadecolor = this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                        this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(fadecolor.r, fadecolor.g, fadecolor.b, FadeAlpha);
                    }

                    fadecolor = this.gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>().color;     // 子の星の色を取得
                    gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>().color = new Color(fadecolor.r, fadecolor.g, fadecolor.b, 1.0f - FadeAlpha);    // 子の星のアルファ値を変更
                    if(FadeAlpha <= 0)
                    {
                        Status = (byte)GAME_STATUS._CLEAR;
                        var effect = Instantiate(ParticleList[(int)PARTICLE.GOAL]);
                        effect.transform.position = gameObject.transform.position;
                    }
                }

                transform.Translate(ForceX * bombPower, ForceY * bombPower, 0, Space.World);         // 爆発の威力に応じて移動
                transform.Rotate(new Vector3(0, 0, rotatePower));   // 海星を回転

                break;

            case (byte)GAME_STATUS._CLEAR:      // クリア処理

                ForceY = 0.4f;

                this.GetComponent<AudioSource>().PlayOneShot(ClearSound[1]);    // 飛んでいく音を再生

                int StageNum = GameDirector.Instance.GetSceneNumber - 1;        // 現在のステージ番号を取得

                GetComponent<SaveStageInfo>().SaveSatageClearInfo(StageNum);    // ステージクリアを保存

                if(StageNum % 3 == 0)               // 各エリアの最終ステージなら
                {
                    GetComponent<SaveStageInfo>().SaveAreaClearInfo(StageNum / 3);  // エリアのクリアを保存
                }

                StartCoroutine("LoadResult");                           // コルーチンでリザルトシーンを読み込む
                Status = 99;                                            // シーンの2度読み防止
                break;

            case (byte)GAME_STATUS._OVER:       // ゲームオーバー処理                
                StartCoroutine("LoadOver");                             // コルーチンでゲームオーバーシーンを読み込む

                Status = 99;                                            // シーンの2度読み防止
                break;
        }

        if (transform.position.y < START_Y)                      // 海星のY座標がスタートの座標より下にいるなら
        {
            Vector2 pos = transform.position;                   // 海星の座標を保存
            transform.position = new Vector2(pos.x, START_Y);   // X座標はそのままでY座標をスタートの座標に変更
        }
    }

    private IEnumerator LoadResult()
    {
        AsyncOperation result = GameDirector.Instance.LoadResult();     // リザルトシーンを非同期で読み込む
        result.allowSceneActivation = false;                            // フェード処理が終わるまではシーン遷移を許可しない(注意点としてallowSceneActivationがfalseのままだとisDoneはfalseのまま)

        while(!result.isDone)                               // リザルトシーンの読み込みがまだの時かつ、フェード処理が終わってない時
        {
            if (FadeImage.rectTransform.anchoredPosition.y > 0)       // フェード処理が終わってない場合
            {
                FadeImage.rectTransform.anchoredPosition = 
                    new Vector2(FadeImage.rectTransform.anchoredPosition.x, FadeImage.rectTransform.anchoredPosition.y - 100);     // フェード画像のy座標を50下げる
            }
            else                                            // フェード処理が終わったら
            {
                result.allowSceneActivation = true;         // シーン遷移を許可
            }

            transform.Rotate(new Vector3(0, 0, rotatePower));   // 海星を回転

            transform.Translate(ForceX * bombPower, ForceY * bombPower, 0, Space.World);         // 爆発の威力に応じて移動

            yield return null;
        }
    }

    private IEnumerator LoadOver()
    {
        AsyncOperation over = GameDirector.Instance.LoadGameOrver();    // ゲームオーバーシーンを非同期で読み込む
        over.allowSceneActivation = false;                              // フェード処理が終わるまではシーン遷移を許可しない(注意点としてallowSceneActivationがfalseのままだとisDoneはfalseのまま)

        while(!over.isDone)                                 // リザルトシーンの読み込みがまだの時かつ、フェード処理が終わってない時
        {
            if(FadeImage.rectTransform.anchoredPosition.y < 0)
            {
                FadeImage.rectTransform.anchoredPosition = 
                    new Vector2(FadeImage.rectTransform.anchoredPosition.x, FadeImage.rectTransform.anchoredPosition.y + 50);     // フェード画像のy座標を50上げる
            }
            else                                        // フェード処理が終わったら
            {
                over.allowSceneActivation = true;       // シーン遷移を許可
            }

            transform.Rotate(new Vector3(0, 0, rotatePower));   // 海星を回転

            transform.Translate(ForceX * bombPower, ForceY * bombPower, 0, Space.World);         // 爆発の威力に応じて移動

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // 最後の腕を消費し、かつ何かのオブジェクトにあたった場合
        if(GameDirector.Instance.GetArmNumber() <= 1)
        {
            Presstime = 0;      // ゲームオーバーまでの時間を延長
        }

        if (col.collider.tag == "Wall")
        {
            if (!GameDirector.Instance.GetPauseFlg)         // 最初のアニメーションでエフェクトを発生させないようにする
            {

                Vector3 hitPos;
                foreach (ContactPoint2D point in col.contacts)
                {
                    hitPos = point.point;                                                   // 衝突した座標を取得
                    var effect = Instantiate(ParticleList[(int)PARTICLE.WALLTOUTCH]);       // エフェクト生成
                    effect.transform.position = hitPos;                                     // エフェクトを衝突した座標に移動
                    VariousFixer vf = effect.GetComponent<VariousFixer>();                  // スクリプト取得
                    if (transform.position.x < 0)       // 画面の左側で壁にあたった場合
                    {
                        vf.RotationY(90f);              // エフェクトを右向きに
                    }
                    else
                    {
                        vf.RotationY(270f);             // エフェクトを左向きに
                    }
                }

                if (transform.position.x < 0)        // 画面の左側で岩にあたった場合
                {
                    rotatePower = 7.0f;             // 時計回りに回転
                                                    // 右上に力を加える
                    ForceX = 0.1f;
                    ForceY = 0.1f;
                }
                else
                {
                    rotatePower = -7.0f;            // 反時計回りに回転
                                                    // 左上に力を加える
                    ForceX = -0.1f;
                    ForceY = 0.1f;
                }
            }
        }
        else if (col.collider.tag == "RightWall")
        {
            Vector3 hitPos;
            foreach (ContactPoint2D point in col.contacts)
            {
                hitPos = point.point;                                                   // 衝突した座標を取得
                var effect = Instantiate(ParticleList[(int)PARTICLE.WALLTOUTCH]);       // エフェクト生成
                effect.transform.position = hitPos;                                     // エフェクトを衝突した座標に移動
                VariousFixer vf = effect.GetComponent<VariousFixer>();                  // スクリプト取得
                vf.RotationY(GetAngle(transform.position, hitPos));                      // スクリプト内の関数で角度を修正
            }

            rotatePower = -7.0f;            // 反時計回りに回転

            // 左上に力を加える
            ForceX = -0.1f;
            ForceY = 0.1f;
        }
        else if (col.collider.tag == "LeftWall")
        {
            Vector3 hitPos;
            foreach (ContactPoint2D point in col.contacts)
            {
                hitPos = point.point;                                                   // 衝突した座標を取得
                var effect = Instantiate(ParticleList[(int)PARTICLE.WALLTOUTCH]);       // エフェクト生成
                effect.transform.position = hitPos;                                     // エフェクトを衝突した座標に移動
                VariousFixer vf = effect.GetComponent<VariousFixer>();                  // スクリプト取得
                vf.RotationY(GetAngle(transform.position, hitPos));                      // スクリプト内の関数で角度を修正
            }

            rotatePower = 7.0f;            // 時計回りに回転

            // 右上に力を加える
            ForceX = 0.1f;
            ForceY = 0.1f;
        }

        // クラゲの頭の部分にあたった時
        if(col.collider.tag == "Top")
        {
            // クラゲ本体の座標と頭の位置の差を取得
            Vector2 distance = col.gameObject.GetComponent<JellyFish_Top>().GetDistance();
            
            // クラゲが向いている方向に力を加える
            ForceX = distance.x * 0.8f;
            ForceY = distance.y * 0.8f;

            var effect = Instantiate(ParticleList[(int)PARTICLE.BOMB], gameObject.transform); // 泡のパーティクルを生成
            effect.transform.localScale /= 0.4f;        // 大きさを修正
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // クラゲの足に入った場合
        if(col.tag == "Under")
        {
            ForceX *= 0.5f;
            ForceY *= 0.5f;
            
        }

        // 海流に入った時
        if (col.tag == "OceanUp")
        {
            if(OceanFlag)               // フラグがtrueなら
            {
                ForceY += 0.015f;        // 海星を上方向に加速させる
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y + 0.03f);     // 重力加速度を減衰
                if (rb.velocity.y >= 0) // 重力加速度が0以上になったら
                {
                    OceanFlag = true;       // フラグをtrueにする
                    ForceY += 0.007f;
                }
            }
        }
        else if(col.tag == "OceanDown")
        {
            if (!OceanFlag)                 // フラグがfalseなら
            {
                ForceY -= 0.005f;            // 海星を下方向に加速させる
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y - 0.02f);    // 重力加速度を加速

                if (rb.velocity.y <= 0.1f)
                    OceanFlag = false;
            }
        }
        else if(col.tag == "OceanLeft")
        {
            ForceX -= 0.005f;
        }
        else if(col.tag == "OceanRight")
        {
            ForceX += 0.005f;
        }
    }

    private float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float degree = rad * Mathf.Rad2Deg;

        if (degree < 0)
        {
            degree += 360;
        }

        return degree;
    }

    // 一時停止の際にほかのシーンに遷移する場合に重力を無効化する
    public void RemoveGravity()
    {
        this.GetComponent<ConstantForce2D>().enabled = false;
        if(ArrowObject.activeSelf)
        {
            ArrowObject.SetActive(false);
        }
        rb.velocity = Vector2.zero;
    }
}
