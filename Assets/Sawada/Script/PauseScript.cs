using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private float RectLeft;
    [SerializeField] private float RectRight;
    [SerializeField] private float RectTop;
    [SerializeField] private float RectButtom;

    [SerializeField] private GameObject UI_Pause01;

    void PauseButton()
    {
        Time.timeScale = 0;
        if (Time.timeScale <= 0) { GameDirector.Instance.SetPauseFlg = true; }
        else{ Debug.LogError("TimeScaleError"); }

        GameObject ui = Instantiate(UI_Pause01) as GameObject;
        Vector2 RectPos = Vector2.zero;
        ui.transform.parent = GameObject.Find("Canvas").transform;
        ui.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        ui.GetComponent<RectTransform>().localScale = Vector2.one;
        ui.GetComponent<RectTransform>().offsetMax = new Vector2(RectRight, RectTop);
        ui.GetComponent<RectTransform>().offsetMin = new Vector2(RectLeft, RectButtom);
    }
}