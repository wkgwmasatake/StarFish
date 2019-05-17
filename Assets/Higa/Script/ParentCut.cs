using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCut : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.parent = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
