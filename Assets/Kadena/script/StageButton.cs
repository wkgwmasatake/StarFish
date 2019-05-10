using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageButton : MonoBehaviour {
    [SerializeField] private GameObject Director;

    private AudioSource SE_Taped;
    private AudioSource SE_Failed;

    Button stage_1;
    Button stage_2;
    Button stage_3;
    Button coButton;

    private int now_status;
    
    public string NAME;//inspector上からステージ名を入力出来るようにする

    private string Namestage;
    private enum StateNum// ステージのクリア状況
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    // Use this for initialization
    void Start()
    {
        init();
        SE_Taped = GetComponent<AudioSource>();
        
    }

    private　void init()
    {
        //GetObj();//canvas-> Parent_stage-> object取得
        coButton = GetComponent<UnityEngine.UI.Button>();// ボタンのコンポーネントを取得

        //ステージの状態変更ありの場合
        //directorから情報を取得して反映させたい

        string name = gameObject.name;
        switch (name)
        {
            case "Stage1":
                now_status = StageDirector.Instance.GetStateStage(0, 0);
                break;
            case "Stage2":
                now_status = StageDirector.Instance.GetStateStage(1, 0);
                break;
            case "Stage3":
                now_status = StageDirector.Instance.GetStateStage(2, 0);
                break;
            case "Stage4":
                now_status = StageDirector.Instance.GetStateStage(3, 0);
                break;        
            case "Stage5":
                now_status = StageDirector.Instance.GetStateStage(4, 0);
                break;
            case "Stage6":
                now_status = StageDirector.Instance.GetStateStage(5, 0);
                break;
            case "Stage7":
                now_status = StageDirector.Instance.GetStateStage(6, 0);
                break;
            case "Stage8":
                now_status = StageDirector.Instance.GetStateStage(7, 0);
                break;
            case "Stage9":
                now_status = StageDirector.Instance.GetStateStage(8, 0);
                break;
            case "Stage10":
                now_status = StageDirector.Instance.GetStateStage(9, 0);
                break;
            case "Stage11":
                now_status = StageDirector.Instance.GetStateStage(10, 0);
                break;
            case "Stage12":
                now_status = StageDirector.Instance.GetStateStage(11, 0);
                break;
            case "Stage13":
                now_status = StageDirector.Instance.GetStateStage(12, 0);
                break;
            case "Stage14":
                now_status = StageDirector.Instance.GetStateStage(13, 0);
                break;
            case "Stage15":
                now_status = StageDirector.Instance.GetStateStage(14, 0);
                break;
        }
        Change_SetActive(now_status);

    }

    private void Change_SetActive(int num)
    {
        if (num == (int)StateNum.Locked)
        {
            foreach (Transform child in transform)//ロック状態
            {
                child.gameObject.SetActive(true);  
            }
            coButton.enabled = false;
        }
        else        {
            foreach (Transform child in transform)//非ロック状態
            {
                child.gameObject.SetActive(false);
            }
            coButton.enabled = true;
        }
    }
    public void ButtonStage()//クリックした時
    {
        SE_Taped.PlayOneShot(SE_Taped.clip);//効果音再生

        string name = "";
        name = NAME;
        SceneManager.LoadScene(name);
    }
}
