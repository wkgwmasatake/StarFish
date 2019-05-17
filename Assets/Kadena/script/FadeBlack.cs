using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeBlack : MonoBehaviour {
    private float red, green, blue, alpha;//gameobjectの色を取得するfloat型変数

    // Use this for initialization
    void Start () {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alpha = 0;
    }

    public IEnumerator FadeIn()
    {
        float size = 0f;
        float speed = 0.01f;
        while (size <= 1.0f)
        {
            
            GetComponent<Image>().color = new Color(red, green, blue, alpha);
            alpha += speed ;
            //size += speed;
            //yield return null;
            if(alpha >= 1.0f)
            {
                StartCoroutine(FadeOut());
                yield break;
            }
        }
        
    }
    public IEnumerator FadeOut()
    {
        float size = 1.0f;
        float speed = 0.01f;

        while (size >= 0f)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alpha);
            alpha -= speed ;
            //size -= speed;
            //yield return null;
            if (alpha <= 0f) { yield break; }

        }
    }
}
