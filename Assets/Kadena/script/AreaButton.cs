using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AreaButton : MonoBehaviour {
    public bool flg_firstarea;//一番最初のエリアか否か

    [SerializeField] private GameObject Director;

    private AudioSource SE_Taped;
    private AudioSource SE_Failed;
    private bool first_flg = true;//初回起動であるか否か

    Button area_1;
    Button area_2;
    Button area_3;
    Button coButton;// ボタンのコンポーネントを取得

    public const string preKeyAreaStatus = "AreaStatus_";
    public string Namearea;

    private enum StateNum// エリアの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }
    
    void Awake()
    {
        Init();
    }

    // Use this for initialization
    void Start () {
        SE_Taped = GetComponent<AudioSource>();
	}

    private void Init()
    {
        GetObj();//canvas-> Parent_area-> object取得
        string KeyAreaStatus = preKeyAreaStatus + Namearea;

        if (flg_firstarea == true)//1ステージ目以外をlockedに設定する
        {
            PlayerPrefs.SetInt(KeyAreaStatus, (int)StateNum.Selected);
        }
        else
        {
            PlayerPrefs.SetInt(KeyAreaStatus, (int)StateNum.Locked);
        }

        int now_status = PlayerPrefs.GetInt(KeyAreaStatus);
        coButton = GetComponent<UnityEngine.UI.Button>();// ボタンのコンポーネントを取得

        if (now_status == (int)StateNum.Locked)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);//ロック状態を示す画像を表示   
            }
            coButton.enabled = false;
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);//ロック状態を示す画像を表示
            }
            coButton.enabled = true;
        }
        if (gameObject.name == "Area_1")
        {
            int getnum = PlayerPrefs.GetInt(KeyAreaStatus);
            AreaDirector.Instance.SetStateArea(0, getnum);
        }
        else if (gameObject.name == "Area_2")
        {
            int getnum = PlayerPrefs.GetInt(KeyAreaStatus);
            AreaDirector.Instance.SetStateArea(1, getnum);
        }
        else if (gameObject.name == "Area_3")
        {
            int getnum = PlayerPrefs.GetInt(KeyAreaStatus);
            AreaDirector.Instance.SetStateArea(2, getnum);
        }
    }

    public void ButtonArea()//クリックした時
    {
        SE_Taped.PlayOneShot(SE_Taped.clip);//効果音再生
        SceneManager.LoadScene("Stage_Select");//ステージの読み込み
    }

    private void GetObj()
    {
        area_1 = GameObject.Find("Area/Parent_area/area_1").GetComponent<Button>();
        area_2 = GameObject.Find("Area/Parent_area/area_2").GetComponent<Button>();
        area_3 = GameObject.Find("Area/Parent_area/area_3").GetComponent<Button>();
    }
}
