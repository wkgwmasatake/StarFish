using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageDirector : SingletonMonoBehaviour<StageDirector>
{
    [SerializeField] private int[] stage;//現在選択している配列の位置 0:左端 STAGE_MAX:右端
    [SerializeField] private GameObject icon_1;
    [SerializeField] private GameObject icon_2;
    [SerializeField] private GameObject icon_3;

    [SerializeField] private GameObject Area1;
    [SerializeField] private GameObject Area2;
    [SerializeField] private GameObject Area3;
    [SerializeField] private GameObject Area4;
    [SerializeField] private GameObject Area5;

    [SerializeField] private int num_cleared;//クリア状況の進行受け取り用

    public enum StateNum// ステージの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    private int STAGE_MAX = 15;//ステージ総数

    private int START_NUM;
    private int num_stage;//配列stageの変数受け入れ用
    private Text StageName;
    
    private int[] Stage_state;

    //Awake -> Start -> Update
    void Awake()//オブジェクト本体の起動時処理用　
    {
        Init_Stage_Select();//
        //ゲームディレクターのGetStageClear_Flgを用いてクリア状況をnum_clearedに代入する

        string area_num = PlayerPrefs.GetString("Select_Area");

        switch (area_num)//クリア済のエリアを選択した時
        {
            case "Area_1":
                if(num_cleared > 2) { num_cleared = 2; }
                break;
            case "Area_2":
                if (num_cleared > 5) { num_cleared = 5; }
                break;
            case "Area_3":
                if (num_cleared > 8) { num_cleared = 8; }
                break;
            case "Area_4":
                if (num_cleared > 11) { num_cleared = 11; }
                break;
        }

        switch (num_cleared)//クリア状況によるエリア表示の切り替え
        {
            case 0:
                setAll_Area(true, false, false, false, false);
                setAll_Stage(0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 0;
                break;
            case 1:
                setAll_Area(true, false, false, false, false);
                setAll_Stage(0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 0;
                break;
            case 2:
                setAll_Area(true, false, false, false, false);
                setAll_Stage(0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 0;
                break;
            case 3:
                setAll_Area(false, true, false, false, false);
                setAll_Stage(0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 3;
                break;
            case 4:
                setAll_Area(false, true, false, false, false);
                setAll_Stage(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 3;
                break;
            case 5:
                setAll_Area(false, true, false, false, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 3;
                break;
            case 6:
                setAll_Area(false, false, true, false, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 6;
                break;
            case 7:
                setAll_Area(false, false, true, false, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 6;
                break;
            case 8:
                setAll_Area(false, false, true, false, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1);
                START_NUM = 6;
                break;
            case 9:
                setAll_Area(false, false, false, true, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1);
                START_NUM = 9;
                break;
            case 10:
                setAll_Area(false, false, false, true, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1);
                START_NUM = 9;
                break;
            case 11:
                setAll_Area(false, false, false, true, false);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1);
                START_NUM = 9;
                break;
            case 12:
                setAll_Area(false, false, false, false, true);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1);
                START_NUM = 12;
                break;
            case 13:
                setAll_Area(false, false, false, false, true);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
                START_NUM = 12;
                break;
            case 14:
                setAll_Area(false, false, false, false, true);
                setAll_Stage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                START_NUM = 12;
                break;
        }
    }

    void Start()//他オブジェクトを参照する場合はこちらで行う
    {
        StageName = GameObject.Find("Stage_Text").GetComponent<Text>();
        StageName.text = "ステージ " + (START_NUM + 1);
        num_stage = START_NUM;
    }

    private void Init_Stage_Select()//シーン開始時の処理　エリアのクリアフラグ読み取りもここで行うこと
    {
        stage = new int[STAGE_MAX + 1];       
        Stage_state = new int[STAGE_MAX + 1];
    }

    //1～5番目のエリアのクリア状況初期処理
    private void setAll_Area( bool first, bool second, bool third, bool fourth, bool fifth)
    {
        Area1.SetActive(first);
        Area2.SetActive(second);
        Area3.SetActive(third);
        Area4.SetActive(fourth);
        Area5.SetActive(fifth);
    }
    //1～15番目のステージのクリア状況初期処理
    private void setAll_Stage(int num_1, int num_2, int num_3, int num_4, int num_5, int num_6, int num_7,
    int num_8, int num_9, int num_10, int num_11, int num_12, int num_13, int num_14, int num_15)
    {
        Stage_state[0] = num_1; Stage_state[1] = num_2; Stage_state[2] = num_3;
        Stage_state[3] = num_4; Stage_state[4] = num_5; Stage_state[5] = num_6;
        Stage_state[6] = num_7; Stage_state[7] = num_8; Stage_state[8] = num_9;
        Stage_state[9] = num_10; Stage_state[10] = num_11; Stage_state[11] = num_12;
        Stage_state[12] = num_13; Stage_state[13] = num_14; Stage_state[14] = num_15;
    }

    public void SetNumStage(int num)//外部からエリア配列の要素数を変更する処理
    {
        num_stage += num;//エリア番号の変更
        int add = num_stage + 1;//要素数→エリア番号に変換するための加算処理
        StageName.text = "ステージ " + add;
    }

    public int GetNumStage()//エリア配列の要素数を外部から取得する
    {
        return num_stage;
    }
    
    //要素pos_numの中にクリア状況state_numをセットする countで参照する要素位置を変更する 
    //count -1:１つ前の要素数  0:変更なし  1:１つ後の要素数
    //public void SetStateStage(int pos_num, int count, int state_num)
    //{
    //    Stage_state[pos_num + count] = state_num;
    //    SetChangeIcon();        
    //}

    public void SetState_Unlocked(int pos_num, int count)//unlock状態にする
    {
        Stage_state[pos_num + count] = (int)StateNum.Unlocked;
        SetChangeIcon();
    }

    public void SetState_Selected(int pos_num, int count)//selected状態にする
    {
        Stage_state[pos_num + count] = (int)StateNum.Selected;
        SetChangeIcon();
    }

    public int GetStateStage(int pos_num, int count)//要素pos_numの変数をget_numに代入して外部に出力する
    {
        return Stage_state[pos_num + count];       
    }

    public int GetClearNUM()//クリア状況の取得
    {
        return num_cleared;
    }

    public void SetChangeIcon()
    {
        icon_1.GetComponent<IconManager>().CheckState();
        icon_2.GetComponent<IconManager>().CheckState();
        icon_3.GetComponent<IconManager>().CheckState();
    }
}