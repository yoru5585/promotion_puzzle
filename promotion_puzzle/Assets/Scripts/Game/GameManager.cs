using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�^�[��
    enum gameState
    {
        enter,
        player,
        interval,
        enemy,
        end
    }

    gameState state = gameState.enter;
    public bool IsGameStop = true;

    PlayerController playerController;
    GoalManager goalManager; 
    StageDatas stageDatas;

    private void Awake()
    {
        //�}�X�^�[�f�[�^�����[�h
        stageDatas = GetComponent<StageDatas>();
        stageDatas.LoadData();
        Debug.Log("a");
        
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        goalManager = GetComponent<GoalManager>();
    }

    void Update()
    {
        if (IsGameStop) return;

        switch (state)
        {
            case gameState.enter:
                //Debug.Log("�^�[���J�n");
                //�ړ��\�ȃp�l���𐶐�
                playerController.ShowMovable();

                state = gameState.player;
                break;
            case gameState.player:
                //Debug.Log("�v���C���[�^�[��");
                if (Input.GetMouseButtonDown(0))
                {
                    //�N���b�N�����I�u�W�F�N�g���`�F�b�N
                    if (playerController.CheckClickedObject())
                    {
                        //�v���C���[�𓮂���
                        playerController.Move();
                        //�ړ��\�ȃp�l��������
                        playerController.DestroyMovable();

                        state = gameState.interval;
                    }
                }
                break;
            case gameState.interval:
                //Debug.Log("�S�[���`�F�b�N");
                if (goalManager.CheckGoal())
                {
                    //�v���C���[�I�u�W�F�N�g���N�C�[���ɒu��������
                    playerController.ReplaceGoalPlayer();

                    if (goalManager.CheckClear(playerController.GetCurrentPlayerNum()))
                    {
                        //�N���A
                        goalManager.ShowResult();
                        IsGameStop = true;
                    }
                }

                state = gameState.enemy;
                break;
            case gameState.enemy:
                //Debug.Log("�G�^�[��");


                state = gameState.end;
                break;
            case gameState.end:
                //Debug.Log("�^�[���I��");


                state = gameState.enter;
                break;
            default:
                break;
        }
    }
}
