using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStageInfo : MonoBehaviour {

    // ステージのクリア状況を取得する際に呼び出す関数
    public int LoadStageClear(int StageStatus)
    {
        // ステージもしくはエリアのクリア状況が取得できなければ-1を返す
        if(StageStatus <= 0)
        {
            return -1;
        }

        // 1が立っている最上位ビットを取得
        StageStatus = ((StageStatus & 0xFF00) != 0) ? StageStatus & 0xFF00 : StageStatus;
        StageStatus = ((StageStatus & 0xF0F0) != 0) ? StageStatus & 0xF0F0 : StageStatus;
        StageStatus = ((StageStatus & 0xCCCC) != 0) ? StageStatus & 0xCCCC : StageStatus;
        StageStatus = ((StageStatus & 0xAAAA) != 0) ? StageStatus & 0xAAAA : StageStatus;

        // ステージ、もしくはエリアをどこまでクリアしたかを取得し、その値がステージの最大数より大きければ-1を返す
        if(Mathf.Log(StageStatus, 2) > GameDirector.Instance.GetSTAGE_MAX)
        {
            return -1;
        }

        return ((int)Mathf.Log(StageStatus, 2)) + 1;      // 求めた値の2の何乗か + 1を出すことでどのステージ、エリアまで解放しているかを出すことができる。
    }
}
