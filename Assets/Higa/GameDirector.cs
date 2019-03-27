using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{

    public Text disTex;         // 空までの距離(UI)
    public Text armTex;         // 腕の残り本数(UI)

    public int StageStatus;     // ステージのクリア状況
    public int AreaStatus;      // エリアの制覇状況
    public int PearlStatus;     // 真珠の取得状況


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

        if (armNumber < 6)
        {
            armTex.text = armNumber.ToString();
        }
    }


    public void SetPosition(Vector2 posi)
    {
        position = posi;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public void SetArmNumber(int num)
    {
        armNumber = num;
    }

    public int GetArmNumber()
    {
        return armNumber;
    }

}