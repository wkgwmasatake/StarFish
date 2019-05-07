using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStageInfo : MonoBehaviour {

    // ステージをクリアした際に呼び出す関数。引数にはステージ番号を持たせる。
    public void SaveSatageClearInfo(int stageNo)
    {
        if(stageNo < 1)
        {
            return;
        }

        int StageStatus = GameDirector.Instance.GetStageClear_Flg;      // ステージのクリア状況を取得

        if(StageStatus < 0)
        {
            return;
        }
        StageStatus = StageStatus | (int)Mathf.Pow(2, stageNo);         // クリアしたステージの番号の2乗と現在のステージの和をステージのクリア状況として変数に格納

        GameDirector.Instance.SetStageClear_Flg = StageStatus;          // 更新したステージのクリア状況をディレクターに渡す
    }

    // アプリケーション終了時にデータを保存するために呼び出す
    public void SaveData()
    {
        PlayerPrefs.SetInt("STAGE", GameDirector.Instance.GetStageClear_Flg);       // ステージのクリア状況をPlayerPrefsに保存
        PlayerPrefs.SetInt("AREA", GameDirector.Instance.GetAreaClear_Flg);         // エリアの制覇状況をPlayerPrefsに保存
    }
}
