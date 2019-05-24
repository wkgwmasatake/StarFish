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
        SceneManager.LoadScene("Option_Scene");//オプションシーンへの遷移
    }

    public void ButtonArea()//オプションボタン以外へのシーン遷移
    {
        PlayerPrefs.SetString("pre_scene", SceneManager.GetActiveScene().name);
        string name = SceneManager.GetActiveScene().name;

        switch (name)
        {
            case "Stage_Select":
                SceneManager.LoadScene("Title");
                break;
            case "Option_Scene":
                SceneManager.LoadScene(pre_scene);
                break;
            case "Area_Select":
                SceneManager.LoadScene("Title");
                break;
        }
        //if (SceneManager.GetActiveScene().name == "Stage_Select")
        //{
        //    SceneManager.LoadScene("Area_Select");
        //}
        //else if(SceneManager.GetActiveScene().name == "Option_Scene")
        //{
        //    SceneManager.LoadScene(pre_scene);
        //}
        //else if(SceneManager.GetActiveScene().name == "Title")
        //{
        //    SceneManager.LoadScene("Title");
        //}
    }
}
