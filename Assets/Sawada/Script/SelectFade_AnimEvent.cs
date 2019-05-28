using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFade_AnimEvent : MonoBehaviour
{

    public void Anim_End()
    {
        Debug.Log("アニメーション終了");

        //　各機能を使えるように
        SelectDirector.Instance.Fade_End();
    }
}