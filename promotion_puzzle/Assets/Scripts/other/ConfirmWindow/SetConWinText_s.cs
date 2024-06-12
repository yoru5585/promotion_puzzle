using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SetConWinText_s : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject originButton;
    [SerializeField] GameObject panel;
    List<GameObject> createButtons = new List<GameObject>();

    public void SetText(ConWinData_s conWinData)
    {
        mainText.text = conWinData.confirmText; //確認内容をセット
        buttonText.text = conWinData.buttonText[0]; //一つ目のボタンのテキストをセット
        for (int i = 1; i < conWinData.buttonText.Length; i++) //二つ以上ボタンを使用する場合
        {
            GameObject obj = Instantiate(originButton, originButton.transform.parent); //ボタンを生成
            createButtons.Add(obj); //オブジェクトをリストに追加
            obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = conWinData.buttonText[i]; //テキストをセット
        }
    }

    public void SetEvent(Action action = null)
    {
        if (action != null) //特に何もイベントがない場合はウィンドウを閉じるだけ
        {
            originButton.GetComponent<Button>().onClick.AddListener(() => action.Invoke());
            //現状「はい」を押したときのイベントしか用意しない。
            //複数ボタンでそれぞれ独自のイベントを発生させたくなったら新たに作る。
        }

    }

    public void OpenWindow()
    {
        panel.SetActive(true); //ウィンドウを開く
    }

    public void CloseWindow()
    {
        panel.SetActive(false); //ウィンドウを閉じる
        originButton.GetComponent<Button>().onClick.RemoveAllListeners(); //ボタンのイベントをリムーブ
        DeleteAddButton();
    }

    void DeleteAddButton()
    {
        foreach (GameObject obj in createButtons)
        {
            Destroy(obj); //生成したボタンを削除
        }
        createButtons.Clear(); //リストを初期化
    }
}
