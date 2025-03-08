using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Windows;

public enum State
{
    o,
    壁,
    ゴール,
    白P,
    黒R,
    黒L,
    黒T
}
public class StageEditManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text selectedText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Transform parent;
    List<ButtonInfo> squareList = new List<ButtonInfo>();
    string selectedFileName;
    string activeFileName;
    StageData editStageData;
    StageDatas stageDatas;
    // Start is called before the first frame update
    void Start()
    {
        stageDatas = GetComponent<StageDatas>();
        editStageData = ScriptableObject.CreateInstance<StageData>();
        setup();
        DispStageFiles();
    }

    void setup()
    {
        squareList.Clear();
        selectedFileName = null;
        selectedText.text = selectedFileName;
        int count = 0;
        foreach (Transform obj in parent)
        {
            int x = count % 8;
            int y = (int)count / 8;

            ButtonInfo bi = obj.gameObject.GetComponent<ButtonInfo>();
            bi.Init(new Vector2(x, y), State.o);
            squareList.Add(bi);
            count++;
        }
        Debug.Log("セットアップ完了しました。");
    }
    //画面上のデータを、エディット用StageDataに書き込み
    void ParseToStageData()
    {
        string stgaeName = editStageData.stageName;
        string fileName = editStageData.fileName;
        editStageData = ScriptableObject.CreateInstance<StageData>();
        foreach (var squ in squareList)
        {
            switch (squ.state)
            {
                case State.壁:
                    editStageData.blockOriginSqu.Add(squ.pos);
                    break;
                case State.白P:
                    PlayerSquare ps = new();
                    ps.currentAlphabet = (int)squ.pos.x;
                    ps.currentNum = (int)squ.pos.y;
                    editStageData.playerOriginSqu.Add(ps);
                    break;
                case State.ゴール:
                    GoalSquare gs = new();
                    gs.Alphabet = (int)squ.pos.x;
                    gs.Num = (int)squ.pos.y;
                    editStageData.goalOriginSqu.Add(gs);
                    break;
                case State.黒R:
                    EnemySquare es_r = new();
                    es_r.currentPos = squ.pos;
                    editStageData.ruiterOriginSqu.Add(es_r);
                    break;
                case State.黒L:
                    EnemySquare es_l = new();
                    es_l.currentPos = squ.pos;
                    editStageData.loperOriginSqu.Add(es_l);
                    break;
                case State.黒T:
                    EnemySquare es_t = new();
                    es_t.currentPos = squ.pos;
                    editStageData.trmOriginSqu.Add(es_t);
                    break;
                default:
                    break;
            }
        }
        editStageData.stageName = stgaeName;
        editStageData.fileName = fileName;
    }
    //エディット用StageDataを、画面上のデータに書き込み
    void ParseToGUI()
    {
        foreach (var data in editStageData.playerOriginSqu)
        {
            Vector2 tmp = new Vector2(data.currentAlphabet, data.currentNum);
            ButtonInfo bi = squareList.Find(s => s.pos == tmp);
            if (bi != null)
            {
                bi.state = State.白P;
                bi.ChangeText();
            }
        }

        foreach (var data in editStageData.goalOriginSqu)
        {
            Vector2 tmp = new Vector2(data.Alphabet, data.Num);
            ButtonInfo bi = squareList.Find(s => s.pos == tmp);
            if (bi != null)
            {
                bi.state = State.ゴール;
                bi.ChangeText();
            }
        }

        foreach (var data in editStageData.blockOriginSqu)
        {
            ButtonInfo bi = squareList.Find(s => s.pos == data);
            if (bi != null)
            {
                bi.state = State.壁;
                bi.ChangeText();
            }
        }

        List<EnemySquare> tmpList = new();
        tmpList.AddRange(editStageData.ruiterOriginSqu);
        foreach (var data in tmpList)
        {
            ButtonInfo bi = squareList.Find(s => s.pos == data.currentPos);
            if (bi != null)
            {
                bi.state = State.黒R;
                bi.ChangeText();
            }
        }
        tmpList.Clear();

        tmpList.AddRange(editStageData.loperOriginSqu);
        foreach (var data in tmpList)
        {
            ButtonInfo bi = squareList.Find(s => s.pos == data.currentPos);
            if (bi != null)
            {
                bi.state = State.黒L;
                bi.ChangeText();
            }
        }
        tmpList.Clear();

        tmpList.AddRange(editStageData.trmOriginSqu);
        foreach (var data in tmpList)
        {
            ButtonInfo bi = squareList.Find(s => s.pos == data.currentPos);
            if (bi != null)
            {
                bi.state = State.黒T;
                bi.ChangeText();
            }
        }
    }
    //画面上のデータを削除
    public void ClearGUIData()
    {
        foreach (var data in squareList)
        {
            data.state = State.o;
            data.ChangeText();
        }
    }
    //フォルダ内のデータを一覧表示
    public void DispStageFiles()
    {
        List<string> optionList = new List<string>();
        string[] names = stageDatas.GetDirectoryFileName();
        activeFileName = "";
        foreach (string name in names)
        {
            activeFileName += $"{name}\n";
            optionList.Add(name);
        }
        text.text = activeFileName;
        dropdown.ClearOptions();
        dropdown.AddOptions(optionList);

    }
    //新規ファイル作成
    public void OnAddButtonClicked()
    {
        string input = inputField.text + ".json";
        if (activeFileName.Contains(input)|| input == null || input.Equals("")) return;

        selectedFileName = input;
        selectedText.text = input;
        ParseToStageData();
        editStageData.fileName = selectedFileName;
        editStageData.stageName = selectedFileName;
        stageDatas.SaveDataToJson(input, editStageData);
        DispStageFiles();
    }
    //ファイルをロード
    public void OnLoadButtonClicked()
    {
        //string input = inputField.text;
        string input = dropdown.options[dropdown.value].text;
        if (!CheckName(input)) return;

        setup();
        selectedFileName = input;
        selectedText.text = input;
        editStageData = stageDatas.LoadDataFromName(input);
        ParseToGUI();

    }
    //編集中ファイルをセーブ
    public void OnSaveButtonClicked()
    {
        if (!CheckName(selectedFileName)) return;
        ParseToStageData();
        stageDatas.SaveDataToJson(selectedFileName, editStageData);
    }
    //選択したファイルを削除
    public void OnDeleteButtonClicked()
    {
        //string input = inputField.text;
        string input = dropdown.options[dropdown.value].text;
        if (!CheckName(input)) return;

        stageDatas.DeleteData(input);
        selectedFileName = null;
        selectedText.text = selectedFileName;
        ClearGUIData();
        DispStageFiles();
    }

    bool CheckName(string name)
    {
        return (activeFileName.Contains(name) && name != null && !name.Equals(""));
    }

    public void LoadMainScene()
    {
        FadeManager.Instance.LoadScene("MainScene", 1.0f);
    }
}
