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

    public void SaveAreaClearInfo(int areaNo)
    {
        if(areaNo < 1)
        {
            return;
        }

        int AreaStatus = GameDirector.Instance.GetAreaClear_Flg;        // エリアのクリア状況を取得

        if(AreaStatus < 0)
        {
            return;
        }

        AreaStatus = AreaStatus | (int)Mathf.Pow(2, areaNo);            // 制覇したステージの番号の2乗と現在のエリアの和をエリアのクリア状況として変数に格納

        GameDirector.Instance.SetAreaClear_Flg = AreaStatus;            // 更新したエリアのクリア状況をディレクターに渡す
    }

    // それぞれのステージをクリアした際に真珠の獲得状況を保存する関数
    public void SaveGetPearlInfo(int stageNo)
    {
        if (stageNo < 1)
        {
            return;
        }

        int PearlStatus = GameDirector.Instance.GetPearlFlag;      // 真珠の取得状況を取得

        if (PearlStatus < 0)
        {
            return;
        }
        PearlStatus = PearlStatus | (int)Mathf.Pow(2, stageNo);    // クリアしたステージの番号の2乗と現在のステージの和を真珠の取得状況として変数に格納

        GameDirector.Instance.SetPearlFlag = PearlStatus;          // 更新したステージのクリア真珠の取得状況をディレクターに渡す
    }
}
