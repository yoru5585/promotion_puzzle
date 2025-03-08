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
    ��,
    �S�[��,
    ��P,
    ��R,
    ��L,
    ��T
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
        Debug.Log("�Z�b�g�A�b�v�������܂����B");
    }
    //��ʏ�̃f�[�^���A�G�f�B�b�g�pStageData�ɏ�������
    void ParseToStageData()
    {
        string stgaeName = editStageData.stageName;
        string fileName = editStageData.fileName;
        editStageData = ScriptableObject.CreateInstance<StageData>();
        foreach (var squ in squareList)
        {
            switch (squ.state)
            {
                case State.��:
                    editStageData.blockOriginSqu.Add(squ.pos);
                    break;
                case State.��P:
                    PlayerSquare ps = new();
                    ps.currentAlphabet = (int)squ.pos.x;
                    ps.currentNum = (int)squ.pos.y;
                    editStageData.playerOriginSqu.Add(ps);
                    break;
                case State.�S�[��:
                    GoalSquare gs = new();
                    gs.Alphabet = (int)squ.pos.x;
                    gs.Num = (int)squ.pos.y;
                    editStageData.goalOriginSqu.Add(gs);
                    break;
                case State.��R:
                    EnemySquare es_r = new();
                    es_r.currentPos = squ.pos;
                    editStageData.ruiterOriginSqu.Add(es_r);
                    break;
                case State.��L:
                    EnemySquare es_l = new();
                    es_l.currentPos = squ.pos;
                    editStageData.loperOriginSqu.Add(es_l);
                    break;
                case State.��T:
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
    //�G�f�B�b�g�pStageData���A��ʏ�̃f�[�^�ɏ�������
    void ParseToGUI()
    {
        foreach (var data in editStageData.playerOriginSqu)
        {
            Vector2 tmp = new Vector2(data.currentAlphabet, data.currentNum);
            ButtonInfo bi = squareList.Find(s => s.pos == tmp);
            if (bi != null)
            {
                bi.state = State.��P;
                bi.ChangeText();
            }
        }

        foreach (var data in editStageData.goalOriginSqu)
        {
            Vector2 tmp = new Vector2(data.Alphabet, data.Num);
            ButtonInfo bi = squareList.Find(s => s.pos == tmp);
            if (bi != null)
            {
                bi.state = State.�S�[��;
                bi.ChangeText();
            }
        }

        foreach (var data in editStageData.blockOriginSqu)
        {
            ButtonInfo bi = squareList.Find(s => s.pos == data);
            if (bi != null)
            {
                bi.state = State.��;
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
                bi.state = State.��R;
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
                bi.state = State.��L;
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
                bi.state = State.��T;
                bi.ChangeText();
            }
        }
    }
    //��ʏ�̃f�[�^���폜
    public void ClearGUIData()
    {
        foreach (var data in squareList)
        {
            data.state = State.o;
            data.ChangeText();
        }
    }
    //�t�H���_���̃f�[�^���ꗗ�\��
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
    //�V�K�t�@�C���쐬
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
    //�t�@�C�������[�h
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
    //�ҏW���t�@�C�����Z�[�u
    public void OnSaveButtonClicked()
    {
        if (!CheckName(selectedFileName)) return;
        ParseToStageData();
        stageDatas.SaveDataToJson(selectedFileName, editStageData);
    }
    //�I�������t�@�C�����폜
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
