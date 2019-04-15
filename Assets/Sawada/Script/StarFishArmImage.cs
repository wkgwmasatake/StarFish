using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarFishArmImage : MonoBehaviour {

    [SerializeField] private int ArmNumber;
    [SerializeField] private int StartArmNum;

    [SerializeField] private Image[] LegRender;
    [SerializeField] private Sprite legImage;

    private const int _MAX_LEG = 5;

	// Use this for initialization
	void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        ArmNumber = GameDirector.Instance.GetArmNumber();
        if (ArmNumber == StartArmNum && ArmNumber != 0 && StartArmNum != 0)
        {
            StartArmNum--;
            if(StartArmNum >= 0 && LegRender[StartArmNum] != null) LegRender[StartArmNum].sprite = legImage;
        }
	}
}
