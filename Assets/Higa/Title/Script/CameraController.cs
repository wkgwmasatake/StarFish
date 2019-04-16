using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

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

    private PHASE now_phase;
    private float speed;
    private float time;

    private bool pawnflg1;
    private bool pawnflg2;
    private bool pawnflg3;



    Vector3 startPos;

    // Use this for initialization
    void Start () {

        startPos = _cam1.transform.position;
        _cam1.SetActive(true);
        _cam2.SetActive(false);
        
        //bubble1.SetActive(false);
        //bubble2.SetActive(false);
        //bubble3.SetActive(false);

        now_phase = PHASE.START;
        speed = 0f;
        time = 0f;

        pawnflg1 = pawnflg2 = pawnflg3 = false;
	}
	
	// Update is called once per frame
	void Update () {

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


        //if (Input.GetKeyDown("1"))
        //{
        //    _cam1.gameObject.SetActive(!_cam1.gameObject.activeSelf);
        //    _cam2.gameObject.SetActive(!_cam2.gameObject.activeSelf);
        //}
	}

    private void StartProcess()
    {
        if (Input.GetMouseButtonDown(0))
        {
            now_phase = PHASE.BUBBLE;
        }

        //bubble1.SetActive(true);
        //bubble2.SetActive(true);
        //bubble3.SetActive(true);
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
            now_phase = PHASE.RISING;
            pawnflg1 = pawnflg2 = pawnflg3 = false;
        }

        //if (Input.GetKeyDown("1"))
        //{
        //    Instantiate(bubble1);
        //}
        //if (Input.GetKeyDown("2"))
        //{
        //    Instantiate(bubble2);
        //}
        //if (Input.GetKeyDown("3"))
        //{
        //    Instantiate(bubble3);
        //}
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

            pawnflg1 = true;
        }

        speed += 0.005f;

        if (_cam1.transform.position.y + speed < 20)
        {

            Vector3 pos = _cam1.transform.position;

            pos.y += speed;

            _cam1.transform.position = pos;

            //Debug.Log(pos.y);
        }
        else
        {
            _cam1.gameObject.SetActive(!_cam1.gameObject.activeSelf);
            _cam2.gameObject.SetActive(!_cam2.gameObject.activeSelf);

            whiteback.gameObject.SetActive(!whiteback.gameObject.activeSelf);

            now_phase = PHASE.SPLASH;
            //Destroy(this.gameObject);

            //_cam1.transform.position = startPos;
            //speed = 0f;
        }

    }


    private void SplashProcess()
    {
        //whiteback.GetComponent<SpriteRenderer>().color.a 
    }


    private void StarfishProcess()
    {

    }


    private void FireWorksProcess()
    {

    }


    private void EndProcess()
    {

    }

}

