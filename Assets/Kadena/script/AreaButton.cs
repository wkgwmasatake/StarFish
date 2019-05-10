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

    [SerializeField] Button area_1;
    [SerializeField] Button area_2;
    [SerializeField] Button area_3;
    [SerializeField] Button area_4;
    [SerializeField] Button area_5;
    Button coButton;// ボタンのコンポーネントを取得

    private int now_status;
    public string Namearea;

    private enum StateNum// エリアの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    int num_cleared;

    // Use this for initialization
    void Start () {
        Init();
        SE_Taped = GetComponent<AudioSource>();
	}

    private void Init()
    {
        coButton = GetComponent<UnityEngine.UI.Button>();// ボタンのコンポーネントを取得

        string name = gameObject.name;
        switch (name)
        {
            case "Area_1":
                now_status = AreaDirector.Instance.GetStateArea(0, 0);
                break;
            case "Area_2":
                now_status = AreaDirector.Instance.GetStateArea(1, 0);
                break;
            case "Area_3":
                now_status = AreaDirector.Instance.GetStateArea(2, 0);
                break;
            case "Area_4":
                now_status = AreaDirector.Instance.GetStateArea(3, 0);
                break;
            case "Area_5":
                now_status = AreaDirector.Instance.GetStateArea(4, 0);
                break;
        }
        Change_SetActive(now_status);
        //Debug.Log(now_status);
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
        else
        {
            foreach (Transform child in transform)//非ロック状態
            {
                child.gameObject.SetActive(false);
            }
            coButton.enabled = true;
        }
    }

    public void ButtonArea()//クリックした時
    {
        PlayerPrefs.SetString("Select_Area", gameObject.name);
        SE_Taped.PlayOneShot(SE_Taped.clip);//効果音再生
        SceneManager.LoadScene("Stage_Select");//ステージの読み込み
    }
}
