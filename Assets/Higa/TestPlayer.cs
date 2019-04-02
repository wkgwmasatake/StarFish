using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    public Vector2 position;
    public int armNumber;

    [SerializeField] ParticleSystem tapEffect;              // タップしたとき
    [SerializeField] ParticleSystem Explosion;              // 腕の爆発
    [SerializeField] ParticleSystem WallHit;                // 壁に当たったとき
    [SerializeField] ParticleSystem Pearl;                  // パールを取得したとき
    [SerializeField] ParticleSystem Rotation;               // 回転したとき
    [SerializeField] ParticleSystem FireWorks;              // 花火
    [SerializeField] ParticleSystem fw_red;
    [SerializeField] ParticleSystem fw_green;
    [SerializeField] ParticleSystem fw_blue;
    [SerializeField] ParticleSystem fw_yellow;
    [SerializeField] Camera _camera;                        // カメラの座標


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

            Debug.Log(pos);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var effect = GameObject.Instantiate(Explosion);
            effect.transform.position = new Vector2( 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var effect = GameObject.Instantiate(WallHit);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var effect = GameObject.Instantiate(Pearl);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            var effect = GameObject.Instantiate(Rotation);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            var effect = GameObject.Instantiate(FireWorks);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            var effect = GameObject.Instantiate(fw_red);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            var effect = GameObject.Instantiate(fw_green);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            var effect = GameObject.Instantiate(fw_blue);
            effect.transform.position = new Vector2(0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            var effect = GameObject.Instantiate(fw_yellow);
            effect.transform.position = new Vector2(0, 0);
        }
    }
}
