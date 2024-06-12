using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDatas : MonoBehaviour
{
    //�X�e�[�W�̏����܂Ƃ߂����X�g(�}�X�^�[�f�[�^)
    public List<StageData> stageDataList = new List<StageData>();
    //�X�e�[�W�̐�
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
