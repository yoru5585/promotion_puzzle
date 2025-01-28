using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInstance_s : MonoBehaviour
{
    //ゲーム全体で影響するオブジェクトを管理するスクリプト
    //シングルトン
    public static CheckInstance_s instance;

    private void Awake()
    {
        //自身が重複しているかチェック
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
