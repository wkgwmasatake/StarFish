using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour {

    private float lifetime;

	// Use this for initialization
	void Start () {

        gameObject.transform.parent = null;

        ParticleSystem partcleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, (float)partcleSystem.main.duration);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
