using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    enum PHASE
    {
        FADE,
        LOGO,
        READY,
        START,
 
    }

    [SerializeField] AudioSource se_start;
    [SerializeField] AudioSource se_titlebreak;
    [SerializeField] AudioSource se_vibe;
    [SerializeField] AudioSource se_falling;
    [SerializeField] AudioSource bgm_title;

    [SerializeField] Image blackfade1;
    [SerializeField] Image blackfade2;
    [SerializeField] SpriteRenderer titleLogo;
    [SerializeField] SpriteRenderer start_text;

    [SerializeField] GameObject constellation;
    [SerializeField] GameObject big_star;
    [SerializeField] float TitelBreakTime;
    [SerializeField] GameObject[] star_child;
    [SerializeField] GameObject vibe_star;
    [SerializeField] GameObject falling_star;
    [SerializeField] GameObject lost_star;

    [SerializeField] private SceneObject nextScene;

    private PHASE now_phase;
    private float time;

    private bool pawnflg1;
    private bool pawnflg2;
    private bool pawnflg3;
    private bool pawnflg4;

    private float fadetime_logo;
    private float fadetime_logo_out;
    private float fadetime_start;
    private float shaketime;

    private bool startFlg;
    private bool logoFlg;
    private bool fadeFlg;

    private Explodable explodable;

    // Use this for initialization
    void Start () {

        blackfade1.enabled = true;
        blackfade2.enabled = true;

        now_phase = PHASE.FADE;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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
                ChangePhase(PHASE.START);
                startFlg = true;

                Invoke("LoadScene", 3.0f);
                //SceneManager.LoadScene(nextScene);
            }

        }

        switch (now_phase)
        {

            case PHASE.FADE:
                FadeProcess();
                break;

            case PHASE.LOGO:
                LogoProcess();
                break;

            case PHASE.READY:
                ReadyProcess();
                break;

            case PHASE.START:
                StartProcess();
                break;
        }

	}

    private void FadeProcess()
    {
        blackfade1.rectTransform.anchoredPosition =
            new Vector2(blackfade1.rectTransform.anchoredPosition.x, blackfade1.rectTransform.anchoredPosition.y - 100);     // フェード画像のy座標を50下げる

        //Debug.Log(blackfade1.rectTransform.anchoredPosition);

        if (blackfade1.rectTransform.anchoredPosition.y < -5000f)
        {
            ChangePhase(PHASE.LOGO);
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
    

    private void BigStarProcess()
    {
        if (pawnflg1 == false)
        {
            Instantiate(big_star);

            ChangePhase(PHASE.START);
            pawnflg1 = true;
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

        if (pawnflg1 == false)
        {
            explodable = titleLogo.GetComponent<Explodable>();
            var _big_star = Instantiate(big_star);

            pawnflg1 = true;
        }

        if(pawnflg1 == true && pawnflg2 == false) {
            time += Time.deltaTime;
            if(time >= TitelBreakTime)
            {
                explodable.explode();
                ExplosionForce ef = GameObject.Find("Force").GetComponent<ExplosionForce>();
                ef.doExplosion(transform.position);

                se_titlebreak.Play();

                time = 0f;
                pawnflg2 = true;
            }
        }

        if(pawnflg2 == true && pawnflg3 == false)
        {
            for(int i = 0; i < star_child.Length; i++)
            {
                var _vibe_star = Instantiate(vibe_star);
                _vibe_star.transform.position = star_child[i].transform.position;
            }

            Destroy(constellation);

            se_vibe.Play();
            Invoke("SeFalling",1.5f);

            pawnflg3 = true;
        }

        //if(pawnflg3 == true && pawnflg4 == false)
        //{
        //    time = Time.deltaTime;
        //    if (time >= 1.0f)
        //    {
        //        for (int i = 0; i < star_child.Length; i++)
        //        {
        //            var _star_child = Instantiate(falling_star);
        //            _star_child.transform.position = star_child[i].transform.position;

        //            var _lost_star = Instantiate(lost_star);
        //            _lost_star.transform.position = star_child[i].transform.position;
        //        }

                

        //        pawnflg4 = true;
        //    }
        //}

    }


    private void ChangePhase(PHASE p)
    {
        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = pawnflg4 = false;

        time = 0f;
    }


    private void SkipProcess()
    {

        blackfade1.enabled = false;
        blackfade2.enabled = false;

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

    private void SeFalling()
    {
        se_vibe.Stop();
        se_falling.Play();
    }
}

