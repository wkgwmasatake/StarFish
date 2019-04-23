using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AreaDirector : SingletonMonoBehaviour<AreaDirector> {

    [SerializeField] private int[] area;//現在選択している配列の位置 0:左端 AREA_MAX:右端
    [SerializeField] private GameObject areatext;
    [SerializeField] private GameObject icon_1;
    [SerializeField] private GameObject icon_2;
    [SerializeField] private GameObject icon_3;

    public enum StateNum// エリアの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    private int AREA_MAX = 2;//エリアの総数を3と仮定   
    private int START_NUM = 0;
    private int num_area;//配列areaの変数受け入れ用
    private Text AreaName;

    //0, // 選択可能かつ未選択
    //1,// 選択不可能
    //2,// 選択可能かつ選択中
    
    private int[] Area_state;

    //Awake -> Start -> Update
    void Awake()//オブジェクト本体の起動時処理用　他オブジェクトの参照厳禁
    {
        Init_Area_Select();//
    }

    void Start()//他オブジェクトを参照する場合はこちらで行う
    {
        AreaName = GameObject.Find("Area_Text").GetComponent<Text>();
        Debug.Log(AreaName);
        AreaName.text = "エリア " + (START_NUM + 1);
    }

    private void Init_Area_Select()//シーン開始時の処理　エリアのクリアフラグ読み取りもここで行うこと
    {
        area = new int[AREA_MAX + 1];
        num_area = START_NUM;
        Area_state = new int[AREA_MAX + 1];           
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
    }

    public int GetStateArea(int pos_num)//要素pos_numの変数をget_numに代入して外部に出力する
    {
        int get_num = Area_state[pos_num];       
        return get_num;
    }

    public void SetChangeIcon()
    {        
        icon_1.GetComponent<IconManager>().CheckState();
        icon_2.GetComponent<IconManager>().CheckState();
        icon_3.GetComponent<IconManager>().CheckState();
    }
}
