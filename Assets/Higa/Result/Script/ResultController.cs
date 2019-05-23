using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject starry_sky;
    [SerializeField] GameObject[] constellation_parent;
    [SerializeField] SpriteRenderer[] constellation_line;
    [SerializeField] SpriteRenderer[] constellation_image;
    [SerializeField] GameObject[] sheep_stars;
    [SerializeField] GameObject[] cassiopeia_stars;
    [SerializeField] GameObject[] smallbear_stars;
    [SerializeField] GameObject[] balance_stars;
    [SerializeField] GameObject[] crab_stars;
    [SerializeField] int now_clear_stage;
    [SerializeField] float fadetime;

    [SerializeField] float RisingTime;

    private PHASE now_phase;

    private GameObject _rising_star;
    private ParticleSystem _star_ps;
    private float time;
    private int area_num;
    private int stage_num;
    private SpriteRenderer now_line;
    private SpriteRenderer now_image;
    

    private bool pawnflg1, pawnflg2, pawnflg3;

	// Use this for initialization
	void Start () {

        whitefade.enabled = true;


        starry_sky.SetActive(false);

        now_phase = PHASE.FADE;

        pawnflg1 = pawnflg2 = pawnflg3 = false;

        //area_num = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetAreaClear_Flg);
        stage_num = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetStageClear_Flg);
        area_num = (stage_num - 2) / 3;
        stage_num -= 3 * area_num;
        now_clear_stage = GameDirector.Instance.GetSceneNumber - 1;
        now_clear_stage -= 3 * area_num;
        Debug.Log("sceneNumber : " + GameDirector.Instance.GetSceneNumber);
        Debug.Log("stage_num : " + stage_num);
        Debug.Log("area_num : " + area_num);

        ImageSetActive(area_num);
        switch (area_num)
        {
            case 0:
                PawnStarCluster(constellation_line[area_num],constellation_image[area_num], sheep_stars);
                break;

            case 1:
                PawnStarCluster(constellation_line[area_num], constellation_image[area_num], cassiopeia_stars);
                break;

            case 2:
                PawnStarCluster(constellation_line[area_num], constellation_image[area_num], smallbear_stars);
                break;

            case 3:
                PawnStarCluster(constellation_line[area_num], constellation_image[area_num], balance_stars);
                break;

            case 4:
                PawnStarCluster(constellation_line[area_num], constellation_image[area_num], crab_stars);
                break;

            default:
                Debug.Log("Stage Load Error");
                break;
        }


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Debug.Log(GameDirector.Instance.GetArmNumber());
        switch (now_phase)
        {
            case PHASE.FADE:
                FadeProcess();
                break;

            case PHASE.STAR:
                //StarfishProcess();
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


    //private void StarfishProcess()
    //{

    //    if(pawnflg1 == false)
    //    {

    //        _rising_star = Instantiate(rising_star);
    //        //Rigidbody2D rb = _star.GetComponent<Rigidbody2D>();
    //        //_star_ps = _rising_star.GetComponent<ParticleSystem>();

    //        //Vector2 force = new Vector2(0f, 45f);
    //        //rb.AddForce(force, ForceMode2D.Impulse);
    //        //rb.AddTorque(5f, ForceMode2D.Impulse);

    //        Invoke("PawnFadeStar", 1f);

    //        pawnflg1 = true;

    //    }

    //    time += Time.time;
    //    if (time >= RisingTime)
    //    {
    //        if (GameObject.Find("GameDirector") != null)
    //        {
    //            GameDirector.Instance.ParticleFlg = true;
    //            //Debug.Log("finish");
    //        }
    //        else
    //        {
    //            Debug.Log("Fireworks finished");
    //        }

    //        ChangePhase(PHASE.END);
    //    }
    //    //Debug.Log(GameDirector.Instance.ParticleFlg);
    //}


    private void CameraProcess()
    {
        if(camera.transform.position != new Vector3(0, -2, -10))
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5.0f, Time.deltaTime);
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, -2, -10), Time.deltaTime);

            float alpha = 1 / (60 * fadetime);
            var color = now_line.color;
            color.a += alpha;
            now_line.color = color;
            now_image.color = color;
        }
        else
        {
            //GameDirector.Instance.ParticleFlg = true;
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

    private void PawnStarCluster(SpriteRenderer _line, SpriteRenderer _image, GameObject[] _stars)
    {
        //for (int i = 0; i < sheep_stars.Length-1; i++)
        //{
        //    sheep_stars[i] = constellation_line.transform.GetChild(i).gameObject;
        //}

        float alpha = 0;
        var color = _line.color;
        color.a = alpha;
        _line.color = color;
        _image.color = color;
        now_line = _line;
        now_image = _image;

        for (int i = 0; i < stage_num - 1; i++)
        {
            if (i != now_clear_stage- 1)
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
                camera.orthographicSize = 1.0f;
            }

        }

    }

    private IEnumerator PawnFadeStar(Vector3 pos, float second)
    {
        yield return new WaitForSeconds(second);

        var _fade_star = Instantiate(fade_star);
        _fade_star.transform.position = pos;

        yield return new WaitForSeconds(second);
        starry_sky.SetActive(true);
        ChangePhase(PHASE.CAMERA);
        GameDirector.Instance.ParticleFlg = true;

        //yield return null;
    }

    private void ImageSetActive(int num)
    {

        for (int i = 0; i < 5; i++)
        {
            if(i != num)
            {
                try
                {
                    constellation_parent[i].SetActive(false);
                }
                catch
                {
                    Debug.Log("constellation_parent[" + i + "]");
                }

            }
            else
            {
                constellation_parent[i].SetActive(true);
            }

        }
    }
}
