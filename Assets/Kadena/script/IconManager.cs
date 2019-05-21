using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IconManager : MonoBehaviour {
    [SerializeField] private Sprite Unlocked;
    [SerializeField] private Sprite Locked;
    [SerializeField] private Sprite Selected;

    private int get_clear_num;//directorからのクリア状況受け取り用
    void Start () {
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
        string scene_name = SceneManager.GetActiveScene().name;
        switch (scene_name)
        {
            case "Area_Select":
                string obj_name = gameObject.name;
                switch (obj_name)
                {
                    case "Icon_1":
                        int icon_1 = AreaDirector.Instance.GetStateArea(0, 0);
                        objSwitch(icon_1);
                        break;
                    case "Icon_2":
                        int icon_2 = AreaDirector.Instance.GetStateArea(1, 0);
                        objSwitch(icon_2);
                        break;
                    case "Icon_3":
                        int icon_3 = AreaDirector.Instance.GetStateArea(2, 0);
                        objSwitch(icon_3);
                        break;
                    case "Icon_4":
                        int icon_4 = AreaDirector.Instance.GetStateArea(3, 0);
                        objSwitch(icon_4);
                        break;
                    case "Icon_5":
                        int icon_5 = AreaDirector.Instance.GetStateArea(4, 0);
                        objSwitch(icon_5);
                        break;
                }
                break;
            case "Stage_Select":
                get_clear_num = StageDirector.Instance.GetClearNUM();
                string name = gameObject.name;

                switch (get_clear_num)
                {
                    case 1:
                    case 2:
                    case 3:
                        switch (name)
                        {
                            case "Icon_1":
                                int icon_1 = StageDirector.Instance.GetStateStage(0, 0);
                                objSwitch(icon_1);
                                break;
                            case "Icon_2":
                                int icon_2 = StageDirector.Instance.GetStateStage(1, 0);
                                objSwitch(icon_2);
                                break;
                            case "Icon_3":
                                int icon_3 = StageDirector.Instance.GetStateStage(2, 0);
                                objSwitch(icon_3);
                                break;
                        }
                        break;
                    case 4:
                    case 5:
                    case 6:
                        switch (name)
                        {
                            case "Icon_1":
                                int icon_1 = StageDirector.Instance.GetStateStage(3, 0);
                                objSwitch(icon_1);
                                break;
                            case "Icon_2":
                                int icon_2 = StageDirector.Instance.GetStateStage(4, 0);
                                objSwitch(icon_2);
                                break;
                            case "Icon_3":
                                int icon_3 = StageDirector.Instance.GetStateStage(5, 0);
                                objSwitch(icon_3);
                                break;
                        }
                        break;
                    case 7:
                    case 8:
                    case 9:
                        switch (name)
                        {
                            case "Icon_1":
                                int icon_1 = StageDirector.Instance.GetStateStage(6, 0);
                                objSwitch(icon_1);
                                break;
                            case "Icon_2":
                                int icon_2 = StageDirector.Instance.GetStateStage(7, 0);
                                objSwitch(icon_2);
                                break;
                            case "Icon_3":
                                int icon_3 = StageDirector.Instance.GetStateStage(8, 0);
                                objSwitch(icon_3);
                                break;
                        }
                        break;
                    case 10:
                    case 11:
                    case 12:
                        switch (name)
                        {
                            case "Icon_1":
                                int icon_1 = StageDirector.Instance.GetStateStage(9, 0);
                                objSwitch(icon_1);
                                break;
                            case "Icon_2":
                                int icon_2 = StageDirector.Instance.GetStateStage(10, 0);
                                objSwitch(icon_2);
                                break;
                            case "Icon_3":
                                int icon_3 = StageDirector.Instance.GetStateStage(11, 0);
                                objSwitch(icon_3);
                                break;
                        }
                        break;
                    case 13:
                    case 14:
                    case 15:
                        switch (name)
                        {
                            case "Icon_1":
                                int icon_1 = StageDirector.Instance.GetStateStage(12, 0);
                                objSwitch(icon_1);
                                break;
                            case "Icon_2":
                                int icon_2 = StageDirector.Instance.GetStateStage(13, 0);
                                objSwitch(icon_2);
                                break;
                            case "Icon_3":
                                int icon_3 = StageDirector.Instance.GetStateStage(14, 0);
                                objSwitch(icon_3);
                                break;
                        }
                        break;
                }
                break;
        }
    }
}
