using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Director : SingletonMonoBehaviour<UI_Director>
{
    //シーンメイン
    [SerializeField] private SceneObject SceneMain;

    //シーンメニュー
    [SerializeField] private SceneObject SceneMenu;

    //シーンネクスト
    [SerializeField] private SceneObject SceneNext;

    [SerializeField] private GameObject UI_Pause01;
    [SerializeField] private GameObject UI_Pause02;

    #region UI

    //一時停止
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
    }

    //リトライ
    public void RetryButton()
    {
        //TimeScaleが０なら元に戻す
        if (Time.timeScale <= 0) Time.timeScale = 1;
        GameDirector.Instance.SetPauseFlg = false;

        Debug.Log(GameDirector.Instance.GetSceneName);

        // 前回プレイしたシーンを読み込み
        SceneManager.LoadScene(GameDirector.Instance.GetSceneName);
    }

    //再開
    public void PlayButton()
    {
        //TimeScaleが０なら元に戻す
        if (Time.timeScale <= 0)
        {
            //TimeScaleを元に戻す
            Time.timeScale = 1;
            GameDirector.Instance.SetPauseFlg = false;

            Debug.Log("Play");
        }
        else
        {
            Debug.LogError("NotPlay!");
        }

        //UI_Pause01を非アクティブ化
        UI_Pause01.SetActive(false);

    }

    //メニュー
    public void MenuButton()
    {
        //現在のシーンがメインなら
        if(SceneManager.GetActiveScene().name == SceneMain)
        {
            //UI_Pause02アクティブ化
            UI_Pause02.SetActive(true);

            //UI_Pause01非アクティブ化
            UI_Pause01.SetActive(false);
        }
        //それ以外なら
        else
        {
            //メニューシーンがアタッチされていたら
            if(SceneMenu != null)
            {
                SceneManager.LoadScene(SceneMenu);
            }
            //いなければ
            else
            {
                Debug.Log("Not SceneMenu");
            }
        }
    }

    //確認用ボタン
    public void CheckButton_YES()
    {
        Debug.Log("Load Scene!");
    }
    public void CheckButton_NO()
    {
        //UI_Pause02非アクティブ化
        UI_Pause02.SetActive(false);

        //UI_Pause01アクティブ化
        UI_Pause01.SetActive(true);
    }

    //Nextボタン
    public void NextButton()
    {
        //次のシーンがアタッチされていたら
        if(SceneNext != null)
        {
            SceneManager.LoadScene(SceneNext);
        }
        //いなければ
        else
        {
            Debug.Log("Not SceneNext");
        }
    }

    #endregion
}