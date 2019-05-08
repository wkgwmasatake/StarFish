using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishScript : MonoBehaviour {

    public Vector2 GetDistance()
    {
        Vector2 distance;

        distance.x = this.transform.position.x - this.transform.parent.gameObject.transform.position.x;
        distance.y = this.transform.position.y - this.transform.parent.gameObject.transform.position.y;

        Debug.Log(transform.parent.gameObject.transform.position.y + ":: pY");
        Debug.Log(this.transform.position.y + ":: cY");
        return distance;
    }
}
