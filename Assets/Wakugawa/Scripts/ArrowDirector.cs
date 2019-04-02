using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetArrowPos(Transform armPos)
    {
        this.transform.localPosition = new Vector2(-armPos.localPosition.x, -armPos.localPosition.y);         // 矢印の位置を設定
        this.transform.localRotation = Quaternion.Euler(0, 0, armPos.localEulerAngles.z + 180);     // 矢印の回転角を設定
    }
}
