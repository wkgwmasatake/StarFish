using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AreaButtonController : MonoBehaviour
{
    [SerializeField] private int areaNumber;
    [SerializeField] private Sprite[] unlockSprite;     //解放時の画像
    [SerializeField] private Sprite lockSprite;         //未解放時の画像
    [SerializeField] private GameObject StageDirector;

    private Vector3 originposition;                  //初期X座標
    private float moveXposition;                    //行きたいX座標
    private float movespeed = 30f;
    private SecretAreaDirector ADirector;
    private int beforeANumber;
    private bool moveFlg;

    private RectTransform RTransform;

    private byte releaseflg;        //解放しているかどうかのフラグ

    public enum LOCKSTATE
    {
        LOCK,
        UNLOCK
    };

    // Use this for initialization
    void Start ()
    {
        RTransform = GetComponent<RectTransform>();
        ADirector = GameObject.Find("AreaSelectDirector").GetComponent<SecretAreaDirector>();
        originposition = RTransform.anchoredPosition;

        if (releaseflg == (byte)LOCKSTATE.LOCK)
        {
            GetComponent<Image>().sprite = lockSprite;
        }
        else
        {
            GetComponent<Image>().sprite = unlockSprite[areaNumber];
        }
    }

    private void Update()
    {
        int difference = ADirector.GetSelectAreaNunber() - beforeANumber;
        Debug.Log(difference);

        //AreaNumberが変わっていたら、合わせる準備をする
        if (ADirector.GetSelectAreaNunber() != beforeANumber)
        {
            if (!moveFlg)
            {
                moveFlg = true;
                //beforeANumber = ADirector.GetSelectAreaNunber();
                moveXposition = RTransform.anchoredPosition.x +
                    ((float)-difference * ADirector.GetButtonXOffset());

                Debug.Log(moveXposition);
            }
        }

        if (!moveFlg) return;

        if (moveXposition != RTransform.anchoredPosition.x)
        {
            RTransform.anchoredPosition =
                new Vector2(RTransform.anchoredPosition.x + (movespeed * -difference),
                            RTransform.anchoredPosition.y);
        }

        //目標座標に近くなったら、目標座標に上書き
        //移動フラグを切り替え、前回の選択エリア番号に今の番号を入れる
        if ((ADirector.GetButtonXOffset() * 0.03f) >
            Mathf.Abs(moveXposition - RTransform.anchoredPosition.x))
        {
            RTransform.anchoredPosition =
                new Vector3(moveXposition, originposition.y, originposition.z);

            moveFlg = false;
            beforeANumber = ADirector.GetSelectAreaNunber();
        }

    }

    public void SetReleaseFlg(byte value)
    {
        releaseflg = value;
    }

    public void SetAreaNumber(int value)
    {
        areaNumber = value;
    }

    public void ChangeScene()
    {
        GameObject SDirector = Instantiate(StageDirector) as GameObject;
        SceneManager.LoadScene("StageSelect_Secret");
    }
}
