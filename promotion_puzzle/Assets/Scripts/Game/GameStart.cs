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

    }
    public void OnGameStart(int stageNum = 0)
    {
        //�{�[�h��\��
        title.SetActive(false);
        objects.SetActive(true);
        titleCamera.ChangeCamera();

        //�����ŃX�e�[�W�̏���ǂݍ���
        Debug.Log(stageNum);
        currentStageNum = stageNum;
        objAnim.SetTrigger("RotateTrigger");
    }

    public void StageSetting()
    {
        //�Q�[���̏����ݒ�
        squareController.Init();
        playerController.Init(currentStageNum);
        goalManager.Init(currentStageNum);
        menu.SetActive(true);
        game.SetActive(true);
        gameManager.IsGameStop = false;
    }

    public void ObjAnimEnd()
    {
        ShowStageText();
    }

    public void GameContinue()
    {
        //�����Ď��̃X�e�[�W�Ɍ������p
        currentStageNum++;
        //���̃X�e�[�W�����݂��Ȃ��ꍇ�V�[�������[�h
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
        stageAnim.gameObject.GetComponent<TextMeshProUGUI>().text = $"STAGE {currentStageNum + 1}";
        stageAnim.SetTrigger("StageTrigger");
    }
}