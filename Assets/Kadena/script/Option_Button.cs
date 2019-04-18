using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Option_Button : MonoBehaviour {

    public void ButtonOption()
    {
        SceneManager.LoadScene("Option_Scene");//オプションシーンへの遷移
    }
    public void ButtonArea()
    {
        SceneManager.LoadScene("Area_Select");//エリアシーンへの遷移
    }
}
