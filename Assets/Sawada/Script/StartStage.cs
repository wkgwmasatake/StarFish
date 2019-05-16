using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStage : MonoBehaviour
{
    private string stageName;
    private Animation anim;
    
	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = StageName_Configuration();
        anim = GetComponent<Animation>();
	}

    private void Update()
    {
        // 自分自身のアニメーションが終わったら
        if(!anim.IsPlaying(anim.name))
        {
            // プレイヤー側が動けるように
            GameDirector.Instance.SetPauseFlg = false;
        }
    }

    // 今のステージシーンの名前を直接取得
    string StageName_Configuration()
    {
        stageName = GameDirector.Instance.GetSceneName;
        return stageName;
    }
}
