using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartStage : MonoBehaviour
{
    private string stageName;
    
	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = StageName_Configuration();
        GetComponent<Animation>().Play();
	}
	
    string StageName_Configuration()
    {
        stageName = GameDirector.Instance.GetSceneName;
        return stageName;
    }
}
