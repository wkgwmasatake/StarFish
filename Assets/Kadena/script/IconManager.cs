using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour {
    [SerializeField] private Sprite Unlocked;
    [SerializeField] private Sprite Locked;
    [SerializeField] private Sprite Selected;
    private SpriteRenderer icon_sprite;
    
    //Unlocked = 0, // 選択可能かつ未選択
    //Locked = 1,// 選択不可能
    //Selected = 2,// 選択可能かつ選択中
    
    // Use this for initialization
    void Start () {
        icon_sprite = gameObject.GetComponent<SpriteRenderer>();
        CheckState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void CheckState()//エリアのステータスを反映してアイコンを切り替える
    {
        if (gameObject.name == "Icon_1")
        {
            int icon_1 = AreaDirector.Instance.GetStateArea(0);
            switch (icon_1)
            {
                case 0:
                    icon_sprite.sprite = Unlocked;
                    break;
                case 1:
                    icon_sprite.sprite = Locked;
                    break;
                case 2:
                    icon_sprite.sprite = Selected;
                    break;
            }
        }
        else if (gameObject.name == "Icon_2")
        {
            int icon_2 = AreaDirector.Instance.GetStateArea(1);
            switch (icon_2)
            {
                case 0:
                    icon_sprite.sprite = Unlocked;
                    break;
                case 1:
                    icon_sprite.sprite = Locked;
                    break;
                case 2:
                    icon_sprite.sprite = Selected;
                    break;
            }
        }
        else if (gameObject.name == "Icon_3")
        {
            int icon_3 = AreaDirector.Instance.GetStateArea(2);
            switch (icon_3)
            {
                case 0:
                    icon_sprite.sprite = Unlocked;
                    break;
                case 1:
                    icon_sprite.sprite = Locked;
                    break;
                case 2:
                    icon_sprite.sprite = Selected;
                    break;
            }
        }
    }
}
