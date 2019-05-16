using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Director : MonoBehaviour
{


    /// <summary>
    /// 情報格納用変数
    /// </summary>
    [SerializeField] private SceneObject SceneMenu;　         //シーンメニュー
    [SerializeField] private SceneObject SceneGameOrver;　    //シーンゲームオーバー
    [SerializeField] private SceneObject SceneResult;    　　 //シーンリザルト



    /// <summary>
    /// UI関連変数
    /// </summary>
    [SerializeField] private GameObject UI_Pause01;
    [SerializeField] private GameObject UI_Pause02;
    [SerializeField] private float time;
    [SerializeField] private GameObject CountDown;

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
    };
    SEState sState;
    ButtonState bState;
    [SerializeField] private AudioClip[] SE;

    [SerializeField] private CanvasGroup PanelAlpha;
    private float alphaPlus = 0.05f;


    // 各ステージ共通NAME
    private const string STAGE_NAME = "TestStage";

    private void Update()
    {
        // 花火が出終わったら
        if (GameDirector.Instance.ParticleFlg)
        {
            PanelAlpha.gameObject.SetActive(true);
            PanelAlpha.alpha += alphaPlus;
        }

        if (PanelAlpha != null)
        {
            if (Input.GetMouseButtonDown(0) && PanelAlpha.alpha < 1)
            {
                PanelAlpha.gameObject.SetActive(true);
                PanelAlpha.alpha = 1;
            }
        }
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
        StartCoroutine(SECourutin(ButtonState.Retry, SEState.SE2));
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

        //シーンがゲームオーバーかリザルトなら
        if(SceneManager.GetActiveScene().name == SceneGameOrver || SceneManager.GetActiveScene().name == SceneResult)
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
    }


    /// <summary>
    /// 
    ///     YES/NO 
    /// 
    /// </summary>
    public void CheckButton_YES()
    {
        StartCoroutine(SECourutin(ButtonState.YES, SEState.SE2));
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
        StartCoroutine(SECourutin(ButtonState.Next, SEState.SE2));
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
            case ButtonState.Retry:
                GameDirector.Instance.SetPauseFlg = false;
                // 前回プレイしたシーンを読み込み
                SceneManager.LoadScene(GameDirector.Instance.GetSceneName);
                break;

            case ButtonState.Play:
                Time.timeScale = 1;
                break;

            case ButtonState.YES:
                SceneManager.LoadScene(SceneMenu);
                break;

            case ButtonState.Menu:
                SceneManager.LoadScene(SceneMenu);
                break;

            case ButtonState.Next:
                Debug.Log(GameDirector.Instance.GetSceneNumber);
                SceneManager.LoadScene(STAGE_NAME + GameDirector.Instance.GetSceneNumber.ToString());
                break;
        }

        Time.timeScale = 1;

    }

    #endregion
}