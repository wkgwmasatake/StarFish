using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LineLighting : MonoBehaviour {
    [SerializeField] private int CLEAR_STAGE;//発行させる条件となるステージ
    [SerializeField] private int CLEAR_NUM;//エリア内のステージ総数
    private int num;

    private Color white =  new Color(1, 1, 1, 1);
    private Color yellow = new Color(1, 1, 0, 1);
    private Color plain = new Color(0.86f, 0.86f, 0.86f, 1);

    // Use this for initialization
    void Start()
    {
        switch (CLEAR_STAGE)
        {
            case 1:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 2:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 3:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 4:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 5:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 6:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 7:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 8:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 9:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 10:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 11:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 12:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 13:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 14:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
            case 15:
                num = SelectDirector.Instance.Get_cntStageCleared();
                break;
        }

        //エリア内の全ステージをクリア済か否かで処理を切り替える
        if(num >= CLEAR_NUM)
        {
            Light_Emission(yellow);
        }
        else if(num > CLEAR_STAGE)
        {
            Light_Emission(white);
        }
        else if(num <= CLEAR_STAGE)//ゲーム開始時はplainで
        {
            Light_Emission(plain);
        }
    }
    
    private void Light_Emission(Color color)//変色処理
    {
        GetComponent<Image>().color = color;
    }
}
