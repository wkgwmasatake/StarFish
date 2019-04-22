using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour {

    [SerializeField] Camera _camera;
    [SerializeField] ParticleSystem tapEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);

            //tapEffect.transform.position = pos;
            //tapEffect.Emit(1);

            var effect = GameObject.Instantiate(tapEffect);
            effect.transform.position = pos;

            //Debug.Log(pos);
        }
    }
}
