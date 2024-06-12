using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager_s : MonoBehaviour
{
    string filePath;
    SaveData_s save;
    //[SerializeField] PlayerInfo_s playerInfo;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        Debug.Log(filePath);
        save = new SaveData_s();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SaveData_s>(data);

            GetData();
            Debug.Log("ロードしました。");
            return;
        }

        Debug.Log("データがないのでロードできませんでした。");
    }

    public void Save()
    {
        SetData();
        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
        Debug.Log("セーブしました。");
    }

    public void SetData()
    {
        //save.playerName = playerInfo.playerName;
        //save.gatyaPoint = playerInfo.gatyaPoint;
    }

    public void GetData()
    {
        //playerInfo.playerName = save.playerName;
        //playerInfo.gatyaPoint = save.gatyaPoint;
    }

    public void DeleteData()
    {
        File.Delete(filePath);
        Debug.Log("デリートしました。");
    }

    public bool CheckFileExists()
    {
        return File.Exists(filePath);
    }
}

