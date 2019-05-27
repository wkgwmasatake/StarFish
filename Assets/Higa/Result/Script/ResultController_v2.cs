using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController_v2 : MonoBehaviour
{

    enum PHASE
    {
        FADE,
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
    [SerializeField] float fadetime_cam;
    [SerializeField] AudioSource star_rising;
    [SerializeField] AudioSource star_fill;
    [SerializeField] AudioSource star_complete;
    [SerializeField] AudioSource bgm;

    private PHASE now_phase;

    private GameObject _rising_star;
    private int release_area;
    private int release_stage;
    private int now_stage;
    private int now_area;
    private SpriteRenderer now_line;
    private SpriteRenderer now_image;


    // Use this for initialization
    void Start()
    {

        whitefade.enabled = true;


        starry_sky.SetActive(false);

        now_phase = PHASE.FADE;

        //release_area = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetAreaClear_Flg);
        release_stage = GetComponent<LoadStageInfo>().LoadStageClear(GameDirector.Instance.GetStageClear_Flg) - 2;
        release_area = release_stage / 3;
        release_stage = release_stage - release_area * 3;
        now_stage = GameDirector.Instance.GetSceneNumber - 2;
        now_area = now_stage / 3;
        now_stage = now_stage - now_area * 3;
        Debug.Log("GetSceneNumber : " + GameDirector.Instance.GetSceneNumber);
        Debug.Log("now_stage : " + now_stage);
        Debug.Log("now_area : " + now_area);
        Debug.Log("release_stage : " + release_stage);
        Debug.Log("release_area : " + release_area);

        ImageSetActive(now_area);
        switch (now_area)
        {
            case 0:
                PawnStarCluster(constellation_line[now_area], constellation_image[now_area], sheep_stars);
                break;

            case 1:
                PawnStarCluster(constellation_line[now_area], constellation_image[now_area], cassiopeia_stars);
                break;

            case 2:
                PawnStarCluster(constellation_line[now_area], constellation_image[now_area], smallbear_stars);
                break;

            case 3:
                PawnStarCluster(constellation_line[now_area], constellation_image[now_area], balance_stars);
                break;

            case 4:
                PawnStarCluster(constellation_line[now_area], constellation_image[now_area], crab_stars);
                break;

            default:
                Debug.Log("Stage Load Error");
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

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

            case PHASE.CAMERA:
                CameraProcess();
                break;
        }

        //Debug.Log(now_phase);
    }

    private void FadeProcess()
    {

        whitefade.rectTransform.anchoredPosition =
                    new Vector2(whitefade.rectTransform.anchoredPosition.x, whitefade.rectTransform.anchoredPosition.y - 100);     // フェード画像のy座標を50下げる

        //Debug.Log(whitefade.rectTransform.anchoredPosition);

        if (whitefade.rectTransform.anchoredPosition.y < -2850f)
        {
            //ChangePhase(PHASE.CAMERA);
        }
    }

    private void CameraProcess()
    {
        if (camera.transform.position != new Vector3(0, -2, -10))
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5.0f, Time.deltaTime);
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, -2, -10), Time.deltaTime);

            float alpha = 1 / (60 * fadetime_cam);
            var color = now_line.color;
            color.a += alpha;
            now_line.color = color;
            if(now_area < release_area || (release_stage == 2 && now_stage == 2))
            {
                now_image.color = color;

            }
            bgm.volume = Mathf.Lerp(bgm.volume, 1.0f, Time.deltaTime / 3);
            Debug.Log(bgm.volume);
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

        if(now_area < release_area)
        {
            release_stage = 2;
        }

        if (release_stage < 0)
        {
            release_stage = 0;
            now_stage = 0;
        }

        for (int i = 0; i <= release_stage; i++)
        {
            if (i != now_stage)
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
        star_fill.Play();

        yield return new WaitForSeconds(second);
        starry_sky.SetActive(true);
        ChangePhase(PHASE.CAMERA);

        if (now_area < release_area || (release_stage == 2 && now_stage == 2))
        {
            star_complete.Play();
        }

        GameDirector.Instance.ParticleFlg = true;

        //yield return null;
    }

    private void ImageSetActive(int num)
    {

        for (int i = 0; i < 5; i++)
        {
            if (i != num)
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