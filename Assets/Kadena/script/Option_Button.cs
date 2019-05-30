using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Option_Button : MonoBehaviour {
    private string pre_scene = "";//sting型変数の受け取り用
    void Start()
    {
        pre_scene = PlayerPrefs.GetString("pre_scene");
    }

    public void ButtonOption()
    {
        PlayerPrefs.SetString("pre_scene", SceneManager.GetActiveScene().name);
        StartCoroutine(FadeScene("Option_Scene"));
        //SceneManager.LoadScene("Option_Scene");//オプションシーンへの遷移
    }

    public void ButtonArea()//オプションボタン以外へのシーン遷移
    {
        PlayerPrefs.SetString("pre_scene", SceneManager.GetActiveScene().name);
        string name = SceneManager.GetActiveScene().name;

        Debug.Log("リターン");

        switch (name)
        {
            case "Test_Stage_Select":
            case "Stage_Select":
                StartCoroutine(FadeScene("Title"));
                //SceneManager.LoadScene("Title");
                break;
            case "Option_Scene":
                StartCoroutine(FadeScene(pre_scene));

                //SceneManager.LoadScene(pre_scene);
                break;            
        }
    }

    IEnumerator FadeScene(string scene)
    {
        GameObject obj = GameObject.Find("Select_bgm");
        obj.GetComponent<BGM_Select>().Change_Fade_Flg();

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }
}
