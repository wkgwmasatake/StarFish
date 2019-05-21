using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_CameraAnim : MonoBehaviour
{
    private Animation anim;
    private bool _animFlg = false;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();
	}
	
    public IEnumerator CameraMove()
    {
        anim.Play();
        yield return new WaitForSecondsRealtime(2.0f);
    }
}
