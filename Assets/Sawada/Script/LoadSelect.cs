using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelect : MonoBehaviour
{

    [SerializeField] private GameObject blackfade;
    [SerializeField] private float VolumeDown;   // 下げる音量
    
    private AsyncOperation SelectScene;
    private AudioSource BGM;

	// Use this for initialization
	void Start ()
    {
        SelectScene = GameDirector.Instance.LoadAreaSelect();
        SelectScene.allowSceneActivation = false;

        BGM = Camera.main.GetComponent<AudioSource>();
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

    // コルーチン
    IEnumerator Transition()
    {
        while (BGM.volume > 0)
        {
            BGM.volume -= VolumeDown;
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
        Transition_StageSelect();
    }
}
