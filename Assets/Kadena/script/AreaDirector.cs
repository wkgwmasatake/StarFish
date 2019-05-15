using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AreaDirector : SingletonMonoBehaviour<AreaDirector> {

    [SerializeField] private int[] area;//現在選択している配列の位置 0:左端 AREA_MAX:右端
    [SerializeField] private GameObject icon_1;
    [SerializeField] private GameObject icon_2;
    [SerializeField] private GameObject icon_3;
    [SerializeField] private GameObject icon_4;
    [SerializeField] private GameObject icon_5;
    [SerializeField] private GameObject Left_arrow;
    [SerializeField] private GameObject Right_arrow;

    [SerializeField] private int temporary_num;//進行状況の仮値
    private int num_cleared;//クリア状況の進行受け取り用

    private enum StateNum// エリアの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    private GameObject areatext;

    private int AREA_MAX = 5;//エリアの総数   
    private int START_NUM;
    private int num_area;//配列areaの変数受け入れ用
    private Text AreaName;
    
    [SerializeField] private int[] Area_state;

    //Awake -> Start -> Update
    void Awake()//オブジェクト本体の起動時処理用　他オブジェクトの参照厳禁
    {
        //num_cleared = temporary_num;
        Init_Area_Select();

        switch (num_cleared)
        {
            case 0:
                setAll_Stage(0, 1, 1, 1, 1);
                break;
            case 1:
                setAll_Stage(0, 0, 1, 1, 1);
                break;
            case 2:
                setAll_Stage(0, 0, 0, 1, 1);
                break;
            case 3:
                setAll_Stage(0, 0, 0, 0, 1);
                break;
            case 4:
                setAll_Stage(0, 0, 0, 0, 0);
                break;
        }
    }

    void Start()//他オブジェクトを参照する場合はこちらで行う
    {
        AreaName = GameObject.Find("Area_Text").GetComponent<Text>();
        AreaName.text = "エリア " + (START_NUM + 1);
        num_area = START_NUM;
    }

    //1～3番目のエリアのクリア状況初期処理
    private void Init_Area_Select()//シーン開始時の処理　エリアのクリアフラグ読み取りもここで行うこと
    {
        //ゲームディレクターのGetAreaClear_Flgを用いてクリア状況をnum_clearedに代入する
        int clear_area = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetAreaClear_Flg);

        //if (clear_area == -1)
        //{
        //    clear_area = 0;
        //}
        //else if (clear_area == 2 || clear_area == 3)
        //{
        //    clear_area = 2;
        //}
        //else if (clear_area >= 4 && clear_area < 8)
        //{
        //    clear_area = 3;
        //}
        //else if (clear_area >= 8)
        //{
        //    clear_area = 4;
        //}

        num_cleared = clear_area;

        Debug.Log("area " + clear_area);
        Debug.Log(num_cleared);

        area = new int[AREA_MAX + 1];
        Area_state = new int[AREA_MAX + 1];
    }

    private void setAll_Stage(int num_1, int num_2, int num_3, int num_4, int num_5)
    {
        Area_state[0] = num_1;
        Area_state[1] = num_2;
        Area_state[2] = num_3;
        Area_state[3] = num_4;
        Area_state[4] = num_5;
    }

    public void SetNumArea(int num)//外部からエリア配列の要素数を変更する処理
    {
        num_area += num;//エリア番号の変更
        int add = num_area + 1;//要素数→エリア番号に変換するための加算処理
        AreaName.text = "エリア " + add;
    }

    public int GetNumArea()//エリア配列の要素数を外部から取得する
    {
        return num_area;
    }

    public void SetStateArea(int pos_num, int state_num)//要素pos_numの中にクリア状況state_numをセットする
    {
        Area_state[pos_num] = state_num;
        SetChangeIcon();
    }

    public void SetState_Unlocked(int pos_num, int count)//unlock状態にする
    {
        Area_state[pos_num + count] = (int)StateNum.Unlocked;
        SetChangeIcon();
    }

    public void SetState_Selected(int pos_num, int count)//selected状態にする
    {
        Area_state[pos_num + count] = (int)StateNum.Selected;
        SetChangeIcon();
    }

    public int GetStateArea(int pos_num, int count)//要素pos_numの変数をget_numに代入して外部に出力する
    {
        Debug.Log(Area_state[pos_num + count]);
        return Area_state[pos_num + count];
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
        icon_4.GetComponent<IconManager>().CheckState();
        icon_5.GetComponent<IconManager>().CheckState();
    }
}
