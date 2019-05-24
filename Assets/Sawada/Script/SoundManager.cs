using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource BGM;
    private bool FadeFlg = false;

    [SerializeField] private float VolumeDown;    // 下げる音量
    
	// Use this for initialization
	void Start ()
    {
        BGM = GetComponent<AudioSource>();
	}
	
    public void BGM_Fade()
    {
        StartCoroutine("BGM_FadeCoroutine");
    }

    IEnumerator BGM_FadeCoroutine()
    {
        while(BGM.volume > 0)
        {
            BGM.volume -= VolumeDown;
            yield return null;
        }
    }
}
