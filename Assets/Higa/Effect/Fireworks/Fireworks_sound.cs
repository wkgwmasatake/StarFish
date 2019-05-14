using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks_sound : MonoBehaviour {

    [SerializeField] AudioSource[] AS;

    private int array_n;

	// Use this for initialization
	void Start () {
        array_n = AS.Length;
        //Debug.Log(AS.Length);
        Invoke("PlaySound", 0.6f);
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
