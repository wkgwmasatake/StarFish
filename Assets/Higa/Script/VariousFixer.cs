using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariousFixer : MonoBehaviour {

    [SerializeField] bool Scale;            // ヒトデから生成するエフェクトのみオン
    [SerializeField] bool FireWorks;        // 最後の爆発（花火）だけオン

    private ParticleSystem ps;
    
    private string EFFECT_SORTING_LAYER_NAME = "Effect";    // エフェクト用のレイヤー名

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {

        if(Scale)
            ScaleFix();



        AutoDelete();

        ParentCut();

        SetSortingLayer(transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ScaleFix()                              // ヒトデの大きさ補正を直す
    {
        ps.gameObject.transform.localScale /= 0.4f;
    }

    private void AutoDelete()                            // パーティクルの Duration で指定した時間で消えるように
    {
        Destroy(gameObject, (float)ps.main.duration);

        if (FireWorks)
        {
            
        }
    }

    private void ParentCut()                             // 親子関係を絶つ
    {
        gameObject.transform.parent = null;
    }

    private void SetSortingLayer(Transform parent)      // エフェクトが手前に表示されるように
    {
        //レンダラーがある場合のみレイヤーを設定
        if (parent.GetComponent<Renderer>())
        {
            parent.GetComponent<Renderer>().sortingLayerName = EFFECT_SORTING_LAYER_NAME;
        }

        //子がいる場合には、それにも同じ処理を行う
        foreach (Transform child in parent.transform)
        {
            SetSortingLayer(child);
        }
    }

    public void RotationY(float y)                      // 指定された分だけ Y軸を回転
    {
        ps.gameObject.transform.Rotate(new Vector3(0, 1, 0), y);
    }
}
