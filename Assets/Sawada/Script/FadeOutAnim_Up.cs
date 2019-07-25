using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAnim_Up : MonoBehaviour
{
    private Animation anim;

    private GameObject PauseUI;  // UIのポーズオブジェクト
    private GameObject StartStage;  // ステージネームを表示するオブジェクト

    [SerializeField] private float UpAlpha;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();

        PauseUI = GameObject.Find("Canvas_beta").transform.GetChild(1).transform.GetChild(1).gameObject;
        StartStage = GameObject.Find("Canvas_beta").transform.GetChild(3).gameObject;

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

    private void OnDestroy()
    {
        StartStage.SetActive(true);
        PauseUI.SetActive(true);
    }

}