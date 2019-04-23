using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StegeClick : MonoBehaviour {

    [SerializeField] private float TOUCH_SPEED = 300f;//378f;
    [SerializeField] private GameObject Parent_stage;
    private GameObject test_obj;
    [SerializeField] private GameObject Parent_icon;
    [SerializeField] private GameObject Director;
    [SerializeField] private float move_time = 0.6f;
    private bool touch_left = false;
    private bool touch_right = false;
    private AudioSource  tap_SE;
    void Start()
    {
        tap_SE = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (touch_left == true)
        {
            Move_Left();
        }
        else if (touch_right == true)
        {
            Move_Right();
        }
    }

    public void Left_Onclick()
    {
        if (gameObject.name == "Left_arrow" && touch_left == false && StageDirector.Instance.GetNumStage() > 0)
        {
            tap_SE.PlayOneShot(tap_SE.clip);
            StageDirector.Instance.SetNumStage(-1);
            touch_left = true;
            StartCoroutine(Change_left());
        }
    }

    public void Right_Onclick()
    {
        if (gameObject.name == "Right_arrow" && touch_right == false && StageDirector.Instance.GetNumStage() < 2)
        {
            tap_SE.PlayOneShot(tap_SE.clip);
            StageDirector.Instance.SetNumStage(1);
            touch_right = true;
            StartCoroutine(Change_right());
        }
    }

    private void Move_Left()
    {
        Parent_stage.transform.position += new Vector3(TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    private void Move_Right()
    {
        Parent_stage.transform.position += new Vector3(-TOUCH_SPEED * Time.deltaTime, 0, 0);
    }

    IEnumerator Change_left()
    {
        yield return new WaitForSeconds(move_time);
        touch_left = false;
    }

    IEnumerator Change_right()
    {
        yield return new WaitForSeconds(move_time);
        touch_right = false;
    }
}
