using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish_SE : MonoBehaviour
{
    private AudioSource SE;

	// Use this for initialization
	void Start ()
    {
        SE = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SE.Play();
        }
    }
}
