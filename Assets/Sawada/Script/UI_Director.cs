﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Director : MonoBehaviour
{


    // 各ステージ共通NAME
    private const string STAGE_NAME = "TestStage";


    /// <summary>
    /// 情報格納用変数
    /// </summary>
    [SerializeField] private SceneObject SceneMenu;　         //シーンメニュー
    [SerializeField] private SceneObject SceneGameOrver;　    //シーンゲームオーバー
    [SerializeField] private SceneObject SceneResult;    　　 //シーンリザルト
    [SerializeField] private GameObject player;




    /// <summary>
    /// UI関連変数
    /// </summary>
    [SerializeField] private GameObject UI_Pause01;
    [SerializeField] private GameObject UI_Pause02;
    [SerializeField] private float time;
    [SerializeField] private GameObject CountDown;
    private GameObject Panel_UI;       // UIのPanel情報格納用変数
    private GameObject Panel_UI_Title;
    private GameObject PauseUI_Panel;  // UIのパネル
    private GameObject PauseUI_Panel_Yes;
    private GameObject Pause_UI;       // ポーズUI




    /// <summary>
    ///  SE関連変数
    /// </summary>
    private enum SEState
    {
        SE1,
        SE2,
        SE3,
    };
    private enum ButtonState
    {
        Pause,
        Retry,
        Play,
        Menu,
        YES,
        NO,
        Next,
        Title,
    };
    SEState sState;
    ButtonState bState;




    [SerializeField] private AudioClip[] SE;
    [SerializeField] private GameObject Fade_Down;
    [SerializeField] private GameObject Black_Fade;
    [SerializeField] private CanvasGroup PanelAlpha;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private float VolumeDown;
    [SerializeField] private float downAlpha;
    

    private bool isPlayingCourutin = false;   // Update内のコルーチン用フラグ


    private float alphaPlus = 0.05f;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneGameOrver || SceneManager.GetActiveScene().name == SceneResult)
        {
            Panel_UI = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
            Panel_UI_Title = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        }
        else
        {
            PauseUI_Panel = GameObject.Find("Canvas_beta").transform.GetChild(1).transform.GetChild(2).gameObject;
            Pause_UI = GameObject.Find("Canvas_beta").transform.GetChild(1).transform.GetChild(1).gameObject;
            PauseUI_Panel_Yes = GameObject.Find("Canvas_beta").transform.GetChild(1).transform.GetChild(3).gameObject;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == SceneGameOrver || SceneManager.GetActiveScene().name == SceneResult)
        {
            // 花火が出終わったら
            if (GameDirector.Instance.ParticleFlg)
            {
                if (GameDirector.Instance.GetSceneNumber != 16 || SceneManager.GetActiveScene().name == SceneGameOrver)
                {
                    StartCoroutine(UI_FadeCourutin(Panel_UI));
                }
                else
                {
                    StartCoroutine(UI_FadeCourutin(Panel_UI_Title));
                }
            }
        }
    }
    IEnumerator UI_FadeCourutin(GameObject panel)
    {
        // このコルーチンに一度でも入ったら強制的にbreakで抜ける
        if (isPlayingCourutin)
        {
            yield break;
        }

        while (panel.GetComponent<CanvasGroup>().alpha < 1)
        {
            panel.SetActive(true);
            panel.GetComponent<CanvasGroup>().alpha += downAlpha;

            yield return null;
        }

        // 一度目に入ったのでフラグを立てる
        isPlayingCourutin = true;
    }


    #region UI関連メソッド（Button用）
    // Use this for initialization

    /// <summary>
    /// 
    ///  　　　一時停止
    ///  
    /// </summary>
    public void PauseButton()
    {
        //ポーズが呼ばれたか →　一度でも呼ばれていると入らない
        if (!GameDirector.Instance.GetPauseFlg)
        {
            GetComponent<AudioSource>().clip = SE[0];
            GetComponent<AudioSource>().Play();

            Time.timeScale = 0;
            if (Time.timeScale <= 0) { GameDirector.Instance.SetPauseFlg = true; }
            else { Debug.LogError("TimeScaleError"); }

            //UI_Pause01アクティブ化
            UI_Pause01.SetActive(true);
        }
        else
        {
            Debug.LogError("PauseFlgError");
        }
    }


    /// <summary>
    /// 
    /// 　　　リトライ
    /// 
    /// </summary>
    public void RetryButton()
    {
        // ２度押し防止
        if(!GameDirector.Instance.GetRetryFlg)
        {
            StartCoroutine(SECourutin(ButtonState.Retry, SEState.SE2));
            GameDirector.Instance.SetRetryFlg = true;
        }
    }


    /// <summary>
    /// 
    ///      　再開
    /// 
    /// </summary>
    public void PlayButton()
    {
        SEPlay(SEState.SE2);

        //UI_Pause01を非アクティブ化
        UI_Pause01.SetActive(false);

        //TimeScaleが０なら元に戻す
        if (Time.timeScale <= 0)
        {
            CountDown.SetActive(true);

            StartCoroutine("CountDownCorutine");

        }
        else
        {
            Debug.LogError("NotPlay!");
        }
    }
    // コルーチン
    IEnumerator CountDownCorutine()
    {
        CountDown.GetComponent<Text>().text = "3";
        yield return new WaitForSecondsRealtime(1);
        CountDown.GetComponent<Text>().text = "2";
        yield return new WaitForSecondsRealtime(1);
        CountDown.GetComponent<Text>().text = "1";
        yield return new WaitForSecondsRealtime(1);
        GameDirector.Instance.SetPauseFlg = false;
        CountDown.SetActive(false);
        //TimeScaleを元に戻す
        Time.timeScale = 1;
    }


    /// <summary>
    /// 
    ///     メニュー
    /// 
    /// </summary>
    public void MenuButton()
    {
        // ２度押し防止
        if(!GameDirector.Instance.GetMenuFlg)
        {
            //シーンがゲームオーバーかリザルトなら
            if (SceneManager.GetActiveScene().name == SceneGameOrver || SceneManager.GetActiveScene().name == SceneResult)
            {
                StartCoroutine(SECourutin(ButtonState.Menu, SEState.SE2));
            }
            //それ以外なら
            else
            {
                SEPlay(SEState.SE2);

                //UI_Pause02アクティブ化
                UI_Pause02.SetActive(true);

                //UI_Pause01非アクティブ化
                UI_Pause01.SetActive(false);
            }
            GameDirector.Instance.SetMenuFlg = true;
        }
    }


    /// <summary>
    /// 
    ///     YES/NO 
    /// 
    /// </summary>
    public void CheckButton_YES()
    {
        // ２度押し防止
        if(!GameDirector.Instance.GetMenu_YesFlg)
        {
            StartCoroutine(SECourutin(ButtonState.YES, SEState.SE2));
            GameDirector.Instance.SetMenu_YesFlg = true;
        }
    }
    public void CheckButton_NO()
    {
        SEPlay(SEState.SE3);
        //UI_Pause02非アクティブ化
        UI_Pause02.SetActive(false);

        //UI_Pause01アクティブ化
        UI_Pause01.SetActive(true);
    }


    /// <summary>
    /// 
    ///     ネクスト
    /// 
    /// </summary>
    public void NextButton()
    {
        // ２度押し防止
        if (!GameDirector.Instance.GetNextFlg)
        {
            StartCoroutine(SECourutin(ButtonState.Next, SEState.SE2));
            GameDirector.Instance.SetNextFlg = true;
        }
    }

    /// <summary>
    ///  タイトルへ
    /// </summary>
    public void Title()
    {
        StartCoroutine(SECourutin(ButtonState.Title, SEState.SE2));
    }

    /// <summary>
    ///  再生メソッド
    /// </summary>
    void SEPlay(SEState sState)
    {
        switch(sState)
        {
            case SEState.SE1:
                GetComponent<AudioSource>().clip = SE[0];
                break;

            case SEState.SE2:
                GetComponent<AudioSource>().clip = SE[1];
                break;

            case SEState.SE3:
                GetComponent<AudioSource>().clip = SE[2];
                break;
        }

        GetComponent<AudioSource>().Play();

    }



    /// <summary>
    ///  再生終了コルーチン
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator SECourutin(ButtonState bState, SEState sState)
    {
        SEPlay(sState);

        yield return new WaitForSecondsRealtime(0.5f);

        // 各関数で分岐
        switch(bState)
        {
            case ButtonState.Retry:  // もう一度

                // 現在のシーンがリザルトまたはゲームオーバーのシーンなら
                if (SceneManager.GetActiveScene().name == SceneGameOrver || SceneManager.GetActiveScene().name == SceneResult)
                {
                    //Fade_Downオブジェクト生成
                    GameObject retry_Obj = Instantiate(Fade_Down) as GameObject;
                    retry_Obj.transform.parent = GameObject.Find("FadePoint").transform; // FadePointを探してその子に設定

                    StartCoroutine("VolumeDown_Courutin");   // BGMフェード

                    // アルファ値を０まで下げる
                    while (Panel_UI.GetComponent<CanvasGroup>().alpha > 0)
                    {
                        Panel_UI.GetComponent<CanvasGroup>().alpha -= downAlpha;
                        yield return null;
                    }
                }
                // それ以外なら
                else
                {
                    if (Time.timeScale <= 0) Time.timeScale = 1;                     // タイムスケールを元に戻す
                    player.GetComponent<StarFishOriginal>().RemoveGravity();         // 海星の重力を消す
                    GameObject retry_Obj = Instantiate(Fade_Down) as GameObject;         // Fade_Dwonオブジェクト生成
                    retry_Obj.transform.parent = GameObject.Find("FadePoint").transform; // FadePointを探してその子に設定

                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Fade();

                    // 指定のUIのアルファ値が０になるまで回す
                    while (PauseUI_Panel.GetComponent<CanvasGroup>().alpha > 0 && Pause_UI.GetComponent<CanvasGroup>().alpha > 0)
                    {
                        PauseUI_Panel.GetComponent<CanvasGroup>().alpha -= downAlpha;
                        Pause_UI.GetComponent<CanvasGroup>().alpha -= downAlpha;
                        yield return null;
                    }
                }

                yield return new WaitForSecondsRealtime(2);

                // 前回プレイしたシーンを読み込み
                SceneManager.LoadScene(GameDirector.Instance.GetSceneName);

                break;

            case ButtonState.Play:  // 再開
                Time.timeScale = 1;
                break;

            case ButtonState.YES:

                if (Time.timeScale <= 0) Time.timeScale = 1;
                player.GetComponent<StarFishOriginal>().RemoveGravity();     // 海星の重力を消す
                GameObject yes_Obj = Instantiate(Black_Fade) as GameObject;
                yes_Obj.transform.parent = GameObject.Find("FadePoint").transform;

                GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Fade();

                // 指定のUIのアルファ値が０になるまで回す
                while (PauseUI_Panel_Yes.GetComponent<CanvasGroup>().alpha > 0 && Pause_UI.GetComponent<CanvasGroup>().alpha > 0)
                {
                    PauseUI_Panel_Yes.GetComponent<CanvasGroup>().alpha -= downAlpha;
                    Pause_UI.GetComponent<CanvasGroup>().alpha -= downAlpha;
                    yield return null;
                }
                yield return new WaitForSecondsRealtime(2.0f);
                SceneManager.LoadScene(SceneMenu);
                break;

            case ButtonState.Menu:  // メニュー

                GameObject menu_Obj = Instantiate(Black_Fade) as GameObject;         // Black_Fade生成
                menu_Obj.transform.parent = GameObject.Find("FadePoint").transform;  // FadePointをさがしてその子に設定

                StartCoroutine("VolumeDown_Courutin");   // BGMフェード

                while (Panel_UI.GetComponent<CanvasGroup>().alpha > 0)
                {
                    Panel_UI.GetComponent<CanvasGroup>().alpha -= downAlpha;
                    yield return null;
                }
                yield return new WaitForSecondsRealtime(2.0f);
                SceneManager.LoadScene(SceneMenu);
                break;

            case ButtonState.Next:  // 次

                GameObject next_Obj = Instantiate(Fade_Down) as GameObject;
                next_Obj.transform.parent = GameObject.Find("FadePoint").transform; // FadePointを探してその子に設定

                StartCoroutine("VolumeDown_Courutin");   // BGMフェード

                while (Panel_UI.GetComponent<CanvasGroup>().alpha > 0)
                {
                    Panel_UI.GetComponent<CanvasGroup>().alpha -= downAlpha;
                    Debug.Log(Panel_UI.GetComponent<CanvasGroup>().alpha);
                    yield return null;
                }

                yield return new WaitForSecondsRealtime(2.0f);
                SceneManager.LoadScene(STAGE_NAME + GameDirector.Instance.GetSceneNumber.ToString());

                break;

            case ButtonState.Title:  // タイトルへ

                GameObject title_Obj = Instantiate(Black_Fade) as GameObject;
                title_Obj.transform.parent = GameObject.Find("FadePoint").transform;

                StartCoroutine("VolumeDown_Courutin");   // BGMフェード

                while (Panel_UI_Title.GetComponent<CanvasGroup>().alpha > 0)
                {
                    Panel_UI_Title.GetComponent<CanvasGroup>().alpha -= downAlpha;
                    Debug.Log(Panel_UI.GetComponent<CanvasGroup>().alpha);
                    yield return null;
                }

                yield return new WaitForSecondsRealtime(2.0f);
                SceneManager.LoadScene("SpecialResult");

                break;
        }

        Time.timeScale = 1;

    }

    // BGMフェード用コルーチン
    IEnumerator VolumeDown_Courutin()
    {
        while (BGM.volume > 0)
        {
            BGM.volume -= VolumeDown;
            yield return null;
        }
    }

    #endregion
}