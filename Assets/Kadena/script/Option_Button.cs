using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Option_Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonOption()
    {
        //SceneManager.LoadScene(option);//オプションシーンへの遷移
        Debug.Log("Optionへ遷移");
    }
    public void ButtonArea()
    {
        SceneManager.LoadScene("Area_Select");//オプションシーンへの遷移
        Debug.Log("Optionへ遷移");
    }
}
