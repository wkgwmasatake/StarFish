using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoveScript : MonoBehaviour {
	
    [SerializeField] float MoveHorizontal;      // 魚の移動量

	// Update is called once per frame
	void Update () {
        transform.Translate(MoveHorizontal, 0, 0);       // 右に移動

        // 右の画面外に出たら左の画面外に移動
        if(transform.position.x > 5.0f)
        {
            transform.position = new Vector2(-5.0f, Random.Range(2.16f, 4.91f));
        }

        // 左の画面外に出たら右の画面外に移動
        if(transform.position.x < -5.0f)
        {
            transform.position = new Vector3(5.0f, Random.Range(2.16f, 4.91f));
        }
	}
}
