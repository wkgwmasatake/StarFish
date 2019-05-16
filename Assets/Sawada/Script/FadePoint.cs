using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePoint : MonoBehaviour
{
    private GameObject camera;

	// Use this for initialization
	void Start ()
    {
        camera = GameObject.Find("Main Camera").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z);
	}
}
