using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaArrowController : MonoBehaviour
{
    private const int RIGHT_MOVE = 1;
    private const int LEFT_MOVE = -1;

    private GameObject ADirector;

	// Use this for initialization
	void Start ()
    {
        ADirector = GameObject.Find("AreaSelectDirector");
	}

    public void RightArrow()
    {
        ADirector.GetComponent<SecretAreaDirector>().ChangeAreaNunber(RIGHT_MOVE);
    }

    public void LeftArrow()
    {
        ADirector.GetComponent<SecretAreaDirector>().ChangeAreaNunber(LEFT_MOVE);
    }
}
