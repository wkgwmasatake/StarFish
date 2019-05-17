using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    enum PHASE
    {

        LOGO,
        READY,
        CAMERA,
        START,
 
    }

    [SerializeField] AudioSource se_start;
    [SerializeField] AudioSource bgm_title;

    [SerializeField] SpriteRenderer titleLogo;
    [SerializeField] SpriteRenderer start_text;

    [SerializeField] GameObject constellation;
    [SerializeField] GameObject[] star_child;
    [SerializeField] GameObject falling_star;
    [SerializeField] GameObject lost_star;

    [SerializeField] private SceneObject nextScene;

    private PHASE now_phase;
    private float time;

    private bool pawnflg1;
    private bool pawnflg2;
    private bool pawnflg3;

    private float fadetime_logo;
    private float fadetime_logo_out;
    private float fadetime_start;
    private float shaketime;

    private bool startFlg;
    private bool logoFlg;
    private bool fadeFlg;

    // Use this for initialization
    void Start () {

        now_phase = PHASE.LOGO;
        time = 0f;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        fadetime_logo = 2.0f;
        fadetime_logo_out = 0.5f;
        fadetime_start = 1.0f;
        shaketime = 0.5f;


        // フェードアウト
        float alpha = 0;
        var color = titleLogo.color;
        color.a = alpha;
        titleLogo.color = color;
        start_text.color = color;
        

        startFlg = false;
        logoFlg = false;

        for(int i = 0; i < star_child.Length; i++)
        {
            star_child[i] = constellation.transform.GetChild(i).gameObject;
        }

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
                ChangePhase(PHASE.CAMERA);
                startFlg = true;

                Invoke("LoadScene", 2.0f);
                //SceneManager.LoadScene(nextScene);
            }

        }

        switch (now_phase)
        {

            case PHASE.LOGO:
                LogoProcess();
                break;

            case PHASE.READY:
                ReadyProcess();
                break;

            case PHASE.CAMERA:
                CameraProcess();
                break;

            case PHASE.START:
                StartProcess();
                break;
        }

	}


    private void LogoProcess()
    {

            // フェードアウト
            float alpha = 1 / (60 * fadetime_logo);
            var color = titleLogo.color;
            color.a += alpha;
            titleLogo.color = color;

            if (logoFlg == false && color.a >= 1.0f)
            {
                logoFlg = true;
                ChangePhase(PHASE.READY);
            }

        
    }
    

    private void ReadyProcess()
    {
        if (pawnflg1 == false)
        {
            bgm_title.Play();
            //Debug.Log("bgm_play");
            pawnflg1 = true;
        }

        // フェードイン・アウト
        float alpha = 1 / (60 * fadetime_start);
        var color = start_text.color;
        if (!fadeFlg)
        {
            color.a += alpha;
            start_text.color = color;
        }
        else
        {
            color.a -= alpha;
            start_text.color = color;
        }

        if(color.a >= 1)
        {
            fadeFlg = true;
        }
        else if(color.a <= 0)
        {
            fadeFlg = false;
        }

    }


    private void CameraProcess()
    {
        if(pawnflg1 == false)
        {
            CameraShake cs = GameObject.Find("Camera").GetComponent<CameraShake>();
            cs.Shake(shaketime, 0.05f);
            //Invoke(ChangePhase(PHASE.START),0.5f);
            pawnflg1 = true;
        }

        time += Time.deltaTime;
        if (time >= shaketime)
        {
            ChangePhase(PHASE.START);
        }
        //Debug.Log(time);
    }


    private void StartProcess()
    {

        

        // フェードアウト
        float alpha = 1 / (60 * fadetime_logo_out);
        var color = titleLogo.color;
        if(color.a > 0)
        {
            color.a -= alpha;
            titleLogo.color = color;
            bgm_title.volume = alpha;
        }
        

        if (pawnflg1 == false)
        {
            Destroy(constellation);
            pawnflg1 = true;
        }

        if(pawnflg2 == false)
        {
            for(int i = 0; i < star_child.Length; i++)
            {
                var _star_child = Instantiate(falling_star);
                _star_child.transform.position = star_child[i].transform.position;

                var _lost_star = Instantiate(lost_star);
                _lost_star.transform.position = star_child[i].transform.position;
            }

            pawnflg2 = true;
        }


    }


    private void ChangePhase(PHASE p)
    {
        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        time = 0f;
    }


    private void SkipProcess()
    {

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

