using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveCSV : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

        Debug.Log("Save CSV");
    }

    
}
