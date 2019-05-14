using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] audio;
    private AudioSource SE;
    private Animator anim;

    private JellyFish_Top Top;
    private JellyFish_Down Down;

    // Use this for initialization
    void Start()
    {
        SE = GetComponent<AudioSource>();
        anim = transform.root.gameObject.GetComponent<Animator>();

        Top = this.transform.GetChild(0).gameObject.GetComponent<JellyFish_Top>();
        Down = this.transform.GetChild(1).gameObject.GetComponent<JellyFish_Down>();

        Debug.Log(Top.gameObject.name);
        Debug.Log(Down.gameObject.name);
    }

    void Update()
    {
        // プレイヤーがTopに当たったら
        if(Top.Flg && !Down.Flg)
        {
            Top.Flg = false;
            anim.SetTrigger("JumpTrigger");
            SE.clip = audio[0];
            SE.Play();
        }

        // プレイヤーがDownに当たったら
        if(Down.Flg)
        {
            Down.Flg = false;
            SE.clip = audio[1];
            SE.Play();
        }
    }
}
