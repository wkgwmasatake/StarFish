using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFix : MonoBehaviour {

    //public float X;

	// Use this for initialization
	void Start () {

        //ParticleSystem ps = GetComponent<ParticleSystem>();

        //ps.gameObject.transform.rotation = Quaternion.Euler( X, 0, 0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Yfix(float y)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.gameObject.transform.Rotate(new Vector3(0, 1, 0), y);
        //ps.gameObject.transform.rotation = Quaternion.Euler(-90, x, 0);
        //ps.shape.rotation = Quaternion.Euler(0, 0, x);
    }
}
