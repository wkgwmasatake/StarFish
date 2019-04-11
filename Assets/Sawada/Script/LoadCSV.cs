using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadCSV : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (!Load()) Debug.LogError("Not CSV");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool Load()
    {
        StreamReader sr = new StreamReader(@"Assets/Resources/Ghost_Record.csv");

        if (sr == null) return false;

        string line;
        float[,] LoadPos = new float[50, 2];  //２次元配列を用意（５０個分の座標とｘ、ｙ分の２個）
        int i = 0;

        while((line = sr.ReadLine()) != null)
        {
            string[] Spritline = line.Trim('(', ')').Split(',');  //読み込んだ１行に含まれている中かっこを取り除き、カンマで区切って読み込む
            LoadPos[i, 0] = float.Parse(Spritline[0]);  //x座標を抽出
            LoadPos[i, 1] = float.Parse(Spritline[1]);  //y座標を抽出
            i++;
        }

        sr.Close();

        return true;
    }
}