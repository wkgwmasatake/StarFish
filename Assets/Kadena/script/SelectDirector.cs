using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDirector : SingletonMonoBehaviour<SelectDirector> {
    [SerializeField] private string[] AREA_NAME;

    [SerializeField] private int[] stage;//現在選択している配列の位置 0:左端 STAGE_MAX:右端
    [SerializeField] private int[] area;//現在選択している配列の位置 0:左端 AREA_MAX:右端

    [SerializeField] private GameObject icon_1;
    [SerializeField] private GameObject icon_2;
    [SerializeField] private GameObject icon_3;
    [SerializeField] private GameObject icon_4;
    [SerializeField] private GameObject icon_5;

    [SerializeField] private GameObject Area1;
    [SerializeField] private GameObject Area2;
    [SerializeField] private GameObject Area3;
    [SerializeField] private GameObject Area4;
    [SerializeField] private GameObject Area5;

    [SerializeField] private GameObject Left_arrow;
    [SerializeField] private GameObject Right_arrow;

    [SerializeField] private GameObject Option;
    [SerializeField] private GameObject Return;

    [SerializeField] private GameObject under_zoom;

    private int cnt_stage_cleared;//ステージクリア状況の進行受け取り用
    private int cnt_area_cleared;//エリアクリア状況の進行受け取り用

    public enum StateNum// ステージの状態
    {
        Unlocked, // 選択可能かつ未選択
        Locked,// 選択不可能
        Selected,// 選択可能かつ選択中
    }

    private int STAGE_MAX = 15;//ステージ総数
    private int AREA_MAX = 5;//エリアの総数   

    private int START_NUM;
    private int num_stage;//配列stageの変数受け入れ用
    private int num_area;//配列areaの変数受け入れ用

    private int[] Area_state;
    private int[] Stage_state;

    private Text NameText;
    private bool State_zoom = false;

    private Animation BlackFade_Anim;    // BlackFadeのアニメーション
    private bool OneShotFlg = false;     // ループ用フラグ（一回のみ入ってほしいとき）


    //Awake -> Start -> Update
    void Awake()//オブジェクト本体の起動時処理用　
    {
        // 各機能を止める
        Fade_Start();

        //gamedirectorのGetStageClear_Flgを用いてクリア状況をcnt_stage_clearedに代入する
        Init_Stage_Select();//

        string area_num = PlayerPrefs.GetString("Select_Area");

        switch (cnt_stage_cleared)//クリア状況による表示の切り替え
        {
            case 1:
                setAll_Area(0, 1, 1, 1, 1);
                setAll_Stage(0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 0;
                break;
            case 2:
                setAll_Area(0, 1, 1, 1, 1);
                setAll_Stage(2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 0;
                break;
            case 3:
                setAll_Area(0, 1, 1, 1, 1);
                setAll_Stage(2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 0;
                break;
            case 4:
                setAll_Area(0, 0, 1, 1, 1);
                setAll_Stage(2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 3;
                break;
            case 5:
                setAll_Area(0, 0, 1, 1, 1);
                setAll_Stage(2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 3;
                break;
            case 6:
                setAll_Area(0, 0, 1, 1, 1);
                setAll_Stage(2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 3;
                break;
            case 7:
                setAll_Area(0, 0, 0, 1, 1);
                setAll_Stage(2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 6;
                break;
            case 8:
                setAll_Area(0, 0, 0, 1, 1);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1);
                START_NUM = 6;
                break;
            case 9:
                setAll_Area(0, 0, 0, 1, 1);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1);
                START_NUM = 6;
                break;
            case 10:
                setAll_Area(0, 0, 0, 0, 1);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1);
                START_NUM = 9;
                break;
            case 11:
                setAll_Area(0, 0, 0, 0, 1);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1);
                START_NUM = 9;
                break;
            case 12:
                setAll_Area(0, 0, 0, 0, 1);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1);
                START_NUM = 9;
                break;
            case 13:
                setAll_Area(0, 0, 0, 0, 0);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1);
                START_NUM = 12;
                break;
            case 14:
                setAll_Area(0, 0, 0, 0, 0);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1);
                START_NUM = 12;
                break;
            case 15:
                setAll_Area(0, 0, 0, 0, 0);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0);
                START_NUM = 12;
                break;
            case 16:
                setAll_Area(0, 0, 0, 0, 0);
                setAll_Stage(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2);
                START_NUM = 12;
                break;
        }
        NameText = GameObject.Find("Name_Text").GetComponent<Text>();
        SetName(START_NUM + 1);
        num_stage = START_NUM;

    }

    void Start()//他オブジェクトを参照する場合はこちらで行う
    {

        // Black_Fadeを探して、FadePointの子に設定
        GameObject.Find("Blackfade _Up").transform.parent = GameObject.Find("FadePoint").transform;

        // BlackFadeのアニメーションコンポーネントを取得
        BlackFade_Anim = GameObject.Find("Blackfade _Up").GetComponent<Animation>();

    }


    void Update()
    {
        
        // BlackFadeのアニメーションが終わったら
        if(!BlackFade_Anim.IsPlaying("BlackFade_Up") && !OneShotFlg)
        {
            Debug.Log("アニメーション終了");

            //　各機能を使えるように
            Fade_End();

            OneShotFlg = true;
        }

    }


    private void Init_Stage_Select()//シーン開始時の処理　エリアのクリアフラグ読み取りもここで行うこと
    {
        int clear_area= GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetAreaClear_Flg);
        int clear_stage = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetStageClear_Flg);

        cnt_area_cleared = clear_area;
        cnt_stage_cleared = clear_stage;

        stage = new int[STAGE_MAX + 1];
        Stage_state = new int[STAGE_MAX + 1];

        area = new int[AREA_MAX + 1];
        Area_state = new int[AREA_MAX + 1];
    }

    private void setAll_Area(int num_1, int num_2, int num_3, int num_4, int num_5)
    {
        Area_state[0] = num_1;
        Area_state[1] = num_2;
        Area_state[2] = num_3;
        Area_state[3] = num_4;
        Area_state[4] = num_5;
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
    public void SetName(int area_num)
    {
        switch (area_num)
        {
            case 1:
                NameText.text = AREA_NAME[0];
                break;
            case 2:
                NameText.text = AREA_NAME[1];
                break;
            case 3:
                NameText.text = AREA_NAME[2];
                break;
            case 4:
                NameText.text = AREA_NAME[3];
                break;
            case 5:
                NameText.text = AREA_NAME[4];
                break;
        }
    }

    //ステージ状況受け取り
    public void SetNumStage(int num)//外部からエリア配列の要素数を変更する処理
    {
        num_stage += num;//エリア番号の変更
        int add = num_stage + 1;//要素数→エリア番号に変換するための加算処理
        NameText.text = "ステージ " + add;
    }

    public int GetNumStage()//エリア配列の要素数を外部から取得する
    {
        return num_stage;
    }

    public void SetState_Unlocked_Stage(int pos_num, int count)//unlock状態にする
    {
        Stage_state[pos_num + count] = (int)StateNum.Unlocked;
        SetChangeIcon();
    }

    public void SetState_Selected_Stage(int pos_num, int count)//selected状態にする
    {
        Stage_state[pos_num + count] = (int)StateNum.Selected;
        SetChangeIcon();
    }

    public int GetStateStage(int pos_num, int count)//要素pos_numの変数をget_numに代入して外部に出力する
    {
        return Stage_state[pos_num + count];
    }

    public int Get_cntStageCleared()//クリア状況の取得
    {
        return cnt_stage_cleared;
    }
    //ステージ状況受け取り

    //エリア状況受け取り
    public void SetNumArea(int num)//外部からエリア配列の要素数を変更する処理
    {
        num_area += num;//エリア番号の変更
        int add = num_area + 1;//要素数→エリア番号に変換するための加算処理
        SetName(add);
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

    public void SetState_Unlocked_Area(int pos_num, int count)//unlock状態にする
    {
        Area_state[pos_num + count] = (int)StateNum.Unlocked;
        SetChangeIcon();
    }

    public void SetState_Selected_Area(int pos_num, int count)//selected状態にする
    {
        Area_state[pos_num + count] = (int)StateNum.Selected;
        SetChangeIcon();
    }

    public int GetStateArea(int pos_num, int count)//要素pos_numの変数をget_numに代入して外部に出力する
    {
        return Area_state[pos_num + count];
    }

    public int GetareaCleared()//クリア状況の取得
    {
        return cnt_area_cleared - 1;//GameDirectorの初期値が1ならnum_cleaned - 1にするべきか
    }
    
    //エリア状況受け取り
    public void SetChangeIcon()
    {
        icon_1.GetComponent<SelectIcon>().CheckState();
        icon_2.GetComponent<SelectIcon>().CheckState();
        icon_3.GetComponent<SelectIcon>().CheckState();
        icon_4.GetComponent<SelectIcon>().CheckState();
        icon_5.GetComponent<SelectIcon>().CheckState();
    }

    //ズーム状態受け取り
    public bool Get_Statezoom()
    {
        return State_zoom;
    }

    public void Set_Statezoom()
    {
        State_zoom = !State_zoom;
        if(State_zoom == true)
        {
            under_zoom.SetActive(true);
        }
        else
        {
            under_zoom.SetActive(false);
        }
    }


    #region フェード関連

    ///<summry>
    ///  フェード中各機能を触れなくする
    ///</summry>
    void Fade_Start()
    {
        Left_arrow.GetComponent<Button>().enabled = false;       // 左矢印ボタン
        Right_arrow.GetComponent<Button>().enabled = false;      // 右矢印ボタン
        Option.GetComponent<Button>().enabled = false;           // オプションボタン
        Return.GetComponent<Button>().enabled = false;           // 戻るボタン

        Area1.GetComponent<SelectArea>().enabled = false;
        Area1.GetComponent<Button>().enabled = false;
        Area2.GetComponent<SelectArea>().enabled = false;
        Area2.GetComponent<Button>().enabled = false;
        Area3.GetComponent<SelectArea>().enabled = false;
        Area3.GetComponent<Button>().enabled = false;
        Area4.GetComponent<SelectArea>().enabled = false;
        Area4.GetComponent<Button>().enabled = false;
        Area5.GetComponent<SelectArea>().enabled = false;
        Area5.GetComponent<Button>().enabled = false;
    }

    ///<summry>
    ///  フェード終了時、各機能を使えるように
    ///</summry>
    void Fade_End()
    {
        Left_arrow.GetComponent<Button>().enabled = true;       // 左矢印ボタン
        Right_arrow.GetComponent<Button>().enabled = true;      // 右矢印ボタン
        Option.GetComponent<Button>().enabled = true;           // オプションボタン
        Return.GetComponent<Button>().enabled = true;           // 戻るボタン

        Area1.GetComponent<SelectArea>().enabled = true;
        Area1.GetComponent<Button>().enabled = true;
        Area2.GetComponent<SelectArea>().enabled = true;
        Area2.GetComponent<Button>().enabled = true;
        Area3.GetComponent<SelectArea>().enabled = true;
        Area3.GetComponent<Button>().enabled = true;
        Area4.GetComponent<SelectArea>().enabled = true;
        Area4.GetComponent<Button>().enabled = true;
        Area5.GetComponent<SelectArea>().enabled = true;
        Area5.GetComponent<Button>().enabled = true;
    }

    #endregion

}
