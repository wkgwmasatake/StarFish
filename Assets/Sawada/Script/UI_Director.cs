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
    [SerializeField] private SceneObject SceneNext;　         //シーンネクスト
    [SerializeField] private string NextSceneName;            //遷移するシーン名
    [SerializeField] private SceneObject SceneGameOrver;　    //シーンゲームオーバー
    [SerializeField] private SceneObject SceneResult;    　　 //シーンリザルト



    /// <summary>
    /// UI関連変数
    /// </summary>
    [SerializeField] private GameObject UI_Pause01;
    [SerializeField] private GameObject UI_Pause02;
    [SerializeField] private float time;
    [SerializeField] private GameObject CountDown;



    #region UI関連メソッド（Button用）



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
        //TimeScaleが０なら元に戻す
        if (Time.timeScale <= 0) Time.timeScale = 1;
        GameDirector.Instance.SetPauseFlg = false;

        Debug.Log(GameDirector.Instance.GetSceneName);

        // 前回プレイしたシーンを読み込み
        SceneManager.LoadScene(GameDirector.Instance.GetSceneName);
    }



    /// <summary>
    /// 
    ///      　再開
    /// 
    /// </summary>
    public void PlayButton()
    {
        //UI_Pause01を非アクティブ化
        UI_Pause01.SetActive(false);

        //TimeScaleが０なら元に戻す
        if (Time.timeScale <= 0)
        {
            CountDown.SetActive(true);

            StartCoroutine("CountDownCorutine");
            //TimeScaleを元に戻す
            Time.timeScale = 1;

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
        yield return new WaitForSeconds(1);
        CountDown.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        CountDown.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        GameDirector.Instance.SetPauseFlg = false;
        CountDown.SetActive(false);
        yield return new WaitForSeconds(1);
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
            SceneManager.LoadScene(SceneMenu);
        }
        //それ以外なら
        else
        {
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
        //メニューシーンがアタッチされていたら
        if (SceneMenu != null)
        {
            if (Time.timeScale <= 0) Time.timeScale = 1;
            SceneManager.LoadScene(SceneMenu);
        }
        //いなければ
        else
        {
            Debug.LogError("Not SceneMenu");
        }
    }
    public void CheckButton_NO()
    {
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
        //次のシーンがアタッチされていたら
        if (SceneNext != null)
        {
            SceneManager.LoadScene(NextSceneName);
        }
        //いなければ
        else
        {
            Debug.LogError("Not SceneNext");
        }
    }



    #endregion
}