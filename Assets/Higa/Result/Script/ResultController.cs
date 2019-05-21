﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {

    enum PHASE
    {
        FADE,
        STAR,
        CAMERA,

        END,
    }

    [SerializeField] Camera camera;
    [SerializeField] Image whitefade;
    [SerializeField] GameObject rising_star;
    [SerializeField] GameObject fade_star;
    [SerializeField] GameObject shootingStar;
    [SerializeField] GameObject constellation_line;
    [SerializeField] GameObject constellation_image;
    [SerializeField] GameObject[] sheep_stars;
    [SerializeField] GameObject[] cassiopeia_stars;
    [SerializeField] GameObject[] smallbear_stars;
    [SerializeField] GameObject[] balance_stars;
    [SerializeField] GameObject[] crab_stars;
    [SerializeField] int stage_num;
    [SerializeField] int now_clear_stage;


    [SerializeField] AudioSource se_splash;

    [SerializeField] float RisingTime;

    private PHASE now_phase;

    private GameObject _rising_star;
    private ParticleSystem _star_ps;
    private float time;
    private int area_num;
    

    private bool pawnflg1, pawnflg2, pawnflg3;

	// Use this for initialization
	void Start () {

        whitefade.enabled = true;

        //shootingStar.SetActive(false);

        now_phase = PHASE.FADE;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        area_num = 0;
        switch (area_num)
        {
            case 0:
                PawnStarCluster(sheep_stars);
                break;

            case 1:
                PawnStarCluster(cassiopeia_stars);
                break;

            case 2:
                PawnStarCluster(smallbear_stars);
                break;

            case 3:
                PawnStarCluster(balance_stars);
                break;

            case 4:
                PawnStarCluster(crab_stars);
                break;
        }


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

            case PHASE.CAMERA:
                CameraProcess();
                break;
        }

        Debug.Log(now_phase);
	}

    private void FadeProcess()
    {

        whitefade.rectTransform.anchoredPosition =
                    new Vector2(whitefade.rectTransform.anchoredPosition.x, whitefade.rectTransform.anchoredPosition.y - 100);     // フェード画像のy座標を50下げる

        //Debug.Log(whitefade.rectTransform.anchoredPosition);

        if(whitefade.rectTransform.anchoredPosition.y < -2850f)
        {
            //ChangePhase(PHASE.CAMERA);
        }
    }


    private void StarfishProcess()
    {

        if(pawnflg1 == false)
        {

            _rising_star = Instantiate(rising_star);
            //Rigidbody2D rb = _star.GetComponent<Rigidbody2D>();
            //_star_ps = _rising_star.GetComponent<ParticleSystem>();

            //Vector2 force = new Vector2(0f, 45f);
            //rb.AddForce(force, ForceMode2D.Impulse);
            //rb.AddTorque(5f, ForceMode2D.Impulse);

            Invoke("PawnFadeStar", 1f);

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


    private void CameraProcess()
    {
        if(camera.transform.position != new Vector3(0, 0, -10))
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5.0f, Time.deltaTime);
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, 0, -10), Time.deltaTime);
        }
        else
        {
            GameDirector.Instance.ParticleFlg = true;
            ChangePhase(PHASE.END);
        }
    }


    private void ChangePhase(PHASE p)
    {

        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
    }

    private void PawnFadeStar()
    {
        Instantiate(fade_star);
        GameDirector.Instance.ParticleFlg = true;
    }

    private void PawnStarCluster(GameObject[] _stars)
    {
        //for (int i = 0; i < sheep_stars.Length-1; i++)
        //{
        //    sheep_stars[i] = constellation_line.transform.GetChild(i).gameObject;
        //}


        for (int i = 0; i < stage_num; i++)
        {
            if (i != now_clear_stage - 1)
            {
                var _fade_star = Instantiate(fade_star);
                _fade_star.transform.position = _stars[i].transform.position;
                //Destroy(constellation_num[i]);

            }
            else
            {
                var _rising_star = Instantiate(rising_star);
                Vector3 pos = _stars[i].transform.position;
                pos.y -= 4.05f + 0.4165f;   // GrayStarとRisingStarの Y軸 の差

                //_rising_star.transform.position = constellation_num[i].transform.position;
                _rising_star.transform.position = pos;

                //Debug.Log("rising : "+_rising_star.transform.position);
                //Debug.Log("gray : "+constellation_num[i].transform.position);
                //Debug.Log("pos : " + pos);

                StartCoroutine(PawnFadeStar(_stars[i].transform.position, 1.0f));

                Vector3 pos1 = _stars[i].transform.position;
                pos1.z = -10f;
                camera.transform.position = pos1;
                camera.orthographicSize = 0.5f;
            }

        }

    }

    private IEnumerator PawnFadeStar(Vector3 pos, float second)
    {
        yield return new WaitForSeconds(second);

        var _fade_star = Instantiate(fade_star);
        _fade_star.transform.position = pos;

        yield return new WaitForSeconds(second);
        ChangePhase(PHASE.CAMERA);
        GameDirector.Instance.ParticleFlg = true;

        //yield return null;
    }
}
