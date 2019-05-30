using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit_Button : MonoBehaviour {
    private bool flg_credit = false;
    [SerializeField] private GameObject credit;

    public void ButtonCredit()
    {
        flg_credit = !flg_credit;
        if (flg_credit == true)
        {
            credit.SetActive(true);
        }
        else
        {
            credit.SetActive(false);
        }

    }

}
