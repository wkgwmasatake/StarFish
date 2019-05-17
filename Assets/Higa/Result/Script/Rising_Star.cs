using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rising_Star : MonoBehaviour {

    [SerializeField] GameObject TargetPoint;
    [SerializeField] float movetime;
    [SerializeField] float angle;

    private Vector3 targetPos;
    private Vector3 startPos;

    private float now_position;

	// Use this for initialization
	void Start () {

        targetPos = TargetPoint.transform.position;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        //if(transform.position != targetPos)
        //{
        //    float pos_y = transform.position.y;

        //    transform.position = new Vector3(0, pos_y + 1 / (60 * movetime), 0);
        //}

        now_position += 1 / (60 * movetime);
        transform.position = Vector3.Lerp(startPos, targetPos, now_position);



    }
}
