using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    //スプライトの高さ、幅
    private float _height;
    private float _width;

	// Use this for initialization
	void Start () {

        SpriteRenderer obje = GetComponent<SpriteRenderer>();
        _height = obje.bounds.size.y;
        _width = obje.bounds.size.x;
	}

    //ゲッター・セッター
    public float height { get { return _height; } }
    public float width  { get { return _width;  } }
}
