using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

    enum PHASE
    {
        START,

        BUBBLE,
        RISING,
        SPLASH,
        STARFISH,
        FIREWORKS,

        END,
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

    [SerializeField] private SceneObject nextScene;


    private PHASE now_phase;
    private float speed;
    private float time;

    private bool pawnflg1;
    private bool pawnflg2;
    private bool pawnflg3;

    private float fadetime;
    private float currentRemainTime;
    private SpriteRenderer spRenderer;

    Vector3 startPos;

    private bool moveflg1;
    private bool moveflg2;
    private bool moveflg3;

    private GameObject _starfish;
    private float bufposY;

    private bool startFlg;

    // Use this for initialization
    void Start () {

        startPos = _cam1.transform.position;
        _cam1.SetActive(true);
        _cam2.SetActive(false);
        
        //bubble1.SetActive(false);
        //bubble2.SetActive(false);
        //bubble3.SetActive(false);

        now_phase = PHASE.BUBBLE;
        speed = 0f;
        time = 0f;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        fadetime = 0.5f;
        currentRemainTime = fadetime;
        spRenderer = whiteback.GetComponent<SpriteRenderer>();

        moveflg1 = moveflg2 = moveflg3 = false;

        bufposY = -6f;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            if(now_phase < PHASE.SPLASH)
            {
                SkipProcess();
                ChangePhase(PHASE.SPLASH);
            }
            else if(startFlg == false)
            {
                se_start.Play();
                startFlg = true;

                Invoke("LoadScene", 1.5f);
                //SceneManager.LoadScene(nextScene);
            }

        }

        switch (now_phase)
        {
            case PHASE.START:
                StartProcess();
                break;

            case PHASE.BUBBLE:
                BubbleProcess();
                break;

            case PHASE.RISING:
                RisingProcess();
                break;

            case PHASE.SPLASH:
                SplashProcess();
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

    private void StartProcess()
    {

        se_bubble.Play();
        se_bubble.pitch = 1f;

    }


    private void BubbleProcess()
    {
        time += Time.deltaTime;

        if(pawnflg1 == false)
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

        se_bubble.volume -= 0.01f;
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

            //se_bubble.pitch += speed / 3;

            //Debug.Log(se_bubble.pitch);
        }
        else
        {
            _cam1.gameObject.SetActive(!_cam1.gameObject.activeSelf);
            _cam2.gameObject.SetActive(!_cam2.gameObject.activeSelf);

            whiteback.gameObject.SetActive(true);

            se_bubble.Stop();

            ChangePhase(PHASE.SPLASH);

        }

    }


    private void SplashProcess()
    {
        if (pawnflg1 == false)
        {
            Instantiate(cloud1);
            Instantiate(cloud2);
            Instantiate(cloud3);

            pawnflg1 = true;

            Debug.Log(pawnflg1);
        }

        // 残り時間を更新
        currentRemainTime -= Time.deltaTime;

        if (currentRemainTime <= 0f)
        {
            //GameObject.Destroy(gameObject);
            ChangePhase(PHASE.STARFISH);
            
        }

        // フェードアウト
        float alpha = currentRemainTime / fadetime;
        var color = spRenderer.color;
        color.a = alpha;
        spRenderer.color = color;
    }


    private void StarfishProcess()
    {

        if (moveflg1 == false)
        {
            _starfish = Instantiate(starfish);
            Rigidbody2D rb = _starfish.GetComponent<Rigidbody2D>();

            Vector2 force = new Vector2(0f, 30f);
            rb.AddForce(force, ForceMode2D.Impulse);
            rb.AddTorque(5f, ForceMode2D.Impulse);

            moveflg1 = true;
        }

        if (moveflg2 == false)
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


            moveflg2 = true;
        }

        if (moveflg3 == false)
        {
            moveflg3 = true;
        }

        
        if(bufposY <= _starfish.transform.position.y)       // 最大高度まで上がったらヒトデを消す
        {
            bufposY = _starfish.transform.position.y;
            //Debug.Log(bufposY);
        }
        else
        {
            ChangePhase(PHASE.FIREWORKS);

            //Debug.Log("destroy");
        }
    }


    private void FireWorksProcess()
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
            //if (Input.GetMouseButtonDown(0))
            //{
            //    SceneManager.LoadScene(nextScene);
            //}
        }
    }
    

    private void ChangePhase(PHASE p)
    {
        now_phase = p;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        moveflg1 = moveflg2 = moveflg3 = false;

    }


    private void SkipProcess()
    {
        _cam1.gameObject.SetActive(!_cam1.gameObject.activeSelf);
        _cam2.gameObject.SetActive(!_cam2.gameObject.activeSelf);

        se_bubble.Stop();
        se_rising.Stop();

        whiteback.gameObject.SetActive(true);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }

}

