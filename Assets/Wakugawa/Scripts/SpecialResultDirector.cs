﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecialResultDirector : MonoBehaviour {

    [SerializeField] GameObject FadeImage;       // 画面を飛ばす際に黒フェードさせるための画像
    bool TapFlag = false;                   // タップの2度押し防止

    private void Update()
    {
        // 画面をタップした際にアニメーションを飛ばす
        if(Input.GetMouseButtonDown(0) && !TapFlag)
        {
            TapFlag = true;     // フラグをタップ済みに変更(2度押し防止)

            // コルーチンでフェードアウト処理後にタイトルにシーン遷移
            StartCoroutine("SkipAnimation");
        }
    }

    // アニメーション終了時にタイトルシーンに遷移させる
    private void SceneChangeTitle()
    {
        // タイトルを読み込み
        SceneManager.LoadScene("Title");
    }

    // フェードアウト処理後にタイトルにシーン遷移
    private IEnumerator SkipAnimation()
    {

        while(FadeImage.GetComponent<SpriteRenderer>().color.a < 1)
        {
            // 画像の色を取得
            Color FadeColor = FadeImage.GetComponent<SpriteRenderer>().color;

            // 画像の透明度を加算
            FadeImage.GetComponent<SpriteRenderer>().color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, FadeColor.a + 0.01f);

            yield return null;
        }

        SceneManager.LoadScene("Title");
    }
}
