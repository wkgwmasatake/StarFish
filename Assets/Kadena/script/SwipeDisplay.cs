using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDisplay : MonoBehaviour {
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    [SerializeField] private GameObject leftarrow;
    [SerializeField] private GameObject rightarrow;
    string Direction;

    void Update()
    {
        Flick();        
    }
    void Flick()
    {
        if(SelectDirector.Instance.Get_Statezoom() == true) { return; }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                                      Input.mousePosition.y,
                                      Input.mousePosition.z);
            GetDirection();
        }
    }
    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }

        }
        else
        {
            //タッチを検出
            Direction = "touch";
        }
    
        switch (Direction)
        {
            case "right":
                //右フリックされた時の処理
                leftarrow.GetComponent<ArrowClick>().Left_Onclick();

                Debug.Log("right");
                break;

            case "left":
                //左フリックされた時の処理
                rightarrow.GetComponent<ArrowClick>().Right_Onclick();
                Debug.Log("left");
                break;
        }
    }
}
