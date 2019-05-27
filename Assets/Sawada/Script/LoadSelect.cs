using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelect : MonoBehaviour
{

    private AsyncOperation SelectScene;

	// Use this for initialization
	void Start ()
    {
        SelectScene = GameDirector.Instance.LoadAreaSelect();
        SelectScene.allowSceneActivation = false;
	}
	

    // シーン遷移
    public void Transition_StageSelect()
    {
        SelectScene.allowSceneActivation = true;
    }
}
