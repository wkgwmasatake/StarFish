using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeBlack : MonoBehaviour {
    private float red, green, blue, alpha;//gameobjectの色を取得するfloat型変数
    private float add_speed;
    // Use this for initialization
    void Start () {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alpha = 0;
        add_speed = 0.03f;
    }

    public IEnumerator FadeIn()
    {
        SelectDirector.Instance.Set_Statemove();

        float size = 0f;
        float speed = add_speed;
        while (size <= 1.0f)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alpha);
            alpha += speed ;
            size += speed;
            yield return null;
        }
        StartCoroutine(FadeOut());        
    }

    public IEnumerator FadeOut()
    {
        float size = 1.0f;
        float speed = add_speed + 0.03f;

        while (size >= 0f)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alpha);
            alpha -= speed ;
            size -= speed;
            yield return null;           
        }
        SelectDirector.Instance.Set_Statemove();

    }
}
