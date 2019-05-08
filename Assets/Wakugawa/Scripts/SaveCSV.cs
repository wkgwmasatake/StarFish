using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveCSV : MonoBehaviour {

   // public Text testText;

    public void SavePos(Vector2[] position, float[] angle, byte length)
    {
        string filePass;

        // アンドロイドで実行している場合
        if (Application.platform == RuntimePlatform.Android)
        {
            // 軌跡データのパスを保存
            filePass = Application.persistentDataPath + "\\Resources\\Ghost_Record.csv";

        }
        else    // その他(pc)で実行している場合
        {
            // 軌跡データのパスを保存
            filePass = Application.dataPath + "\\Resources\\Ghost_Record.csv";
        }
        // ファイルパスとファイルの上書きを指定(trueもしくは指定しなかったら追記)
        StreamWriter sw = new StreamWriter(filePass, false);

        for(int i = 0; i < length; i++)
        {
            sw.WriteLine(position[i]);      // i番目の座標を1行ずつCSVファイルに書き込み
            sw.WriteLine(angle[i]);         // i番目の角度を1行ずつCSVファイルに書き込み
        }

        sw.Close();
    }

    // ユーザーが見えない場所に保存
    public void BinarySavePos(Vector2[] position, float[] angle, byte length)
    {
        string filePass;

        // アンドロイドで実行している場合
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                using (var StarFish = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var currentActivity = StarFish.GetStatic<AndroidJavaObject>("currentActivity"))
                using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
                {
                    filePass = getFilesDir.Call<string>("getCanonicalPath");    // ユーザーから見えない場所のパスを取得
                }
            }
            catch
            {
                return;
            }
        }
        else
        {
            filePass = Application.dataPath + "/Resources/";  // Resourcesフォルダ直下に保存
        }

        string combinedPath = Path.Combine(filePass, "Ghost_Record" + (GameDirector.Instance.GetSceneNumber - 1).ToString() + ".csv");       // ファイルパスとファイル名を結合

        try
        {
            using (StreamWriter sw = new StreamWriter(combinedPath, false))             // ファイルパスとファイルの上書きを指定(trueもしくは指定しなかったら追記)
            {
                for (int i = 0; i < length; i++)
                {
                    sw.WriteLine(position[i]);
                    sw.WriteLine(angle[i]);
                }
            }
        }
        catch
        {
            return;
        }
    }
}
