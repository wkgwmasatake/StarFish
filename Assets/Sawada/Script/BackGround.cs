using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    /// <summary>
    /// BackGroundの
    /// 高さ、幅　用変数
    /// </summary>
    private float _height;
    private float _width;

	// Use this for initialization
	void Start ()
    {
        // Spriterendererの情報を取得
        SpriteRenderer obje = GetComponent<SpriteRenderer>();

        // 高さ、幅を取得
        _height = obje.bounds.size.y;
        _width = obje.bounds.size.x;
	}


    /// <summary>
    /// 
    ///     ゲッター・セッター
    /// 
    /// </summary>
    public float height { get { return _height; } }
    public float width  { get { return _width;  } }
}
