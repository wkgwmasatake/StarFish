using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetStatus : MonoBehaviour {

    [SerializeField] Image FadeImage;           // フェード用画像
    
    // 今までのデータを削除
    public void DeleteStatus()
    {
        // 今までのクリア状況をリセット
        if (PlayerPrefs.HasKey("STAGE"))
            PlayerPrefs.DeleteKey("STAGE");
        if (PlayerPrefs.HasKey("AREA"))
            PlayerPrefs.DeleteKey("AREA");

        GameDirector.Instance.SetAreaClear_Flg = 1;
        GameDirector.Instance.SetStageClear_Flg = 1;

        // タイトルを非同期で読み込みながらフェード処理
        StartCoroutine("SceneMoveTitle");
    }

    // 非同期でタイトルを読み込みながらタイトルへ遷移
    private IEnumerator SceneMoveTitle()
    {
        AsyncOperation title = SceneManager.LoadSceneAsync("Title");                // タイトルを非同期で読み込み
        title.allowSceneActivation = false;                                         // フェード処理が終わるまではシーン遷移を許可しない(注意点としてallowSceneActivationがfalseのままだとisDoneはfalseのまま)

        FadeImage.gameObject.SetActive(true);                                       // フェード画像のSetActiveをtrueに変更

        // 処理を軽くするためいったん関数を抜ける
        yield return null;

        while(!title.isDone)        // タイトルの読み込みがまだの時かつ、フェード処理が終わってない時
        {
            if(FadeImage.color.a < 1.0f)                // フェード処理がまだの時
            {
                // フェード画像のアルファ値を加算
                FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeImage.color.a + 0.02f);
            }
            else                                        // フェード処理が終わったら
            {
                title.allowSceneActivation = true;     // シーン遷移を許可
            }

            yield return null;
        }

    }
}
