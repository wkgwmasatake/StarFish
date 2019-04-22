using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tap_Test : MonoBehaviour {

    [SerializeField] private SceneObject scene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(scene);
        }
	}
}
