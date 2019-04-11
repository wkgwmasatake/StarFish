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

    public void SavePos(Vector2[] position , byte length)
    {
        
        // ファイルパスとファイルの上書きを指定(trueもしくは指定しなかったら追記)
        StreamWriter sw = new StreamWriter(@"Assets/Resources/Ghost_Record.csv", false);

        for(int i = 0; i < length; i++)
        {
            sw.WriteLine(position[i]);      // i番目の座標を1行ずつCSVファイルに書き込み
        }

        sw.Close();

        Debug.Log("Save CSV");
    }

    
}
