using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectIcon : MonoBehaviour {
    [SerializeField] private Sprite Unlocked;
    [SerializeField] private Sprite Locked;
    [SerializeField] private Sprite Selected;

    private int get_clear_num;//directorからのクリア状況受け取り用
    void Start()
    {
        CheckState();
    }
    //Unlocked = 0, // 選択可能かつ未選択
    //Locked = 1,// 選択不可能
    //Selected = 2,// 選択可能かつ選択中

    private void objSwitch(int num)
    {
        var img = GetComponent<Image>();
        switch (num)
        {
            case 0:
                img.sprite = Unlocked;
                break;
            case 1:
                img.sprite = Locked;
                break;
            case 2:
                img.sprite = Selected;
                break;
        }
    }

    public void CheckState()//エリアのステータスを反映してアイコンを切り替える
    {      
                string obj_name = gameObject.name;
        switch (obj_name)
        {
            case "Icon_1":
                int icon_1 = SelectDirector.Instance.GetStateArea(0, 0);
                objSwitch(icon_1);
                break;
            case "Icon_2":
                int icon_2 = SelectDirector.Instance.GetStateArea(1, 0);
                objSwitch(icon_2);
                break;
            case "Icon_3":
                int icon_3 = SelectDirector.Instance.GetStateArea(2, 0);
                objSwitch(icon_3);
                break;
            case "Icon_4":
                int icon_4 = SelectDirector.Instance.GetStateArea(3, 0);
                objSwitch(icon_4);
                break;
            case "Icon_5":
                int icon_5 = SelectDirector.Instance.GetStateArea(4, 0);
                objSwitch(icon_5);
                break;
        }

    }
}
