﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonClick : MonoBehaviour {

    [SerializeField] private float TOUCH_SPEED;
    [SerializeField] private GameObject Parent_area;
    [SerializeField] private GameObject Parent_icon;
    [SerializeField] private GameObject Director;
    [SerializeField] private float move_time;
    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Right;
    private bool touch_left = false;
    private bool touch_right = false;
    private AudioSource tap_SE;

    public enum StateNum// ステージの状態
    {
        Unlocked = 0, // 選択可能かつ未選択
        Locked = 1,// 選択不可能
        Selected = 2,// 選択可能かつ選択中
    }

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        tap_SE = GetComponent<AudioSource>();
        Left.gameObject.SetActive(false);         
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
        if(touch_left == true)
        {
            return;
        }
        int pos = AreaDirector.Instance.GetNumArea();
        if (touch_left == false && AreaDirector.Instance.GetNumArea() > 0)
        {
            int state = AreaDirector.Instance.GetStateArea(pos, -1);
            int now_state = AreaDirector.Instance.GetStateArea(pos, 0);

            //if (state != (int)StateNum.Locked)
            //{
            //    switch (state)
            //    {
            //        case (int)StateNum.Unlocked:
            //            AreaDirector.Instance.SetState_Selected(pos, -1);
            //            if (now_state != (int)StateNum.Locked)
            //            {
            //                AreaDirector.Instance.SetState_Unlocked(pos, 0);
            //            }
            //            break;
            //        case (int)StateNum.Selected:
            //            AreaDirector.Instance.SetState_Unlocked(pos, -1);
            //            if (now_state != (int)StateNum.Locked)
            //            {
            //                AreaDirector.Instance.SetState_Selected(pos, 0);
            //            }
            //            break;
            //    }
            //}
            //else
            //{
            //    if (now_state != (int)StateNum.Locked)
            //    {
            //        AreaDirector.Instance.SetState_Unlocked(pos, 0);
            //    }
            //}

            tap_SE.PlayOneShot(tap_SE.clip);
            AreaDirector.Instance.SetNumArea(-1);
            touch_left = true;
            StartCoroutine(Change_left());
        }
    }

    public void Right_Onclick()
    {
        if(touch_right == true)
        {
            return;
        }
        int pos = AreaDirector.Instance.GetNumArea();

        if (touch_right == false && AreaDirector.Instance.GetNumArea() < 4)
        {
            int state = AreaDirector.Instance.GetStateArea(pos, 1);
            int now_state = AreaDirector.Instance.GetStateArea(pos, 0);

            //if (state != (int)StateNum.Locked)
            //{
            //    switch (state)
            //    {
            //        case (int)StateNum.Unlocked:
            //            AreaDirector.Instance.SetState_Selected(pos, 1);
            //            if (now_state != (int)StateNum.Locked)
            //            {
            //                AreaDirector.Instance.SetState_Unlocked(pos, 0);
            //            }
            //            break;
            //        case (int)StateNum.Selected:
            //            AreaDirector.Instance.SetState_Unlocked(pos, 1);
            //            if (now_state != (int)StateNum.Locked)
            //            {
            //                AreaDirector.Instance.SetState_Selected(pos, 0);
            //            }
            //            break;
            //    }
            //}
            //else
            //{
            //    if (now_state != (int)StateNum.Locked)
            //    {
            //        AreaDirector.Instance.SetState_Unlocked(pos, 0);
            //    }
            //}

            tap_SE.PlayOneShot(tap_SE.clip);
            AreaDirector.Instance.SetNumArea(1);
            touch_right = true;
            StartCoroutine(Change_right());
        }
    }

    private void Move_Left()
    {
        //Parent_area.GetComponent<RectTransform>().anchoredPosition += new Vector2(TOUCH_SPEED * Time.deltaTime, 0);
        Parent_area.GetComponent<RectTransform>().localPosition += new Vector3(TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    private void Move_Right()
    {
        //Parent_area.GetComponent<RectTransform>().anchoredPosition -= new Vector2(TOUCH_SPEED * Time.deltaTime, 0);
        Parent_area.GetComponent<RectTransform>().localPosition -= new Vector3(TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    IEnumerator Change_left()
    {
        yield return new WaitForSeconds(move_time);
        touch_left = false;
        if (AreaDirector.Instance.GetNumArea() == 0 )
        {
            this.gameObject.SetActive(false);
        }
        else if (AreaDirector.Instance.GetNumArea() == 3)
        {
            Right.gameObject.SetActive(true);
        }
    }

    IEnumerator Change_right()
    {
        yield return new WaitForSeconds(move_time);
        touch_right = false;
        if (AreaDirector.Instance.GetNumArea() == 4 )
        {
            this.gameObject.SetActive(false);
        }
        else if (AreaDirector.Instance.GetNumArea() == 1)
        {
            Left.gameObject.SetActive(true);
        }
    }
    //public float height { get{ return height; } }//ゲッターセッター
}
