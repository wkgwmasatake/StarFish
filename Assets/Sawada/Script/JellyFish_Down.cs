using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish_Down : MonoBehaviour
{
    private bool _DownFlg = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Flg = true;
            Debug.Log(Flg);
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        Flg = false;
    //        Debug.Log(Flg);
    //    }
    //}

    public bool Flg
    {
        get { return _DownFlg; }
        set { _DownFlg = value; }
    }
}
