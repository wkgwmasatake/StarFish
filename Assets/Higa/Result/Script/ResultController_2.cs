using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController_2 : MonoBehaviour {

    enum PHASE
    {
        FADE,
        STAR,

        END,
    }

    [SerializeField] Image whitefade;
    [SerializeField] GameObject star;
    [SerializeField] GameObject ShootingStar;

    [SerializeField] AudioSource se_splash;

    [SerializeField] float RisingTime;

    private PHASE now_phase;

    private GameObject _star;
    private ParticleSystem _star_ps;
    private float time;

    private bool pawnflg1, pawnflg2, pawnflg3;

	// Use this for initialization
	void Start () {

        whitefade.enabled = true;

        ShootingStar.SetActive(false);

        now_phase = PHASE.FADE;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(GameDirector.Instance.GetArmNumber());
        switch (now_phase)
        {
            case PHASE.FADE:
                FadeProcess();
                break;

            case PHASE.STAR:
                StarfishProcess();
                break;

        }


	}

    private void FadeProcess()
    {

        whitefade.rectTransform.anchoredPosition =
                    new Vector2(whitefade.rectTransform.anchoredPosition.x, whitefade.rectTransform.anchoredPosition.y - 100);     // フェード画像のy座標を50下げる

        //Debug.Log(whitefade.rectTransform.anchoredPosition);

        if(whitefade.rectTransform.anchoredPosition.y < -2850f)
        {
            ChangePhase(PHASE.STAR);
        }
    }


    private void StarfishProcess()
    {

        if(pawnflg1 == false)
        {

            _star = Instantiate(star);
            //Rigidbody2D rb = _star.GetComponent<Rigidbody2D>();
            _star_ps = _star.GetComponent<ParticleSystem>();

            //Vector2 force = new Vector2(0f, 45f);
            //rb.AddForce(force, ForceMode2D.Impulse);
            //rb.AddTorque(5f, ForceMode2D.Impulse);

            pawnflg1 = true;

        }

        time += Time.time;
        if (time >= RisingTime)
        {
            if (GameObject.Find("GameDirector") != null)
            {
                GameDirector.Instance.ParticleFlg = true;
                //Debug.Log("finish");
            }
            else
            {
                Debug.Log("Fireworks finished");
            }

            ChangePhase(PHASE.END);
        }
        //Debug.Log(GameDirector.Instance.ParticleFlg);
    }


    private void ChangePhase(PHASE p)
    {

        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
    }
}
