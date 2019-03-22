using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    //スプライトの高さ、幅
    private float _height;

	// Use this for initialization
	void Start () {

        SpriteRenderer obje = GetComponent<SpriteRenderer>();
        _height = obje.bounds.size.y;
	}

    //ゲッター・セッター
    public float height { get { return _height; } }
}
