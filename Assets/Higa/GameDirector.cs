using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public Text disTex;         // 空までの距離(UI)
    public Text armTex;         // 腕の残り本数(UI)

    private Vector2 position;
    private int armNumber;

    public TestPlayer tp;
    private float goal = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        disTex.text = (goal - Input.mousePosition.y).ToString();
        armTex.text = armNumber.ToString();

	}


    public void setPosition(Vector2 posi)
    {
        position = posi;
    }

    public Vector2 getPosition()
    {
        return position;
    }

    public void setArmNumber(int num)
    {
        armNumber = num;
    }

    public int getArmNumber()
    {
        return armNumber;
    }

}