using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretAreaDirector : MonoBehaviour {

    [SerializeField] private GameObject areaButton;
    [SerializeField] private int buttonOffsetX;
    [SerializeField] private int buttonOffsetY;
    [SerializeField] private GameObject canvasPanel;

    private List<GameObject> areaButtonList = new List<GameObject>();

    public enum LOCKSTATE
    {
        LOCK,
        UNLOCK
    };

    private int areaClear;    

    private void Awake()
    {
        //やりたいことリスト
        //1.エリア解放状況を知りたい
        //2.エリアを選ぶボタンをエリアの数生成したい
        //3.エリア解放状況に応じて、エリアを選ぶボタンの画像を切り替えたい
        //4.エリア解放状況に応じて、エリア簡易アイコンの画像を切り替えたい

        //1
        //areaClear = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetAreaClear_Flg);
        areaClear = 0;

        Debug.Log(areaClear);

        //2GameDirector.Instance.AREA_MAX
        for (int i = 0; i < 5; i++)
        {
            areaButtonList.Add(Instantiate(areaButton) as GameObject);
            areaButtonList[i].transform.SetParent(canvasPanel.transform,false);
            areaButtonList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * buttonOffsetX,buttonOffsetY);
            areaButtonList[i].GetComponent<AreaButtonController>().SetAreaNumber(i);

            if(areaClear < i)
            {
                areaButtonList[i].GetComponent<AreaButtonController>().SetReleaseFlg((byte)LOCKSTATE.LOCK);
            }
            else
            {
                areaButtonList[i].GetComponent<AreaButtonController>().SetReleaseFlg((byte)LOCKSTATE.UNLOCK);
            }
        }

        if (areaClear < 0)
        {
            areaButtonList[0].GetComponent<AreaButtonController>().SetReleaseFlg((byte)LOCKSTATE.UNLOCK);
        }

    }


    // Update is called once per frame
    void Update () {
		
	}
}
