using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour {


	// Use this for initialization
	void Start () {

        gameObject.transform.parent = null;

        ParticleSystem ps = GetComponent<ParticleSystem>();

        Destroy(gameObject, (float)ps.main.duration);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
