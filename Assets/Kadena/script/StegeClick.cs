using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StegeClick : MonoBehaviour
{

    [SerializeField] private float TOUCH_SPEED = 900f;//進行距離
    [SerializeField] private GameObject Parent_icon;
    [SerializeField] private GameObject Director;
    [SerializeField] private float move_time = 0.6f;


    private GameObject now_area;

    public enum StateNum// ステージの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    private bool touch_left = false;
    private bool touch_right = false;
    private AudioSource tap_SE;

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        tap_SE = GetComponent<AudioSource>();

        if (GameObject.Find("Area_1") == true)
        {
            now_area = GameObject.Find("Area_1");
        }
        else if (GameObject.Find("Area_2") == true)
        {
            now_area = GameObject.Find("Area_2");
        }
        else if (GameObject.Find("Area_3") == true)
        {
            now_area = GameObject.Find("Area_3");
        }
        else if (GameObject.Find("Area_4") == true)
        {
            now_area = GameObject.Find("Area_4");
        }
        else
        {
            now_area = GameObject.Find("Area_5");
        }
    }

    void Update()
    {
        if (touch_left == true)
        {
            Move_Left();
        }
        else if (touch_right == true)
        {
            Move_Right();
        }
    }

    public void Left_Onclick()
    {
        int pos = StageDirector.Instance.GetNumStage();
        if(touch_left == false) { 
            if ( pos > 0 && now_area.name == "Area_1"
            || pos > 3 && now_area.name == "Area_2"
            || pos > 6 && now_area.name == "Area_3"
            || pos > 9 && now_area.name == "Area_4"
            || pos > 12 && now_area.name == "Area_5")
            {
                int state = StageDirector.Instance.GetStateStage(pos, -1);
                int now_state = StageDirector.Instance.GetStateStage(pos, 0);

                if (state != (int)StateNum.Locked)
                {
                    switch (state)
                    {
                        case (int)StateNum.Unlocked:
                            StageDirector.Instance.SetState_Selected(pos, -1);
                            if (now_state != (int)StateNum.Locked)
                            {
                                StageDirector.Instance.SetState_Unlocked(pos, 0);
                            }
                            break;
                        case (int)StateNum.Selected:
                            StageDirector.Instance.SetState_Unlocked(pos, -1);
                            if (now_state != (int)StateNum.Locked)
                            {
                                StageDirector.Instance.SetState_Selected(pos, 0);
                            }
                            break;
                    }
                }
                else
                {
                    if (now_state != (int)StateNum.Locked)
                    {
                        StageDirector.Instance.SetState_Unlocked(pos, 0);
                    }
                }
                StageDirector.Instance.SetNumStage(-1);

                touch_left = true;
                StartCoroutine(Change_left());
            }
        }
    }
    public void Right_Onclick()
    {
        int pos = StageDirector.Instance.GetNumStage();
        if (touch_right == false)
        {
            if (pos < 2 && now_area.name == "Area_1"
            || pos < 5 && now_area.name == "Area_2"
            || pos < 8 && now_area.name == "Area_3"
            || pos < 11 && now_area.name == "Area_4"
            || pos < 14 && now_area.name == "Area_5")
            {
                int state = StageDirector.Instance.GetStateStage(pos, 1);
                int now_state = StageDirector.Instance.GetStateStage(pos, 0);
                if (state != (int)StateNum.Locked)
                {
                    switch (state)
                    {
                        case (int)StateNum.Unlocked:
                            StageDirector.Instance.SetState_Selected(pos, 1);
                            if (now_state != (int)StateNum.Locked)
                            {
                                StageDirector.Instance.SetState_Unlocked(pos, 0);
                            }
                            break;
                        case (int)StateNum.Selected:
                            StageDirector.Instance.SetState_Unlocked(pos, 1);
                            if (now_state != (int)StateNum.Locked)
                            {
                                StageDirector.Instance.SetState_Selected(pos, 0);
                            }
                            break;
                    }
                }
                else
                {
                    if (now_state != (int)StateNum.Locked)
                    {
                        StageDirector.Instance.SetState_Unlocked(pos, 0);
                    }
                }
                StageDirector.Instance.SetNumStage(1);
                touch_right = true;
                StartCoroutine(Change_right());
            }
        }
    }
    //public void Right_Onclick()
    //{
    //    if ( touch_right == false && StageDirector.Instance.GetNumStage() < 2 && now_area.name == "Area_1"
    //        || touch_left == false && StageDirector.Instance.GetNumStage() < 5 && now_area.name == "Area_2")
    //    {
    //        StageDirector.Instance.SetNumStage(1);
    //        touch_right = true;
    //        StartCoroutine(Change_right());
    //    }
    //}

    private void Move_Left()
    {
        now_area.GetComponent<RectTransform>().localPosition += new Vector3(TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    private void Move_Right()
    {
        now_area.GetComponent<RectTransform>().localPosition -= new Vector3(TOUCH_SPEED * Time.deltaTime, 0, 0);        //now_area.GetComponent<RectTransform>().anchoredPosition -= new Vector2(TOUCH_SPEED * Time.deltaTime, 0);
    }

    IEnumerator Change_left()
    {
        yield return new WaitForSeconds(move_time);
        touch_left = false;
    }

    IEnumerator Change_right()
    {
        yield return new WaitForSeconds(move_time);
        touch_right = false;
    }    
}