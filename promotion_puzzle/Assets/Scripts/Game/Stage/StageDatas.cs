using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using System.IO;

public class StageDatas : MonoBehaviour
{
    //�X�e�[�W�̏����܂Ƃ߂����X�g(�}�X�^�[�f�[�^)
    public List<StageData> stageDataList = new List<StageData>();
    //�X�e�[�W�̐�
    int stageNum;
    //�X�e�[�W�̕ۑ��ꏊ
    string folderPath;

    //Resources�t�@�C���ɓ����Ă����f�[�^��json��
    void SaveFromScriObjToJson()
    {
        Debug.Log("���݂��Ȃ��̂ō쐬���܂��I");
        Directory.CreateDirectory(folderPath);
        Debug.Log("�t�H���_���쐬���܂���: " + folderPath);
        StageData[] datas = Resources.LoadAll<StageData>("StageDatas");
        foreach (StageData data in datas)
        {
            //json�ɕϊ�
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText($"{folderPath}\\{data.name}.json", json);
            Debug.Log($"Saved JSON to: {folderPath}\\{data.name}.json");
        }
    }

    //Awake�ŏ��������
    public void LoadAllDataToList()
    {
        folderPath = Application.persistentDataPath + "\\StageDatas";
        //���݂���Ȃ珈�����Ȃ�
        if (!Directory.Exists(folderPath))
        {
            SaveFromScriObjToJson();
        }

        stageDataList.Clear();
        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");
        foreach (string jsonFile in jsonFiles)
        {
            //��̃f�[�^�𐶐����Ă���
            StageData myData = ScriptableObject.CreateInstance<StageData>();
            //json�����[�h
            string jsonText = File.ReadAllText(jsonFile);
            //json��StageData�^�ɕϊ�
            JsonUtility.FromJsonOverwrite(jsonText, myData);
            //���X�g�ɒǉ�
            stageDataList.Add(myData);
        }
        stageNum = stageDataList.Count;
    }

    public StageData LoadDataFromName(string fileName)
    {
        folderPath = Application.persistentDataPath + "\\StageDatas";
        string jsonText = File.ReadAllText($"{folderPath}\\{fileName}");
        //��̃f�[�^�𐶐����Ă���
        StageData myData = ScriptableObject.CreateInstance<StageData>();
        //json�����[�h
        JsonUtility.FromJsonOverwrite(jsonText, myData);
        Debug.Log($"Loaded JSON to: {folderPath}\\{fileName}");
        return myData;
    }

    public void SaveDataToJson(string fileName, StageData myData)
    {
        //json�ɕϊ�
        string json = JsonUtility.ToJson(myData, true);
        File.WriteAllText($"{folderPath}\\{fileName}", json);
        Debug.Log($"Saved JSON to: {folderPath}\\{fileName}");
    }

    public void DeleteData(string fileName)
    {
        File.Delete($"{folderPath}\\{fileName}");
    }

    public string[] GetDirectoryFileName()
    {
        folderPath = Application.persistentDataPath + "\\StageDatas";
        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");
        for (int i = 0; i < jsonFiles.Length; i++)
        {
            string tmp = jsonFiles[i];
            jsonFiles[i] = tmp.Remove(0, folderPath.Length + 1);
            Debug.Log(jsonFiles[i]);
        }
        return jsonFiles;
    }

    public int GetStageNum()
    {
        return stageNum;
    }
}
