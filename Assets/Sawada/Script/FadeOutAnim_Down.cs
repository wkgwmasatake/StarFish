using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAnim_Down : MonoBehaviour
{
    private Animation anim;
    private GameObject obj;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();

        obj = GameObject.Find("Canvas_beta").transform.GetChild(1).gameObject;
	}

    private void Update()
    {
        if(anim.IsPlaying("Stage_FadeOut_Down"))
        {
            //obj.SetActive(false);
        }
    }
}