using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonController : MonoBehaviour
{
    private int stageNumber;
    [SerializeField] private Sprite[] unlockSprite;     //解放時の画像
    [SerializeField] private Sprite lockSprite;     //未解放時の画像

    private float originXposition;  //初期X座標
    private GameObject Adirector;

    private byte releaseflg;        //解放しているかどうかのフラグ

    public enum LOCKSTATE
    {
        LOCK,
        UNLOCK
    };

    private GameObject SDirector;

	// Use this for initialization
	void Start ()
    {
        SDirector = GameObject.Find("StageSelectDirector");
        originXposition = transform.position.x;

        if (releaseflg == (byte)LOCKSTATE.LOCK)
        {
            GetComponent<Image>().sprite = lockSprite;
        }
        else
        {
            GetComponent<Image>().sprite = unlockSprite[stageNumber];
        }
    }

    private void Update()
    {
        
    }

    public void SetReleaseFlg(byte value)
    {
        releaseflg = value;
    }

    public void SetStageNumber(int value)
    {
        stageNumber = value;
    }

}
