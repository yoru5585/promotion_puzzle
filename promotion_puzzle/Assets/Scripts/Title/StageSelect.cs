using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject stageSelect;
    [SerializeField] GameObject title;
    [SerializeField] Transform parent;
    GameObject origin;
    int stageTotalNum;

    StageDatas stageDatas;
    GameStart gameStart;

    private void Start()
    {
        stageDatas = GetComponent<StageDatas>();
        gameStart = GetComponent<GameStart>();
        origin = parent.transform.GetChild(0).gameObject;
        
        SetUI();
    }

    public void ShowStageSelect()
    {
        stageSelect.SetActive(true);
        title.SetActive(false);
    }

    public void BackTitle()
    {
        stageSelect.SetActive(false);
        title.SetActive(true);
    }

    public void LoadCreativeScene()
    {
        FadeManager.Instance.LoadScene("CreativeScene", 1.0f);
    }

    void SetUI()
    {
        stageTotalNum = stageDatas.GetStageNum();
        //ステージ名を代入（0番目は既にあるので個別に入れる）
        string str = stageDatas.stageDataList[0].stageName;
        origin.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = (str == "") ? $"STAGE 1" : str;

        origin.GetComponent<Button>().onClick.AddListener(() => 
        {
            stageSelect.SetActive(false);
            gameStart.OnGameStart();
        });

        for (int i = 1; i < stageTotalNum; i++)
        {
            GameObject obj = Instantiate(origin, parent);
            str = stageDatas.stageDataList[i].stageName;
            obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = (str == "") ? $"STAGE {i + 1}" : str;
            int capturedIndex = i; // 新しい変数を作成し、ループの現在の値をキャプチャ
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                stageSelect.SetActive(false);
                gameStart.OnGameStart(capturedIndex);
            });
        }
        
    } 
}
