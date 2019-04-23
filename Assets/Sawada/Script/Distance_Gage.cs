using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance_Gage : MonoBehaviour
{
    private Slider _slider;

    private float pos = 0;

	// Use this for initialization
	void Start ()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Gage").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        // 最初のゴールまでの距離を取得
        _slider.maxValue = GameDirector.Instance.GetStartDistance;

        // 今のプレイヤーの位置からゴールまでの距離を引いて値を取得
        pos = GameDirector.Instance.GetStartDistance - GameDirector.Instance.GetDistance;


        // 最大値を超えたら最大値を代入
        if(pos > _slider.maxValue)
        {
            pos = _slider.maxValue;
        }

        // 反映
        _slider.value = pos;
	}
}