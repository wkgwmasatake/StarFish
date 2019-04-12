using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageButton : MonoBehaviour {
    public bool flg_firststage;//一番最初のエリアか否か

    [SerializeField] private GameObject Director;

    private AudioSource SE_Taped;
    private AudioSource SE_Failed;
    private bool first_flg = true;//初回起動であるか否か

    Button stage_1;
    Button stage_2;
    Button stage_3;
    Button coButton;
    public enum StateNum// エリアの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    void Awake()
    {
        GetObj();//canvas-> Parent_stage-> object取得

        coButton = GetComponent<UnityEngine.UI.Button>();// ボタンのコンポーネントを取得

        if (flg_firststage == false)//最初のエリア以外をクリック出来ないようにする
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);//ロック状態を示す画像を表示   
            }
            coButton.enabled = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        SE_Taped = GetComponent<AudioSource>();
    }

    public void ButtonStage()//クリックした時
    {
        SE_Taped.PlayOneShot(SE_Taped.clip);//効果音再生
        SceneManager.LoadScene("Stage_Test");//ゲームメインの読み込み
        //テスト処理
        //if (gameObject.name == "stage_1")
        //{
        //    StageDirector.Instance.SetStateStage(0, (int)StateNum.Unlocked);
        //    StageDirector.Instance.SetStateStage(1, (int)StateNum.Selected);
        //    SetChangeStage(stage_2);
        //}//テスト処理ここまで        
    }

    private void GetObj()
    {
        stage_1 = GameObject.Find("Stage/Parent_stage/stage_1").GetComponent<Button>();
        stage_2 = GameObject.Find("Stage/Parent_stage/stage_2").GetComponent<Button>();
        stage_3 = GameObject.Find("Stage/Parent_stage/stage_3").GetComponent<Button>();
    }

    private void SetChangeStage(Button obj)//サムネイルのロック表示→表示処理
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);//ロック状態を示す画像を表示
            Debug.Log("ok");
        }
        obj.enabled = true;
    }
}
