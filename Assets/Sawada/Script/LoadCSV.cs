using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadCSV : MonoBehaviour
{
    private float[,] _LoadPos;
    private float[] _LoadAngle;

    private void Start()
    {
        GameDirector.Instance.SetSceneName = SceneManager.GetActiveScene().name;
    }

    public int Load()
    {
        string filePass;

        //アンドロイドで実行している場合
        if(Application.platform == RuntimePlatform.Android)
        {
            //軌跡データのパスを保存
            filePass = Application.persistentDataPath + "\\Resources\\Ghost_Record.csv";
        }
        else
        {
            //軌跡データのパスを保存
            filePass = Application.dataPath + "\\Resources\\Ghost_Record.csv";
        }

        StreamReader sr = new StreamReader(filePass);

        // --- 読み込みエラー ---//
        if (sr == null) return -1;

        _LoadPos = new float[100, 2];
        _LoadAngle = new float[100];

        string line;
        int i = 0;
        int j = 0;

        while((line = sr.ReadLine()) != null)
        {
            if (i++ % 2 == 0)
            {
                string[] Spritline = line.Trim('(', ')').Split(',');  //読み込んだ１行に含まれている中かっこを取り除き、カンマで区切って読み込む
                //Debug.Log(Spritline[0]);
                //Debug.Log(Spritline[1]);
                _LoadPos[j, 0] = float.Parse(Spritline[0]);  //x座標を抽出
                _LoadPos[j, 1] = float.Parse(Spritline[1]);  //y座標を抽出
            }
            else
            {
                _LoadAngle[j] = float.Parse(line);
                j++;
            }
        }

        sr.Close();

        return j;
    }

    //ユーザーが見えない場所に保存
    public int BinaryLoad()
    {
        string filePass;

        //アンドロイドで実行している場合
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                using (var StarFish = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var currentActivity = StarFish.GetStatic<AndroidJavaObject>("currentActivity"))
                using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
                {
                    filePass = getFilesDir.Call<string>("getCanonicalPath");  //ユーザーから見えない場所のパスを取得
                }
            }
            catch
            {
                return -1;
            }
        }
        else
        {
            filePass = Application.dataPath + "/Resources/";    //ファイルパスとファイル名を結合
        }

        string combinedPath = Path.Combine(filePass, "Ghost_Record.csv");   //ファイルパスとファイル名を結合

        string line;
        int i = 0;
        int j = 0;

        try
        {
            using (StreamReader sr = new StreamReader(combinedPath))
            {
                _LoadPos = new float[100, 2];
                _LoadAngle = new float[100];
                while ((line = sr.ReadLine()) != null)
                {
                    if (i++ % 2 == 0)
                    {
                        string[] Spritline = line.Trim('(', ')').Split(',');  //読み込んだ１行に含まれている中かっこを取り除き、カンマで区切って読み込む
                                                                              //Debug.Log(Spritline[0]);
                                                                              //Debug.Log(Spritline[1]);
                        _LoadPos[j, 0] = float.Parse(Spritline[0]);  //x座標を抽出
                        _LoadPos[j, 1] = float.Parse(Spritline[1]);  //y座標を抽出
                    }
                    else
                    {
                        _LoadAngle[j] = float.Parse(line);
                        j++;
                    }
                }
            }
        }
        catch
        {
            return -1;
        }

        return j;

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