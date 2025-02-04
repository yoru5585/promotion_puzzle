using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    [SerializeField] Animator objAnim;
    [SerializeField] Animator stageAnim;
    [SerializeField] GameObject objects;
    [SerializeField] GameObject title;
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;

    SquareController squareController;
    PlayerController playerController;
    GameManager gameManager;
    TitleCamera titleCamera;
    GoalManager goalManager;
    CameraController cameraController;
    StageDatas stageDatas;
    EnemyManager enemyManager;
    GameoverManager gameoverManager;


    int currentStageNum;

    private void Start()
    {
        squareController = GetComponent<SquareController>();
        playerController = GetComponent<PlayerController>();
        gameManager = GetComponent<GameManager>();
        titleCamera = GetComponent<TitleCamera>();
        goalManager = GetComponent<GoalManager>();
        cameraController = GetComponent<CameraController>();
        stageDatas = GetComponent<StageDatas>();
        enemyManager = GetComponentInChildren<EnemyManager>();
        gameoverManager = GetComponent<GameoverManager>();
    }
    public void OnGameStart(int stageNum = 0)
    {
        //ボードを表示
        title.SetActive(false);
        objects.SetActive(true);
        titleCamera.ChangeCamera();

        //ここでステージの情報を読み込み
        Debug.Log(stageNum);
        currentStageNum = stageNum;
        objAnim.SetTrigger("RotateTrigger");
    }

    public void StageSetting()
    {
        //ゲームの初期設定
        squareController.Init();
        playerController.Init(currentStageNum);
        goalManager.Init(currentStageNum);
        enemyManager.Init(currentStageNum);
        menu.SetActive(true);
        game.SetActive(true);
        gameManager.IsGameStop = false;
        gameoverManager.IsGameover = false;
    }

    public void ObjAnimEnd()
    {
        ShowStageText();
    }

    public void GameReload()
    {
        //ステージをリロードする用
        StartCoroutine(Delay.DelayMethod(2f, () =>
        {
            ShowStageText();
            goalManager.GoalDeatroy();
        }));
    }

    public void GameContinue()
    {
        //続けて次のステージに向かう用
        currentStageNum++;
        //次のステージが存在しない場合シーンリロード
        if (currentStageNum >= stageDatas.GetStageNum())
        {
            FadeManager.Instance.LoadScene("MainScene", 3.0f);
            return;
        }
        StartCoroutine(Delay.DelayMethod(2f, () => 
        {
            ShowStageText();
            goalManager.GoalDeatroy();
        }));
    }

    void ShowStageText()
    {
        cameraController.Stop();
        playerController.DestroyPlayer();
        enemyManager.DestroyAllEnemy();
        stageAnim.gameObject.GetComponent<TextMeshProUGUI>().text = $"STAGE {currentStageNum + 1}";
        stageAnim.SetTrigger("StageTrigger");
    }
}
