using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadCSV : MonoBehaviour
{
    private float[,] _LoadPos;

    public int Load()
    {
        StreamReader sr = new StreamReader(@"Assets/Resources/Ghost_Record.csv");

        if (sr == null) return -1;

        _LoadPos = new float[50, 2];  //２次元配列を用意（５０個分の座標とｘ、ｙ分の２個）
        string line;
        int i = 0;

        while((line = sr.ReadLine()) != null)
        {
            string[] Spritline = line.Trim('(', ')').Split(',');  //読み込んだ１行に含まれている中かっこを取り除き、カンマで区切って読み込む
            _LoadPos[i, 0] = float.Parse(Spritline[0]);  //x座標を抽出
            _LoadPos[i, 1] = float.Parse(Spritline[1]);  //y座標を抽出
            i++;
        }

        sr.Close();

        return i;
    }

    //ゲッター
    public float[,] LoadPos
    {
        get { return _LoadPos; }
    }
}