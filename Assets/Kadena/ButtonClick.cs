using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonClick : MonoBehaviour {
    [SerializeField] private float TOUCH_SPEED = 3.5f;
    private int AREA_MAX = 2;//エリアの総数を２と仮定
    [SerializeField] private GameObject Parent_area;
    [SerializeField] private GameObject Parent_icon;
    [SerializeField] private int[] area;//配列内における現在位置 0:左端 AREA_MAX:右端
    private int num_area;//配列areaの変数受け入れ用
    private bool touch_left = false;
    private bool touch_right = false;

    public enum AreaStatus// エリアボタンの状態
    {
        Locked = 0,// 選択不可
        Unlocked = 1, // 選択可能
        Cleared = 2// クリア済み
    }
    //Awake -> Start -> Update
    void Awake()//オブジェクト本体の初期化処理用　他のオブジェクトの参照厳禁
    {
        Init_Area_Select();
    }

    void Start()//他オブジェクトを参照する場合はこちらで行う
    {
              
    }

    void Update()
    {
        if(touch_left == true) {
            Move_Left();
        }
        else if(touch_right == true) {
            Move_Right();
        }
    }

    public void Left_Onclick()
    {
        if(gameObject.tag == "Left_arrow" && touch_left == false && num_area > 0)
        {
            num_area++; 
            touch_left = true;
            StartCoroutine(Change_left());
        }
        Debug.Log(num_area);
    }

    public void Right_Onclick()
    {
        if (gameObject.tag == "Right_arrow" && touch_right == false && num_area < AREA_MAX)
        {
            num_area--;
            touch_right = true;
            StartCoroutine(Change_right());
        }
        Debug.Log(num_area);
    }
    private void Init_Area_Select()//シーン開始時の処理　エリアのクリアフラグ読み取りもここで行うこと
    {
        area = new int[AREA_MAX];
        num_area = 1;
    }

    private void Move_Left()
    {
        Parent_area.transform.position += new Vector3(TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    private void Move_Right()
    {
        Parent_area.transform.position += new Vector3(-TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    IEnumerator Change_left()
    {
        yield return new WaitForSeconds(1.0f);
        touch_left = false;
    }

    IEnumerator Change_right()
    {
        yield return new WaitForSeconds(1.0f);
        touch_right = false;
    }
    
}
