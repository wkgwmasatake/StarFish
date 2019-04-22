using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGM_Select : MonoBehaviour {
    
    //private static bool bgm_created = false;//static変数  
    private float fix_vol;
    AudioSource bgmAS;
    
    //[SerializeField] AudioClip[] clips;

    //void Awake()
    //{
    //    if (!bgm_created)
    //    {
    //        //これが最初のシーンなら、以降シーン遷移で破棄されないようにする  
    //        DontDestroyOnLoad(gameObject);
    //        bgm_created = true;
    //    }
    //    else
    //    {
    //        //最初のシーンでないなら、すでに存在しているため破棄  
    //        Destroy(gameObject);
    //    }
    //}

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
}
