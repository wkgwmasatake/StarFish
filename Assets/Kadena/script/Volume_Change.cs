using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume_Change : MonoBehaviour
{
    //[SerializeField] private GameObject bgm;
    private float bgm_volume;//初期音量
    private float se_volume;
    [SerializeField] private float min_vol = 0;
    [SerializeField] private float max_vol = 50;
    private AudioSource SE_Tap;
    private bool bgm_flg = true;//再生中か否か true:再生中 false:停止中
    private bool se_flg = true;
    //float型変数取得用の変数
    private float get_bgm;
    private float get_se;
    [SerializeField] private GameObject volume;

    // Use this for initialization
    void Awake()
    {
        bgm_volume = max_vol;
        se_volume = max_vol;
    }

    void Start()
    {
        SE_Tap = GetComponent<AudioSource>();
        if (OptionDirector.Instance.GetBGMvolume() == min_vol)
        {
            bgm_flg = false;
            ChangeIcon(false);
        }
        else if (OptionDirector.Instance.GetBGMvolume() == max_vol)
        {
            bgm_flg = true;
            ChangeIcon(true);
        }

        if (OptionDirector.Instance.GetSEvolume() == min_vol)
        {
            se_flg = false;
            ChangeIcon(false);
        }
        else if (OptionDirector.Instance.GetSEvolume() == max_vol)
        {
            se_flg = true;
            ChangeIcon(true);
        }
    }
    public void ClickBGM()
    {
        if (bgm_flg == true)
        {
            OptionDirector.Instance.SetBGMvolume(min_vol);
            bgm_flg = false;
            ChangeIcon(false);
        }
        else
        {
            OptionDirector.Instance.SetBGMvolume(max_vol);
            bgm_flg = true;
            ChangeIcon(true);
        }
        float bgm_vol = OptionDirector.Instance.GetBGMvolume();
        volume.GetComponent<BGM_Select>().VolumeChange(bgm_vol);
    }
    public void ClickSE()
    {
        if (se_flg == true)
        {
            OptionDirector.Instance.SetSEvolume(min_vol);
            se_flg = false;
            ChangeIcon(false);
        }
        else
        {
            OptionDirector.Instance.SetSEvolume(max_vol);
            se_flg = true;
            ChangeIcon(true);
        }
        SE_Tap.volume = OptionDirector.Instance.GetSEvolume();
        SE_Tap.PlayOneShot(SE_Tap.clip);
    }

    private void ChangeIcon( bool vol)//ロック状態を示す画像を表示   
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(vol);
        }
    }
}
