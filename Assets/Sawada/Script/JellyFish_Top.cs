using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFish_Top : MonoBehaviour
{
    private bool _TopFlg = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Flg = true;
        }
    }

    public bool Flg
    {
        get { return _TopFlg; }
        set { _TopFlg = value; }
    }

    public Vector2 GetDistance()
    {
        Vector2 distance;

        distance.x = this.transform.position.x - this.transform.parent.gameObject.transform.position.x;
        distance.y = this.transform.position.y - this.transform.parent.gameObject.transform.position.y;

        return distance;
    }

}
