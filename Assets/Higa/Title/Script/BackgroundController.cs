using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    void Update()
    {
        float scroll = Mathf.Repeat(Time.time * 0.01f, 1);
        Vector2 offset = new Vector2(0, scroll);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}