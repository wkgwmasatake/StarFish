using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    enum PHASE
    {
        FADE,
        STARFISH,
        TEXT,

        END,
    }

    [SerializeField] Image blackfade;
    [SerializeField] GameObject starfish;

    private PHASE now_phase;

    private bool pawnflg1, pawnflg2, pawnflg3;

	// Use this for initialization
	void Start () {

        blackfade.enabled = true;

        now_phase = PHASE.FADE;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
	}
	
	// Update is called once per frame
	void Update () {

        switch (now_phase)
        {
            case PHASE.FADE:
                FadeProcess();
                break;

            case PHASE.STARFISH:

                break;

            case PHASE.TEXT:

                break;

        }

	}

    private void FadeProcess()
    {
        blackfade.rectTransform.anchoredPosition =
                    new Vector2(blackfade.rectTransform.anchoredPosition.x, blackfade.rectTransform.anchoredPosition.y + 50);     // フェード画像のy座標を50下げる

        Debug.Log(blackfade.rectTransform.anchoredPosition);

        if (blackfade.rectTransform.anchoredPosition.y > 2850f)
        {
            ChangePhase(PHASE.STARFISH);
        }
    }

    private void StarfishProcess()
    {

    }

    private void TextProcess()
    {

    }

    private void ChangePhase(PHASE p)
    {

        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
    }
}
