using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {

    enum PHASE
    {
        FADE,
        STARFISH,
        FIREWORKS,
        BLESSING,

        END,
    }

    const byte _MAX_LEG = 5;        // 腕の最大数

    [SerializeField] Image whitefade;
    [SerializeField] GameObject starfish;
    [SerializeField] Sprite LegImages;
    [SerializeField] GameObject waterdrop;
    [SerializeField] GameObject beStar;
    [SerializeField] GameObject fireworks_L;
    [SerializeField] GameObject fireworks_M;
    [SerializeField] GameObject fireworks_S;
    [SerializeField] GameObject ShootingStar;

    [SerializeField] AudioSource se_splash;

    private PHASE now_phase;

    private GameObject _starfish;
    private float bufposY;

    SpriteRenderer[] LegSpriteRenderer; // 腕のスプライトレンダラー
    

    private bool pawnflg1, pawnflg2, pawnflg3;

	// Use this for initialization
	void Start () {

        whitefade.enabled = true;

        LegSpriteRenderer = new SpriteRenderer[_MAX_LEG];       // 腕の本数分配列を確保

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


            case PHASE.STARFISH:
                StarfishProcess();
                break;

            case PHASE.FIREWORKS:
                FireworksProcess();
                break;

            case PHASE.BLESSING:
                BlessingProcess();
                break;

        }


	}

    private void FadeProcess()
    {

        whitefade.rectTransform.anchoredPosition =
                    new Vector2(whitefade.rectTransform.anchoredPosition.x, whitefade.rectTransform.anchoredPosition.y - 50);     // フェード画像のy座標を50下げる

        //Debug.Log(whitefade.rectTransform.anchoredPosition);

        if(whitefade.rectTransform.anchoredPosition.y < -2850f)
        {
            ChangePhase(PHASE.STARFISH);
        }
    }


    private void StarfishProcess()
    {

        if(pawnflg1 == false)
        {
            //GameDirector.Instance.SetArmNumber(1);

            _starfish = Instantiate(starfish);
            Rigidbody2D rb = _starfish.GetComponent<Rigidbody2D>();

            for (int i = 0; i < _MAX_LEG; i++)                       // それぞれの腕のスプライトレンダラーを取得
            {
                LegSpriteRenderer[i] = _starfish.transform.GetChild(i).GetComponent<SpriteRenderer>();
            }
            for (int i = _MAX_LEG-1; i > GameDirector.Instance.GetArmNumber()-2; i--)
            {
                LegSpriteRenderer[i].sprite = LegImages;
            }

            Vector2 force = new Vector2(0f, 15f);
            rb.AddForce(force, ForceMode2D.Impulse);
            rb.AddTorque(5f, ForceMode2D.Impulse);



            pawnflg1 = true;

        }

        if (pawnflg2 == false)
        {

            for (float i = 1.5f; i <= 2.5f; i += 0.2f)
            {
                var _waterdrop = Instantiate(waterdrop);
                Rigidbody2D rb = _waterdrop.GetComponent<Rigidbody2D>();

                Vector2 force = new Vector2(i, 3f * Random.Range(0.8f, 1.2f));
                rb.AddForce(force, ForceMode2D.Impulse);
                rb.AddTorque(-(Mathf.Abs(i) + 3f), ForceMode2D.Impulse);
            }
            for (float i = -1.5f; i >= -2.5f; i -= 0.2f)
            {
                var _waterdrop = Instantiate(waterdrop);
                Rigidbody2D rb = _waterdrop.GetComponent<Rigidbody2D>();

                Vector2 force = new Vector2(i, 3f * Random.Range(0.8f, 1.2f));
                rb.AddForce(force, ForceMode2D.Impulse);
                rb.AddTorque(Mathf.Abs(i) + 3f, ForceMode2D.Impulse);
            }

            se_splash.Play();


            pawnflg2 = true;

            ChangePhase(PHASE.FIREWORKS);
        }

    }


    private void FireworksProcess()
    {

        if (bufposY <= _starfish.transform.position.y)       // 最大高度まで上がったらヒトデを消す
        {
            bufposY = _starfish.transform.position.y;
            //Debug.Log(bufposY);
        }
        else
        {
            if (pawnflg1 == false)
            {

                var _beStar = Instantiate(beStar);
                var star_pos = _starfish.transform.position;
                _beStar.transform.position = star_pos;

                int armNum = GameDirector.Instance.GetArmNumber();
                Debug.Log(armNum);
                if (armNum == 1)
                {
                    var effect = Instantiate(fireworks_S);
                    var pos = _starfish.transform.position;
                    effect.transform.position = pos;
                }
                else if(armNum == 2)
                {
                    var effect = Instantiate(fireworks_M);
                    var pos = _starfish.transform.position;
                    effect.transform.position = pos;
                }
                else
                {
                    var effect = Instantiate(fireworks_L);
                    var pos = _starfish.transform.position;
                    effect.transform.position = pos;
                }

                

                Destroy(_starfish);

                pawnflg1 = true;

                ChangePhase(PHASE.BLESSING);
            }
        }

    }


    private void BlessingProcess()
    {

        if(pawnflg1 == false)
        {
            ShootingStar.SetActive(true);

            pawnflg1 = true;
        }

    }


    private void ChangePhase(PHASE p)
    {

        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
    }
}
