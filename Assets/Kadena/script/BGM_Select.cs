using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGM_Select : MonoBehaviour {
    
    //private static bool bgm_created = false;//static変数  
    private float fix_vol;
    AudioSource bgmAS;
    
    //[SerializeField] AudioClip[] clips;
    
    void Start()
    {
        bgmAS = GetComponent<AudioSource>();
        float vol = OptionDirector.Instance.GetBGMvolume();
        VolumeChange(vol);
        bgmAS.Play();
    }
    //public void BGMPlay(int num)//bgmの再生
    //{
    //    bgmAS.clip = clips[num];
    //    bgmAS.Play();
    //}

    public void VolumeChange(float num)//bgmの音量変更
    {
        fix_vol = num;
        bgmAS.volume = fix_vol;
    }

     public IEnumerator FadeOut()
    {

        yield return null;
    }
}
