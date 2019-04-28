using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{

    [SerializeField] private GameObject fish;
    [SerializeField] private float span;
    [SerializeField] private float speed;

    private float time = 0;

    private Vector2 pos;

    private GameObject PARENT;

    private void Start()
    {
        // 親オブジェクト生成
        PARENT = new GameObject("Fish_Parent");

        // 最初の生成
        CreateObj();
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
            CreateObj();
        }
    }


    /// <summary>
    /// 
    /// 　　　オブジェクト生成
    /// 
    /// </summary>
    private void CreateObj()
    {
        // 魚プレハブ生成
        GameObject obj = Instantiate(fish) as GameObject;

        // 位置ゼロリセット
        obj.transform.position = Vector2.zero;

        // 親設定
        obj.transform.parent = PARENT.transform;

        // 生成される場所設定
        obj.transform.position = transform.position;

        // 速さ設定
        obj.GetComponent<FishController>().SetSpeed = speed;

        // 位置により反転
        if (obj.transform.position.x > 0) obj.transform.localScale = new Vector2(-1, 1);
    }
}
