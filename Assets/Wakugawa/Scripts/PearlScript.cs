using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlScript : MonoBehaviour {

    [SerializeField] ParticleSystem GetPearlParticle;       // パールを獲得したときのパーティクル

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Instantiate(GetPearlParticle,this.gameObject.transform);      // パーティクルを生成
            StartCoroutine("DestroyPearl");     // 1フレーム後に自分自身を破棄
        }
    }

    private IEnumerator DestroyPearl()
    {
        // 1フレーム後に自分自身を破棄
        yield return null;
        DestroyObject(this.gameObject);
    }
}
