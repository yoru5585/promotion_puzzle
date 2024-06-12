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

    void SetUI()
    {
        stageTotalNum = stageDatas.GetStageNum();
        //0番目は既にあるので個別に入れる
        origin.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"STAGE 1";
        origin.GetComponent<Button>().onClick.AddListener(() => 
        {
            stageSelect.SetActive(false);
            gameStart.OnGameStart();
        });

        for (int i = 1; i < stageTotalNum; i++)
        {
            GameObject obj = Instantiate(origin, parent);
            obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"STAGE {i + 1}";
            int capturedIndex = i; // 新しい変数を作成し、ループの現在の値をキャプチャ
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                stageSelect.SetActive(false);
                gameStart.OnGameStart(capturedIndex);
            });
        }
        
    } 
}
