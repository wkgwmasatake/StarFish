using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{

    [SerializeField] private Sprite fish;
    [SerializeField] private float span;
    [SerializeField] private float speed;

    private float time = 0;

    private Vector2 pos;

    private GameObject PARENT;

    private void Start()
    {
        PARENT = new GameObject("Fish_Parent");
    }

    // Update is called once per frame
    void Update ()
    {
        time += Time.deltaTime;
        if(time >= span)
        {
            // タイムリセット
            time = 0;

            // オブジェクト生成
            GameObject fish = CreateObj();

            // 位置リセット
            fish.transform.position = Vector2.zero;

            // 親設定  
            fish.transform.parent = PARENT.transform;

            // 生成される場所設定
            fish.transform.position = transform.position;
            if (fish.transform.position.x > 0) fish.transform.localScale = new Vector2(-1, 1);

            // コライダー設定
            CreateCollider(fish);
        }
    }


    /// <summary>
    /// 
    /// 　　　オブジェクト生成
    /// 
    /// </summary>
    private GameObject CreateObj()
    {
        // オブジェクト生成
        GameObject Fish = new GameObject("FishObj");

        // スプライトレンダラー追加・スプライト追加
        Fish.AddComponent<SpriteRenderer>().sprite = fish;
        // レイヤー設定
        Fish.GetComponent<SpriteRenderer>().sortingOrder = 2;

        // スクリプト設定
        Fish.AddComponent<FishController>();
        // 移動速度設定
        Fish.GetComponent<FishController>().SetSpeed = speed;

        return Fish;
    }



    /// <summary>
    /// 
    ///      コライダー生成
    /// 
    /// </summary>
    /// <param name="obj"></param>
    void CreateCollider(GameObject obj)
    {
        // ポリゴンコライダー追加
        obj.AddComponent<PolygonCollider2D>();
    }
}
