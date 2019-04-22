using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageDirector : SingletonMonoBehaviour<StageDirector>
{

    [SerializeField] private int[] stage;//現在選択している配列の位置 0:左端 STAGE_MAX:右端
    [SerializeField] private GameObject stagetext;
    [SerializeField] private GameObject icon_1;
    [SerializeField] private GameObject icon_2;
    [SerializeField] private GameObject icon_3;

    public enum StateNum// ステージの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    private int STAGE_MAX = 2;//ステージの総数を２と仮定
    private int START_NUM = 0;

    private int num_stage;//配列stageの変数受け入れ用
    private Text StageName;

    //0, // 選択可能かつ未選択
    //1,// 選択不可能
    //2,// 選択可能かつ選択中

    private int[] Stage_state;

    //Awake -> Start -> Update
    void Awake()//オブジェクト本体の起動時処理用　他オブジェクトの参照厳禁
    {
        Init_Stage_Select();//
    }

    void Start()//他オブジェクトを参照する場合はこちらで行う
    {
        StageName = GameObject.Find("Stage_Text").GetComponent<Text>();
        StageName.text = "ステージ " + (START_NUM + 1);
    }

    private void Init_Stage_Select()//シーン開始時の処理　エリアのクリアフラグ読み取りもここで行うこと
    {
        stage = new int[STAGE_MAX + 1];
        num_stage = START_NUM;
        Stage_state = new int[STAGE_MAX + 1];        
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

    public void SetStateStage(int pos_num, int state_num)//要素pos_numの中にクリア状況state_numをセットする
    {
        Stage_state[pos_num] = state_num;
        SetChangeIcon();
    }

    public int GetStateStage(int pos_num)//要素pos_numの変数をget_numに代入して外部に出力する
    {
        int get_num = Stage_state[pos_num];

        return get_num;
    }

    public void SetChangeIcon()
    {
        icon_1.GetComponent<IconManager>().CheckState();
        icon_2.GetComponent<IconManager>().CheckState();
        icon_3.GetComponent<IconManager>().CheckState();
    }
}