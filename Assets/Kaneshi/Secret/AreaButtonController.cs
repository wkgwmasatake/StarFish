using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AreaButtonController : MonoBehaviour
{
    [SerializeField] public int areaNumber;
    [SerializeField] private Sprite[] unlockSprite;     //解放時の画像
    [SerializeField] private Sprite lockSprite;     //未解放時の画像

    private byte releaseflg;        //解放しているかどうかのフラグ

    private enum LOCKSTATE
    {
        LOCK,
        UNLOCK
    };

    // Use this for initialization
    void Start ()
    {

        if (releaseflg == (byte)LOCKSTATE.LOCK)
        {
            GetComponent<Image>().sprite = lockSprite;
        }
        else
        {
            GetComponent<Image>().sprite = unlockSprite[areaNumber];
        }

    }

    void SetReleaseFlg(byte value)
    {
        releaseflg = value;
    }
}
