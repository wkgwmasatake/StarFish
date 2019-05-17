using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMove : MonoBehaviour
{
    [SerializeField] bool Title;
    [SerializeField] bool Background_Sky;
    [SerializeField] float speed;
    [SerializeField] float ReStartPosX;     //リスタートする位置(２つ目のの道路の初めの位置)
    [SerializeField] float DeletePosX;      //消すＺ座標
    private Vector3 bufPos;
    private float PosXdiff;                 //所定の位置と実際に消えた位置の差分

    // Use this for initialization
    void Start()
    {
        if (!Title)
        {
            float goal_y = GameObject.Find("GoalLine").gameObject.transform.position.y;
            Vector3 pos = new Vector3(0, goal_y, 0);
            this.gameObject.transform.position = pos;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!Background_Sky)
        {
            if (!GameDirector.Instance.GetPauseFlg)
            {
                this.transform.position += new Vector3(speed, 0, 0);
            }


            if (this.transform.position.x > DeletePosX)
            {
                PosXdiff = this.transform.position.x - DeletePosX;
                bufPos = new Vector3(ReStartPosX + PosXdiff, this.transform.position.y, this.transform.position.z);
                this.transform.position = bufPos;
            }
        }
        

    }
}