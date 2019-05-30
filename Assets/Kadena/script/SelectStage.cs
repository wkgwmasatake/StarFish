using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour {

    /// <summary>
    /// 定数
    /// </summary>
    private const byte STAGE_MAX = 3;

    [SerializeField] private GameObject Director;

    private AudioSource SE_Taped;
    private AudioSource SE_Failed;
    private Color yellow = new Color(0.86f, 0.86f, 0.86f, 1);
    private Color white = new Color(1, 1, 1, 1);
    private Color dim_yellow = new Color(0.4f, 0.4f, 0.4f, 1);
    private float alpha;//gameobjectの色を取得するfloat型変数

    Button stage_1;
    Button stage_2;
    Button stage_3;
    Button coButton;

    [SerializeField] private Sprite img_unlock;
    [SerializeField] private Sprite img_lock;
    [SerializeField] private Sprite img_clear;

    [SerializeField] private GameObject Water_Fade;     // 画面切り替え時のフェード
    [SerializeField] private GameObject top_Starry;
    [SerializeField] private GameObject bottom_Starry;

    // 各ボタン
    [SerializeField] private GameObject[] AreaButton;
    [SerializeField] private GameObject[] StageButton;
    [SerializeField] private GameObject BackButton;

    private int now_status;

    public string NAME;　//inspector上からステージ名を入力出来るようにする

    private string Namestage;
    private enum StateNum// ステージのクリア状況
    {
        Unlocked = 0, // 選択可能かつ未クリア
        Locked = 1,// 選択不可能
        Cleared = 2,// クリア済み 
    }

    // Use this for initialization
    void Start()
    {
        init();
        SE_Taped = GetComponent<AudioSource>();
        alpha = 1;
    }

    private void init()
    {
        //GetObj();//canvas-> Parent_stage-> object取得
        coButton = GetComponent<UnityEngine.UI.Button>();// ボタンのコンポーネントを取得

        //ステージの状態変更ありの場合
        //directorから情報を取得して反映させたい

        string name = gameObject.name;
        switch (name)
        {
            case "Stage1":
                now_status = SelectDirector.Instance.GetStateStage(0, 0);
                break;
            case "Stage2":
                now_status = SelectDirector.Instance.GetStateStage(1, 0);
                break;
            case "Stage3":
                now_status = SelectDirector.Instance.GetStateStage(2, 0);
                break;
            case "Stage4":
                now_status = SelectDirector.Instance.GetStateStage(3, 0);
                break;
            case "Stage5":
                now_status = SelectDirector.Instance.GetStateStage(4, 0);
                break;
            case "Stage6":
                now_status = SelectDirector.Instance.GetStateStage(5, 0);
                break;
            case "Stage7":
                now_status = SelectDirector.Instance.GetStateStage(6, 0);
                break;
            case "Stage8":
                now_status = SelectDirector.Instance.GetStateStage(7, 0);
                break;
            case "Stage9":
                now_status = SelectDirector.Instance.GetStateStage(8, 0);
                break;
            case "Stage10":
                now_status = SelectDirector.Instance.GetStateStage(9, 0);
                break;
            case "Stage11":
                now_status = SelectDirector.Instance.GetStateStage(10, 0);
                break;
            case "Stage12":
                now_status = SelectDirector.Instance.GetStateStage(11, 0);
                break;
            case "Stage13":
                now_status = SelectDirector.Instance.GetStateStage(12, 0);
                break;
            case "Stage14":
                now_status = SelectDirector.Instance.GetStateStage(13, 0);
                break;
            case "Stage15":
                now_status = SelectDirector.Instance.GetStateStage(14, 0);
                break;
        }
        Change_SetActive(now_status);
    }

    private void Change_SetActive(int num)
    {
        var img = GetComponent<Image>();
        switch (num)
        {
            case (int)StateNum.Unlocked://非ロック状態
                foreach (Transform child in transform)
                {
                    if(child.gameObject.name == "imagelocked")
                    {
                        child.gameObject.SetActive(false);
                    }
                    img.sprite = img_unlock;
                }
                coButton.enabled = true;

                break;
            case (int)StateNum.Locked://ロック状態
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "imagelocked")
                    {
                        child.gameObject.SetActive(true);
                    }
                    img.sprite = img_unlock;
                }
                coButton.enabled = false;
                Light_Emission(dim_yellow);

                break;
            case (int)StateNum.Cleared://光輝く外見に変更する
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name == "imagelocked")
                    {
                        child.gameObject.SetActive(false);
                    }
                    img.sprite = img_unlock;
                }

                img.sprite = img_clear;
                coButton.enabled = true;
                Light_Emission(white);
                break;
        }
    }

    public void ButtonStage()//クリックした時
    {
        SE_Taped.PlayOneShot(SE_Taped.clip);//効果音再生
        StartCoroutine("Fade_Courutin");
    }

    private void Light_Emission(Color color)//変色処理
    {
        GetComponent<Image>().color = color;
    }



    /// <summary>
    ///  切り替わり時のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Fade_Courutin()
    {
        // ステージのボタン設定オフ
        for(int i = 0; i < STAGE_MAX; i++)
        {
            StageButton[i].GetComponent<Button>().enabled = false;
        }
        BackButton.GetComponent<Button>().enabled = false;

        GameObject Fade = Instantiate(Water_Fade) as GameObject;
        
        GameObject obj = GameObject.Find("Select_bgm");
        obj.GetComponent<BGM_Select>().Change_Fade_Flg();
        yield return new WaitForSeconds(0.4f);
        bottom_Starry.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        top_Starry.SetActive(false);

        yield return new WaitForSeconds(1.2f);

        string name = "";
        name = NAME;

        SceneManager.LoadScene(name);
    }
}
