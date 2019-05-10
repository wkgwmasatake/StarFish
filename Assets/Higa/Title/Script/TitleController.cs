﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    enum PHASE
    {
        BUBBLE,
        RISING,
        FADE,
        STARFISH,
        FIREWORKS,

        END,
        READY,
    }

    [SerializeField] GameObject _cam1;
    [SerializeField] GameObject _cam2;

    [SerializeField] GameObject bubble1;
    [SerializeField] GameObject bubble2;
    [SerializeField] GameObject bubble3;

    [SerializeField] GameObject _speed_effect;

    [SerializeField] GameObject whiteback;

    [SerializeField] GameObject starfish;
    [SerializeField] GameObject waterdrop;

    [SerializeField] GameObject cloud1;
    [SerializeField] GameObject cloud2;
    [SerializeField] GameObject cloud3;

    [SerializeField] GameObject fireworks;

    [SerializeField] AudioSource se_bubble;
    [SerializeField] AudioSource se_rising;
    [SerializeField] AudioSource se_splash;
    [SerializeField] AudioSource se_start;
    [SerializeField] AudioSource bgm_title;

    [SerializeField] SpriteRenderer titleLogo;

    [SerializeField] private SceneObject nextScene;


    private PHASE now_phase;
    private float speed;
    private float time;

    private bool pawnflg1;
    private bool pawnflg2;
    private bool pawnflg3;
    private bool pawnflg4;

    private float fadetime;

    private float currentRemainTime;
    private SpriteRenderer spRenderer;

    Vector3 startPos;

    private GameObject _starfish;
    private float bufposY;

    private bool startFlg;
    private bool logoFlg;

    // Use this for initialization
    void Start () {

        startPos = _cam1.transform.position;
        _cam1.SetActive(true);
        _cam2.SetActive(false);

        now_phase = PHASE.BUBBLE;
        speed = 0f;
        time = 0f;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        fadetime = 0.5f;
        currentRemainTime = fadetime;
        spRenderer = whiteback.GetComponent<SpriteRenderer>();

        bufposY = -6f;

        // フェードアウト
        float alpha = 0;
        var color = titleLogo.color;
        color.a = alpha;
        titleLogo.color = color;

        startFlg = false;
        logoFlg = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            if (now_phase < PHASE.READY)
            {
                SkipProcess();
                ChangePhase(PHASE.READY);
            }
            else if (startFlg == false && logoFlg == true)
            {
                se_start.Play();
                startFlg = true;

                Invoke("LoadScene", 1.5f);
                //SceneManager.LoadScene(nextScene);
            }

        }

        switch (now_phase)
        {
            case PHASE.BUBBLE:
                BubbleProcess();
                break;

            case PHASE.RISING:
                RisingProcess();
                break;

            case PHASE.FADE:
                FadeProcess();
                break;

            case PHASE.STARFISH:
                StarfishProcess();
                break;

            case PHASE.FIREWORKS:
                FireWorksProcess();
                break;

            case PHASE.END:
                EndProcess();
                break;
        }

	}


    private void BubbleProcess()
    {
        time += Time.deltaTime;

        se_bubble.volume -= 0.01f;

        if (pawnflg1 == false)
        {
            Instantiate(bubble1);
            pawnflg1 = true;
        }
        if(pawnflg2 == false && time > 0.2f)
        {
            Instantiate(bubble2);
            pawnflg2 = true;
            time = 0f;
        }
        if(pawnflg3 == false && time > 0.2f)
        {
            Instantiate(bubble3);
            pawnflg3 = true;
            time = 0f;
        }

        if(pawnflg1 && pawnflg2 && pawnflg3 && time > 1.5f)
        {
            ChangePhase(PHASE.RISING);
            time = 0f;
        }

        
    }


    private void RisingProcess()
    {
        if(pawnflg1 == false)
        {
            var effect = Instantiate(_speed_effect, _cam1.transform);
            effect.transform.parent = _cam1.transform;
            var pos = effect.transform.position;

            pos.y = 10f;
            pos.z = 10f;
            effect.transform.position = pos;

            se_rising.Play();

            pawnflg1 = true;
        }

        speed += 0.005f;

        if (_cam1.transform.position.y + speed < 30)
        {

            Vector3 pos = _cam1.transform.position;

            pos.y += speed;

            _cam1.transform.position = pos;

            //Debug.Log(se_bubble.pitch);
        }
        else
        {
            _cam1.gameObject.SetActive(!_cam1.gameObject.activeSelf);
            _cam2.gameObject.SetActive(!_cam2.gameObject.activeSelf);

            whiteback.gameObject.SetActive(true);

            se_bubble.Stop();
            se_rising.Stop();

            ChangePhase(PHASE.FADE);

        }

    }


    private void FadeProcess()
    {

        // 残り時間を更新
        currentRemainTime -= Time.deltaTime;

        if (currentRemainTime <= 0f)
        {
            ChangePhase(PHASE.STARFISH);
            whiteback.SetActive(false);
        }

        // フェードアウト
        float alpha = currentRemainTime / fadetime;
        var color = spRenderer.color;
        color.a = alpha;
        spRenderer.color = color;

        //Debug.Log(color.a);
    }


    private void StarfishProcess()
    {

        if (pawnflg1 == false)
        {
            _starfish = Instantiate(starfish);
            Rigidbody2D rb = _starfish.GetComponent<Rigidbody2D>();

            Vector2 force = new Vector2(0f, 30f);
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

                Vector2 force = new Vector2( i , 5f * Random.Range(0.8f, 1.2f));
                rb.AddForce(force, ForceMode2D.Impulse);
                rb.AddTorque( -i , ForceMode2D.Impulse);
            }
            for (float i = -1.5f; i >= -2.5f; i -= 0.2f)
            {
                var _waterdrop = Instantiate(waterdrop);
                Rigidbody2D rb = _waterdrop.GetComponent<Rigidbody2D>();

                Vector2 force = new Vector2( i , 5f * Random.Range(0.8f, 1.2f));
                rb.AddForce(force, ForceMode2D.Impulse);
                rb.AddTorque( -i , ForceMode2D.Impulse);
            }

            se_splash.Play();


            pawnflg2 = true;
        }

        ChangePhase(PHASE.FIREWORKS);
    }


    private void FireWorksProcess()
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
                var effect = Instantiate(fireworks);
                var pos = _starfish.transform.position;
                effect.transform.position = pos;

                Destroy(_starfish);

                pawnflg1 = true;

                ChangePhase(PHASE.END);
            }
        }
    }


    private void EndProcess()
    {

        if(GameDirector.Instance.ParticleFlg == true)
        {
            if(pawnflg1 == false)
            {
                bgm_title.Play();
                //Debug.Log("bgm_play");
                pawnflg1 = true;
            }

            // フェードアウト
            time += Time.deltaTime;
            float alpha = 1 / (60 * fadetime);
            var color = titleLogo.color;
            color.a += alpha;
            titleLogo.color = color;

            if (logoFlg == false && color.a >= 1.0f)
            {
                logoFlg = true;
                ChangePhase(PHASE.READY);
            }
        }

        
    }
    

    private void ChangePhase(PHASE p)
    {
        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

    }


    private void SkipProcess()
    {
        se_bubble.Stop();
        se_rising.Stop();

        if(_cam2.gameObject.activeSelf == false)
        {
            _cam1.gameObject.SetActive(!_cam1.gameObject.activeSelf);
            _cam2.gameObject.SetActive(!_cam2.gameObject.activeSelf);
        }

        whiteback.gameObject.SetActive(false);

        if (_starfish != null)
        {
            Destroy(_starfish);
        }

        //while (GameObject.Find("water_drop(Clone)") != null)
        //{
        //    //Destroy(GameObject.Find("water_drop(Clone)"));
        //    Debug.Log("Delete");
        //}

        se_splash.Stop();

        if (GameObject.Find("Fireworks_parent(Clone)"))
        {
            Destroy(GameObject.Find("Fireworks_parent(Clone)"));
        }

        var color = titleLogo.color;
        color.a = 1;
        titleLogo.color = color;

        logoFlg = true;

        bgm_title.Play();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }

}
