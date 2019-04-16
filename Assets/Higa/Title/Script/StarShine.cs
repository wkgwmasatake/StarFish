using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShine : MonoBehaviour {

    private bool scaleflg;
    private float startScale;

	// Use this for initialization
	void Start () {

        scaleflg = false;
        startScale = this.gameObject.transform.localScale.x;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(this.transform.localScale.x > 0f && scaleflg == false)
        {
            Vector3 scale = this.transform.localScale;
            scale.x -= 0.01f;
            scale.y -= 0.01f;
            scale.z -= 0.01f;

            this.transform.localScale = scale;

        }
        else if(scaleflg == false)
        {
            scaleflg = true;
        }

        if (this.transform.localScale.x < startScale && scaleflg == true)
        {
            Vector3 scale = this.transform.localScale;
            scale.x += 0.01f;
            scale.y += 0.01f;
            scale.z += 0.01f;

            this.transform.localScale = scale;
        }
        else if(scaleflg == true)
        {
            scaleflg = false;
        }

        Debug.Log(this.gameObject.transform.localScale);

	}
}
