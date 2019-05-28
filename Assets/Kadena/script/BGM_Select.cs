using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGM_Select : MonoBehaviour {
    
    //private static bool bgm_created = false;//static変数  
    private float fix_vol;

    private bool fade_flg = false;
    AudioSource bgmAS;
    
    //[SerializeField] AudioClip[] clips;
    
    void Start()
    {
        bgmAS = GetComponent<AudioSource>();
        float vol = OptionDirector.Instance.GetBGMvolume();
        VolumeChange(vol);
        bgmAS.Play();
    }
    void Update()
    {
        if(fade_flg == true) { FadeoutBGM(); }
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

    public void Change_Fade_Flg()
    {
        fade_flg = !fade_flg;
    }

    private void FadeoutBGM()
    {
        bgmAS.volume -= 0.02f;
    }

     public IEnumerator FadeOut()
    {
        for (; bgmAS.volume < 0; bgmAS.volume--)
        {
            bgmAS.volume = (float)bgmAS.volume / 1000;
        }
        bgmAS.volume = 0;
        Debug.Log(bgmAS.volume);
        bgmAS.enabled = false;
        yield return null;
    }
}
