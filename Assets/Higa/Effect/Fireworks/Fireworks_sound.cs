using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks_sound : MonoBehaviour {

    [SerializeField] AudioSource[] AS;
    [SerializeField] float time;

    private int array_n;

	// Use this for initialization
	void Start () {
        array_n = AS.Length;
        //Debug.Log(AS.Length);
        Invoke("PlaySound", time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void PlaySound()
    {
        for(int i = 0; i < array_n; i++)
        {
            AS[i].Play();
        }
    }
}
