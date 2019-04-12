using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pref_test : MonoBehaviour {
    //DeleteAll    設定情報からすべてのキーと値を削除します。注意して使用してください。
    //DeleteKey    設定情報から key と対応する値を削除します。
    //GetFloat キーが存在する場合、key に対応する値を取得します。
    //GetInt   キーが存在する場合、key に対応する値を取得します。
    //GetString    キーが存在する場合、key に対応する値を取得します。
    //HasKey   key が設定に存在した場合、true を返します
    //Save     変更された値をディスクへと保存します
    //SetFloat key で識別される設定情報の値を設定します。
    //SetInt   key で識別される設定情報の値を設定します。
    //SetString    key で識別される設定情報の値を設定します。

    void Start()
    {
        Test1();
    }

    // 各種挙動確認
    private void Test1()
    {
        int intValue = 0;
        string stringValue = "";
        // SetInt⇒SetString⇒Save⇒GetInt⇒GetStringで、セーブした文字列が取れる
        PlayerPrefs.SetInt("IntKey", 5);
        PlayerPrefs.SetString("StringKey", "mochi");
        PlayerPrefs.Save();
        intValue = PlayerPrefs.GetInt("IntKey");
        Debug.Log(intValue);    // 5
        stringValue = PlayerPrefs.GetString("StringKey");
        Debug.Log(stringValue); // mochi

        // DeleteAllした後に、取得を試みる
        PlayerPrefs.DeleteAll();
        intValue = PlayerPrefs.GetInt("IntKey");
        Debug.Log(intValue);// 0
        stringValue = PlayerPrefs.GetString("StringKey");
        Debug.Log(stringValue);// 空文字列

        // Saveをしないで、値を取得してみる
        // SetIntした後は、コメントアウトしている
        PlayerPrefs.SetInt("IntKey", 5);
        PlayerPrefs.SetString("StringKey", "mochi");
        intValue = PlayerPrefs.GetInt("IntKey");
        Debug.Log(intValue);// 5
        stringValue = PlayerPrefs.GetString("StringKey");
        Debug.Log(stringValue);// mochi
        // ※UnityEditor上確認だけだが、Saveを呼ばない場合も値が保存されている模様
        // ・再実行
        // ・UnityEditorを再起動
        // これらをしても、Saveをしなくても保存されていた

        // HasKeyの検証
        // 一旦DeleteAllしてから取ってみる
        PlayerPrefs.DeleteAll();
        bool resInt = PlayerPrefs.HasKey("IntKey");
        bool resString = PlayerPrefs.HasKey("StringKey");
        Debug.Log(resInt);// false
        Debug.Log(resString);// false

        // Setして取ってみる
        PlayerPrefs.SetInt("IntKey", 5);
        PlayerPrefs.SetString("StringKey", "mochi");
        resInt = PlayerPrefs.HasKey("IntKey");
        resString = PlayerPrefs.HasKey("StringKey");
        Debug.Log(resInt);// true
        Debug.Log(resString);// true

        // この状態で、DeleteKeyをして、値と存在チェックをしてみる
        PlayerPrefs.DeleteKey("IntKey");
        PlayerPrefs.DeleteKey("StringKey");
        intValue = PlayerPrefs.GetInt("IntKey");
        Debug.Log(intValue);// 0
        stringValue = PlayerPrefs.GetString("StringKey");
        Debug.Log(stringValue);// 空文字
        resInt = PlayerPrefs.HasKey("IntKey");
        resString = PlayerPrefs.HasKey("StringKey");
        Debug.Log(resInt);// false
        Debug.Log(resString);// false

        // 結論
        // ・セーブは、Set～をした時点で発生
        // ・無いキーでデータを取ろうとしても、Exceptionにはならない。ただし、値は空で来てしまう
        // ・HasKeyは、SetしてDeleteしていなければtrue、そうでなければfalse
    }
}
