using System.Collections;
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
        GameDirector.Instance.SetArmNumber(_MAX_TAP + 2);       // 腕の本数を初期化(最初のタップと最後のタップは腕を消費しないため7に設定)
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
