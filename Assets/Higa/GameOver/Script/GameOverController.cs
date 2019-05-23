using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    enum PHASE
    {
        FADE,
        STARFISH,
        TEXT,

        END,
    }

    [SerializeField] Image blackfade1;
    [SerializeField] Image blackfade2;
    [SerializeField] GameObject starfish;
    [SerializeField] SpriteRenderer gameover_text;

    private float time;
    private float fadetime;

    private PHASE now_phase;

    private Animator starfish_anim;

    private bool pawnflg1, pawnflg2, pawnflg3;

	// Use this for initialization
	void Start () {

        blackfade1.enabled = true;
        blackfade2.enabled = true;
        gameover_text.enabled = false;

        time = 0f;
        fadetime = 1f;

        // フェードアウト
        float alpha = 0;
        var color = gameover_text.color;
        color.a = alpha;
        gameover_text.color = color;

        now_phase = PHASE.FADE;

        starfish_anim = starfish.GetComponent<Animator>();

        pawnflg1 = pawnflg2 = pawnflg3 = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        switch (now_phase)
        {
            case PHASE.FADE:
                FadeProcess();
                break;

            //case PHASE.STARFISH:
            case PHASE.TEXT:
                StarfishProcess();
                //break;

            
                TextProcess();
                break;

        }


        //Debug.Log(now_phase);
	}

    private void FadeProcess()
    {
        blackfade1.rectTransform.anchoredPosition =
                    new Vector2(blackfade1.rectTransform.anchoredPosition.x, blackfade1.rectTransform.anchoredPosition.y + 100);     // フェード画像のy座標を50下げる

        //Debug.Log(blackfade1.rectTransform.anchoredPosition);

        if (blackfade1.rectTransform.anchoredPosition.y > 2850f)
        {
            ChangePhase(PHASE.TEXT);
        }
    }

    private void StarfishProcess()
    {
        Debug.Log(starfish_anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (starfish_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            //ChangePhase(PHASE.TEXT);
            GameDirector.Instance.ParticleFlg = true;
            Debug.Log("ParticleFlg : " + GameDirector.Instance.ParticleFlg);
        }
    }

    private void TextProcess()
    {
        if(pawnflg1 == false)
        {
            gameover_text.enabled = true;

            pawnflg1 = true;
        }

        // フェードアウト
        time += Time.deltaTime;
        float alpha = 1 / (60 * fadetime);
        var color = gameover_text.color;
        color.a += alpha;
        gameover_text.color = color;

        //Debug.Log(color.a);

        if(now_phase != PHASE.END && color.a >= 1.0f && GameDirector.Instance.ParticleFlg)
        {
            ChangePhase(PHASE.END);
            
        }
    }

    private void ChangePhase(PHASE p)
    {

        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
    }
}
