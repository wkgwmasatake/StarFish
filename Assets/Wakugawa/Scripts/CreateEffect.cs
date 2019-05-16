using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateEffect : MonoBehaviour {

    [SerializeField] ParticleSystem SmokeParticle;        // 土煙のパーティクル

    public void InstantiateParticle()
    {
        // 土煙のパーティクルを2つ生成
        var SmokeRight = Instantiate(SmokeParticle);
        var SmokeLeft = Instantiate(SmokeParticle);

        // 左側のエフェクトを反転させる
        SmokeLeft.transform.localScale = new Vector3(-1, 1, 1);
    }
}
