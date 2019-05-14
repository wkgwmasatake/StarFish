using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks_sound_mini : MonoBehaviour {

    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip se_f_mini;

    private ParticleSystem ps;
    private bool emitflg;
    private int psnum;


	// Use this for initialization
	void Start () {

        ps = GetComponent<ParticleSystem>();
        emitflg = false;
        psnum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (psnum < getSubEmitterParticleNum())
        {
            psnum = getSubEmitterParticleNum();
        }
        else if(psnum > getSubEmitterParticleNum())
        {
            Invoke("PlaySound",1.0f);
            //AS.PlayOneShot(se_f_mini);
            psnum = getSubEmitterParticleNum();
        }
        
    }

    private int getSubEmitterParticleNum()
    {
        int ptNum = 0;
        ParticleSystem[] psArr = GetComponents<ParticleSystem>();
        foreach (ParticleSystem ps in psArr)
        {
            ptNum += ps.particleCount;
        }

        //Debug.Log(ptNum);
        return ptNum;
    }

    private void PlaySound()
    {
        AS.PlayOneShot(se_f_mini);
    }
}
