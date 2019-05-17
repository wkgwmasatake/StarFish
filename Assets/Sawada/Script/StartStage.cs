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
        if(!anim.IsPlaying("StageName_UI"))
        {
            Debug.Log("owatta");
            Destroy(this.gameObject);
        }
    }

    // 今のステージシーンの名前を直接取得
    string StageName_Configuration()
    {
        stageName = GameDirector.Instance.GetSceneName;
        return stageName;
    }

    private void OnDestroy()
    {
        GameDirector.Instance.SetPauseFlg = false;
    }
}
