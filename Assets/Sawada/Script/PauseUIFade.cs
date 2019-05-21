using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIFade : MonoBehaviour
{
    private CanvasGroup canvas;
    private float UpDown = 0.05f;

	// Use this for initialization
	void Start ()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        StartCoroutine("FadeStart");
	}

    IEnumerator FadeStart()
    {
        while (canvas.alpha < 1)
        {
            canvas.alpha += UpDown;
            yield return null;
        }
    }
}
