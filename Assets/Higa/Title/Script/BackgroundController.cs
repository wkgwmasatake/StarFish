using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    void Update()
    {
        float scrollX = Mathf.Repeat(Time.time * 0.03f, 1);
        float scrollY = Mathf.Repeat(Time.time * 0.06f, 1);
        Vector2 offset = new Vector2(scrollX, scrollY);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}