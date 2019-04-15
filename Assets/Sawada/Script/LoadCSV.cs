using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadCSV : MonoBehaviour
{
    private float[,] _LoadPos;
    private float[] _LoadAngle;

    public int Load()
    {
        StreamReader sr = new StreamReader(@"Assets/Resources/Ghost_Record.csv");

        if (sr == null) return -1;

        _LoadPos = new float[200, 2];  //２次元配列を用意
        _LoadAngle = new float[200];
        string line;
        int i = 0;

        while((line = sr.ReadLine()) != null)
        {
            string[] Spritline = line.Trim('(', ')').Split(',');  //読み込んだ１行に含まれている中かっこを取り除き、カンマで区切って読み込む
            if (i % 2 == 0)
            {
                _LoadPos[i, 0] = float.Parse(Spritline[0]);  //x座標を抽出
                _LoadPos[i, 1] = float.Parse(Spritline[1]);  //y座標を抽出
            }
            else
            {
                _LoadAngle[i] = float.Parse(line);
            }

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
    public float[] LoadAngle
    {
        get { return _LoadAngle; }
    }
}