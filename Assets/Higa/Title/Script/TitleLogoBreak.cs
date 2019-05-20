using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogoBreak : MonoBehaviour {

    [SerializeField] float breaktime;

    private float time;
    private bool breakflg;
    public Explodable explodable;

	// Use this for initialization
	void Start () {

        explodable = GetComponent<Explodable>();

        time = 0;
        breakflg = false;

	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        if (time >= breaktime)
        {
            explodable.explode();
            ExplosionForce ef = GameObject.Find("Force").GetComponent<ExplosionForce>();
            ef.doExplosion(transform.position);
            breakflg = true;
        }

	}
}
