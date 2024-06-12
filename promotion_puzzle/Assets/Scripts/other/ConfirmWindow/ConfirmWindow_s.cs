using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ConfirmWindow_s
{
    //確認画面を表示
    /// <summary>
    /// 確認のためのポップアップウィンドウを表示します。
    /// pass:ScriptableObjectのパス
    /// action:ボタンを押したときに行う処理
    /// </summary>
    /// <param name="pass">ScriptableObjectのパス</param>
    /// <param name="action">ボタンを押したときに行う処理</param>
    public static void SetWindow(string pass, Action action = null)
    {
        //表示するテキスト、ボタンの数、アイコンを管理するScriptableObjectを使用する
        SetConWinText_s setConWinText = GameObject.FindGameObjectWithTag("ConWin").GetComponent<SetConWinText_s>();
        ConWinData_s conWinData = Resources.Load<ConWinData_s>(pass); //データを読み込む
        setConWinText.SetText(conWinData); //使用するConWinDataをセット
        setConWinText.SetEvent(action); //ボタンを押したときのイベントをセット
        setConWinText.OpenWindow();
    } 

}
