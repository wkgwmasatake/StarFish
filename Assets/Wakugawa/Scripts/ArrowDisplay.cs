using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArrowDisplay : MonoBehaviour {

    Animator StarFishAnim;
    bool Flag = false;
    byte AmountFlag = 0;
    float gettime = 0;

    // Use this for initialization
	void Start () {
        StarFishAnim = GameObject.Find("OriginalStarFish").GetComponent<Animator>();    // 海星のアニメーターを取得
	}
	
	// Update is called once per frame
	void Update () {
        if(GameDirector.Instance.GetArmNumber() != 7)
        {
            Destroy(this);      // 最初のタップをしたら自分自身を破棄
        }
        else if(Flag)           // アニメーションが終了したら矢印を表示
        {
            const float FillAmountPlus = 0.015f;

            switch (AmountFlag)
            {
                case 0:
                    this.GetComponent<Image>().fillAmount += FillAmountPlus;
                    if (this.GetComponent<Image>().fillAmount >= 1.0f)
                        AmountFlag = 2;
                    break;

                case 1:
                    this.GetComponent<Image>().fillAmount -= FillAmountPlus;
                    if (this.GetComponent<Image>().fillAmount <= 0.0f)
                        AmountFlag = 2;
                    break;

                case 2:
                    gettime += Time.deltaTime;
                    if (gettime >= 1.0f)
                    {
                        if (this.GetComponent<Image>().fillAmount <= 0.0f)
                            AmountFlag = 0;
                        else
                            AmountFlag = 1;
                        gettime = 0;
                    }
                    break;
            }
        }
    }

    public void ChangeArrowFlag()
    {
        Flag = true;
    }
}
