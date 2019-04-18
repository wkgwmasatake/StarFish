using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    [SerializeField] float inteMin;
    [SerializeField] float inteMax;
    [SerializeField] float speed;

    private bool turnflg;


	// Use this for initialization
	void Start () {

        turnflg = true;

	}
	
	// Update is called once per frame
	void Update () {


        if(turnflg == false && this.GetComponent<Light>().intensity <= inteMin)
        {
            turnflg = true;
        }
        else if (turnflg == true && this.GetComponent<Light>().intensity >= inteMax)
        {
            turnflg = false;
        }



        if(turnflg == false)
        {
            this.GetComponent<Light>().intensity -= speed;
            //Debug.Log(this.GetComponent<Light>().intensity);
            //Debug.Log("to Min");
        }
        else
        {
            this.GetComponent<Light>().intensity += speed;
            //Debug.Log(this.GetComponent<Light>().intensity);
            //Debug.Log("to Max");
        }
	}
}
