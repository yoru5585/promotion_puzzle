using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDatas : MonoBehaviour
{
    //ステージの情報をまとめたリスト(マスターデータ)
    public List<StageData> stageDataList = new List<StageData>();
    //ステージの数
    int stageNum; 

    public void LoadData()
    {
        stageDataList.Clear();
        StageData[] datas = Resources.LoadAll<StageData>("StageDatas");
        foreach (StageData data in datas)
        {
            stageDataList.Add(data);
        }
        stageNum = stageDataList.Count;
    }

    public int GetStageNum()
    {
        return stageNum;
    }
}
