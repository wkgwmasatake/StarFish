using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecialTitle : MonoBehaviour
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
    [SerializeField] GameObject gray_star;
    [SerializeField] GameObject fade_star;
    [SerializeField] GameObject starry_sky;
    [SerializeField] GameObject shooting_star;
    [SerializeField] SpriteRenderer titlelogo;
    [SerializeField] float fadetime_cam;
    [SerializeField] AudioSource star_rising;
    [SerializeField] AudioSource star_fill;
    [SerializeField] AudioSource star_complete;

    private PHASE now_phase;



    // Use this for initialization
    void Start()
    {

        whitefade.enabled = true;


        starry_sky.SetActive(false);
        shooting_star.SetActive(false);

        now_phase = PHASE.FADE;


        //titlelogo.enabled = false;

        PawnStarCluster(titlelogo);
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
        if (camera.transform.position != new Vector3(0, 0, -10))
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5.0f, Time.deltaTime);
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, 0, -10), Time.deltaTime);

            float alpha = 1 / (60 * fadetime_cam);
            var color = titlelogo.color;
            color.a += alpha;
            titlelogo.color = color;

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


    private void PawnStarCluster(SpriteRenderer _image)
    {
        //for (int i = 0; i < sheep_stars.Length-1; i++)
        //{
        //    sheep_stars[i] = constellation_line.transform.GetChild(i).gameObject;
        //}

        float alpha = 0;
        var color = _image.color;
        color.a = alpha;
        _image.color = color;

                var _rising_star = Instantiate(rising_star);
                Vector3 pos = gray_star.transform.position;
                pos.y -= 4.05f + 0.4165f;   // GrayStarとRisingStarの Y軸 の差

                //_rising_star.transform.position = constellation_num[i].transform.position;
                _rising_star.transform.position = pos;

                //Debug.Log("rising : "+_rising_star.transform.position);
                //Debug.Log("gray : "+constellation_num[i].transform.position);
                //Debug.Log("pos : " + pos);

                StartCoroutine(PawnFadeStar(gray_star.transform.position, 1.0f));

                Vector3 pos1 = gray_star.transform.position;
                pos1.z = -10f;
                camera.transform.position = pos1;
                camera.orthographicSize = 1.0f;


    }

    private IEnumerator PawnFadeStar(Vector3 pos, float second)
    {
        yield return new WaitForSeconds(second);

        var _fade_star = Instantiate(fade_star);
        _fade_star.transform.position = pos;
        star_fill.Play();

        yield return new WaitForSeconds(second);
        starry_sky.SetActive(true);
        shooting_star.SetActive(true);
        ChangePhase(PHASE.CAMERA);


        //star_complete.Play();


        GameDirector.Instance.ParticleFlg = true;

        //yield return null;
    }

}