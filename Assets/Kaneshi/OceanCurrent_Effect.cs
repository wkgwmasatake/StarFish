using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCurrent_Effect : MonoBehaviour
{
    private ParticleSystem.MinMaxCurve effectLength;
    private float effectSpeed;
    private GameObject[] oceanCurrents;


    // Use this for initialization
	void Start ()
    {
        //main.startLifetime = effectLength;
        //main.simulationSpeed = effectSpeed;

        //パーティクルシステムのメイン部分を取得
        var main = GetComponent<ParticleSystem>().main;

        //親パーティクルの表示時間、速さを取得
        effectLength = main.startLifetime;
        effectSpeed = main.simulationSpeed;

        //子パーティクルの表示時間、速さに親パーティクルの設定を適用
        foreach (Transform child in transform)
        {
            var CMain = child.GetComponent<ParticleSystem>().main;
            CMain.startLifetime = effectLength;
            CMain.simulationSpeed = effectSpeed;
        }

        //this.gameObject.AddComponent<BoxCollider2D>();
    }

}
