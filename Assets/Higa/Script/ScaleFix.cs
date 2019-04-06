﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFix : MonoBehaviour {

	// Use this for initialization
	void Start () {

        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.gameObject.transform.localScale /= 0.4f;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Yfix(float y)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.gameObject.transform.Rotate(new Vector3(0, 1, 0), y);

    }
}
