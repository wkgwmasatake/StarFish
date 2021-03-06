﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDirector : SingletonMonoBehaviour<OptionDirector> {
    private float BGM_vol;
    private float SE_vol;
    private bool BGM_flg;
    private bool SE_flg;

    [SerializeField]private GameObject BGM_select;

    
    public void SetBGMvolume(float num)
    {
        BGM_vol = num;
        PlayerPrefs.SetFloat("BGM_Volume", BGM_vol);
    }

    public float GetBGMvolume()
    {
        BGM_vol = PlayerPrefs.GetFloat("BGM_Volume");
        return BGM_vol;
    }

    public void SetSEvolume(float num)
    {
        SE_vol = num;
        PlayerPrefs.SetFloat("SE_Volume", SE_vol);
    }

    public float GetSEvolume()
    {
        SE_vol = PlayerPrefs.GetFloat("SE_Volume");
        return SE_vol;
    }
    //BGMのフェードアウト処理
    public void startFadeoOutBGM()
    {
        StartCoroutine(BGM_select.GetComponent<BGM_Select>().FadeOut());
    }
}
