using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingPalm : MonoBehaviour {

    [SerializeField] float angleMin;        // 0以上
    [SerializeField] float angleMax;        // 360以下
    [SerializeField] float speed;           // 1フレーム当たりの角度

    private bool turnflg;

	// Use this for initialization
	void Start () {

        turnflg = true;

	}
	
	// Update is called once per frame
	void Update () {

        if (turnflg == false && this.transform.eulerAngles.z <= angleMin)
        {
            turnflg = true;
        }
        else if (turnflg == true && this.transform.eulerAngles.z >= angleMax)
        {
            turnflg = false;
        }

        var rotation = this.transform.eulerAngles;
        //Debug.Log(this.gameObject.transform.eulerAngles);

        if(turnflg == false)
        {
            rotation.z -= speed;
            this.transform.eulerAngles = rotation;
            //Debug.Log("to Min");
        }
        else
        {
            rotation.z += speed;
            this.transform.eulerAngles = rotation;
            //Debug.Log("to Max");
        }



	}
}
