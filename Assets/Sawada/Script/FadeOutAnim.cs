using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAnim : MonoBehaviour
{
    private Animation anim;

    [SerializeField] private GameObject PauseUI;  // UIのポーズオブジェクト
    [SerializeField] private GameObject StartStage;  // ステージネームを表示するオブジェクト

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();

        // プレイヤー側で動かないように
        GameDirector.Instance.SetPauseFlg = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 自身のアニメーションが終了したら自分自身を削除
        if (!anim.IsPlaying("Stage_FadeOut_Up"))
        {
            Destroy(this.gameObject);
        }
	}

    // 削除される１フレーム前に呼び出される
    private void OnDestroy()
    {
        StartStage.SetActive(true);
        PauseUI.SetActive(true);
    }
}
