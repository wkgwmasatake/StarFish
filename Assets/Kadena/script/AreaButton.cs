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

    public enum StateNum// エリアの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }
    
    void Awake()
    {
        GetObj();//canvas-> Parent_area-> object取得
      
        Button coButton = GetComponent<UnityEngine.UI.Button>();// ボタンのコンポーネントを取得

        if(flg_firstarea == false)//最初のエリア以外をクリック出来ないようにする
        {       
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);//ロック状態を示す画像を表示   
            }
            coButton.enabled = false;
        }        
    }

    // Use this for initialization
    void Start () {
        SE_Taped = GetComponent<AudioSource>();
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
