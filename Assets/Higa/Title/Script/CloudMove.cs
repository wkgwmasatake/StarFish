using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    [SerializeField] float direction;       // 右なら 1 , 左なら -1 を入力
    [SerializeField] float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var pos = this.transform.position;
        pos.x += speed * direction;

        this.transform.position = pos;

        if(this.transform.position.x < -6.0f)
        {
            pos.x = 5.0f;
            this.transform.position = pos;
        }

	}
}
