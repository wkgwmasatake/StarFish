using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance_Gage : MonoBehaviour
{
    private GameObject player;
    private Slider _slider;

    private float pos = 0;

	// Use this for initialization
	void Start ()
    {
        //プレイヤーの情報を取得する
        player = GameObject.Find("starfish_betaPre");

        //スライダーを取得する
        _slider = GameObject.Find("Gage").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _slider.maxValue = GameDirector.Instance.GetStartDistance;

        pos = GameDirector.Instance.GetStartDistance - GameDirector.Instance.GetDistance;

        if(pos > _slider.maxValue)
        {
            pos = _slider.maxValue;
        }

        //反映
        _slider.value = pos;
	}
}