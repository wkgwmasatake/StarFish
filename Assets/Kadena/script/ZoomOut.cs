using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour {
    private AudioSource SE_Taped;

    void Start()
    {
        SE_Taped = GetComponent<AudioSource>();
    }

    public void Click_ZoomOut()
    {
        bool flg = SelectDirector.Instance.Get_Statezoom();

        Debug.Log("おされた");

        if (flg == true)
        {
            //SE_Taped.Play();//効果音再生
            SelectDirector.Instance.Set_Statezoom();
            GameObject obj;
            int num = SelectDirector.Instance.GetNumArea();
            switch (num)
            {
                case 0:
                    obj = GameObject.Find("Area_1");
                    obj.GetComponent<SelectArea>().ButtonArea();
                    break;
                case 1:
                    obj = GameObject.Find("Area_2");
                    obj.GetComponent<SelectArea>().ButtonArea();
                    break;
                case 2:
                    obj = GameObject.Find("Area_3");
                    obj.GetComponent<SelectArea>().ButtonArea();
                    break;
                case 3:
                    obj = GameObject.Find("Area_4");
                    obj.GetComponent<SelectArea>().ButtonArea();
                    break;
                case 4:
                    obj = GameObject.Find("Area_5");
                    obj.GetComponent<SelectArea>().ButtonArea();
                    break;
            }
        }
    }
}
