using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume_Change : MonoBehaviour {
    //[SerializeField] private GameObject bgm;
    private float bgm_volume;//初期音量
    private float se_volume;
    [SerializeField] private float min_vol = 0;
    [SerializeField] private float max_vol = 50;
    private AudioSource SE_Tap;
    private bool bgm_flg = true;//再生中か否か true:再生中 false:停止中
    private bool se_flg = true;
    [SerializeField] private GameObject volume;

    // Use this for initialization
    void Awake()
    {
        bgm_volume = max_vol;
        se_volume = max_vol;
    }

    void Start () {
        SE_Tap = GetComponent<AudioSource>();
	}
	
    public void ClickBGM()
    {
       if (bgm_flg == true)
        {
            OptionDirector.Instance.SetBGMvolume(min_vol);
            bgm_flg = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);//ロック状態を示す画像を表示   
            }
        }

        else
        {
            OptionDirector.Instance.SetBGMvolume(max_vol);
            bgm_flg = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);//ロック状態を示す画像を表示   
            }
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
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);//ロック状態を示す画像を表示   
            }
        }
        else
        {
            OptionDirector.Instance.SetSEvolume(max_vol);
            se_flg = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);//ロック状態を示す画像を表示   
            }
        }
        SE_Tap.volume = OptionDirector.Instance.GetSEvolume();
        SE_Tap.PlayOneShot(SE_Tap.clip);
    }
}
