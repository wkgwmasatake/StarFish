using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelect : MonoBehaviour
{

    [SerializeField] private GameObject blackfade;

    private AsyncOperation SelectScene;

	// Use this for initialization
	void Start ()
    {
        SelectScene = GameDirector.Instance.LoadAreaSelect();
        SelectScene.allowSceneActivation = false;
	}

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject obj = Instantiate(blackfade) as GameObject;
            obj.transform.parent = GameObject.Find("FadePoint").transform;

            StartCoroutine("Transition");
        }
    }

    // シーン遷移
    public void Transition_StageSelect()
    {
        SelectScene.allowSceneActivation = true;
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(2.0f);
        Transition_StageSelect();
    }
}
