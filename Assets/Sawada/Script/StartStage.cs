using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStage : MonoBehaviour
{
    [SerializeField] private string[] StageName;

    private string name;

    private Animation anim;
    
	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = StageName_Configuration(GameDirector.Instance.GetSceneNumber);
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
    string StageName_Configuration(int num)
    {
        return StageName[num - 2];

        //switch (num)
        //{
        //    case 2:
        //        name = StageName[0];
        //        break;
        //    case 3:
        //        name = StageName[1];
        //        break;
        //    case 4:
        //        name = StageName[2];
        //        break;
        //    case 5:
        //        name = StageName[3];
        //        break;
        //    case 6:
        //        name = StageName[4];
        //        break;
        //    case 7:
        //        name = StageName[5];
        //        break;
        //    case 8:
        //        name = StageName[6];
        //        break;
        //    case 9:
        //        name = StageName[7];
        //        break;
        //    case 10:
        //        name = StageName[8];
        //        break;
        //    case 11:
        //        name = StageName[9];
        //        break;
        //    case 12:
        //        name = StageName[10];
        //        break;
        //    case 13:
        //        name = StageName[11];
        //        break;
        //    case 14:
        //        name = StageName[12];
        //        break;
        //    case 15:
        //        name = StageName[13];
        //        break;
        //    case 16:
        //        name = StageName[14];
        //        break;
        //}

        //return name;
    }

    private void OnDestroy()
    {
        GameDirector.Instance.SetPauseFlg = false;
    }
}