using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmDisplay : MonoBehaviour {

    [SerializeField] GameObject[] TextButtons;       // 本当にリセットするかどうかの確認ボタンとBGM等のUI

    // 確認ボタンを表示
    public void WarmTextOnDisplay()
    {
        TextButtons[0].gameObject.SetActive(true);  // 確認ボタンのSetActiveをtrueに
        TextButtons[1].gameObject.SetActive(false); // BGMなどのUIのSetActiveをfalseに
        gameObject.SetActive(false);                // 自分自身のSetActiveをfalseに
    }

    // Noボタンを押したとき
    public void ResetCancel()
    {
        TextButtons[0].gameObject.SetActive(true);  // リセットボタンのSetActiveをtrueに
        TextButtons[1].gameObject.SetActive(true);  // BGMなどのUIのSetActiveをfalseに
        gameObject.SetActive(false);                // 自分自身の親のSetActiveをfalseに
    }
}
