using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using System.IO;

public class StageDatas : MonoBehaviour
{
    //ステージの情報をまとめたリスト(マスターデータ)
    public List<StageData> stageDataList = new List<StageData>();
    //ステージの数
    int stageNum;
    //ステージの保存場所
    string folderPath;

    //Resourcesファイルに入っていたデータをjson化
    void SaveFromScriObjToJson()
    {
        Debug.Log("存在しないので作成します！");
        Directory.CreateDirectory(folderPath);
        Debug.Log("フォルダを作成しました: " + folderPath);
        StageData[] datas = Resources.LoadAll<StageData>("StageDatas");
        foreach (StageData data in datas)
        {
            //jsonに変換
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText($"{folderPath}\\{data.name}.json", json);
            Debug.Log($"Saved JSON to: {folderPath}\\{data.name}.json");
        }
    }

    //Awakeで処理される
    public void LoadAllDataToList()
    {
        folderPath = Application.persistentDataPath + "\\StageDatas";
        //存在するなら処理しない
        if (!Directory.Exists(folderPath))
        {
            SaveFromScriObjToJson();
        }

        stageDataList.Clear();
        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");
        foreach (string jsonFile in jsonFiles)
        {
            //空のデータを生成しておく
            StageData myData = ScriptableObject.CreateInstance<StageData>();
            //jsonをロード
            string jsonText = File.ReadAllText(jsonFile);
            //jsonをStageData型に変換
            JsonUtility.FromJsonOverwrite(jsonText, myData);
            //リストに追加
            stageDataList.Add(myData);
        }
        stageNum = stageDataList.Count;
    }

    public StageData LoadDataFromName(string fileName)
    {
        folderPath = Application.persistentDataPath + "\\StageDatas";
        string jsonText = File.ReadAllText($"{folderPath}\\{fileName}");
        //空のデータを生成しておく
        StageData myData = ScriptableObject.CreateInstance<StageData>();
        //jsonをロード
        JsonUtility.FromJsonOverwrite(jsonText, myData);
        Debug.Log($"Loaded JSON to: {folderPath}\\{fileName}");
        return myData;
    }

    public void SaveDataToJson(string fileName, StageData myData)
    {
        //jsonに変換
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
