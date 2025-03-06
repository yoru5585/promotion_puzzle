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
    EnemyManager enemyManager;
    GameoverManager gameoverManager;

    float time = 0;
    float interval = 1.5f;

    private void Awake()
    {
        //�}�X�^�[�f�[�^�����[�h
        stageDatas = GetComponent<StageDatas>();
        stageDatas.LoadData();
        
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        goalManager = GetComponent<GoalManager>();
        enemyManager = GetComponentInChildren<EnemyManager>();
        gameoverManager = GetComponent<GameoverManager>();
    }

    void Update()
    {
        //�f�o�b�O�p
        if (Input.GetKeyDown("a"))
        {
            goalManager.ShowResult();
            state = gameState.enter;
            IsGameStop = true;
        }

        if (IsGameStop) return;
        
        switch (state)
        {
            case gameState.enter:
                //Debug.Log("�^�[���J�n");
                playerController.ResetSelectedPlayer();
                state = gameState.player;

                break;
            case gameState.player:
                //Debug.Log("�v���C���[�^�[��");
                if (Input.GetMouseButtonDown(0))
                {
                    //�N���b�N�����I�u�W�F�N�g���`�F�b�N
                    if (playerController.CheckClickedObject())
                    {
                        //�N���b�N�����}�X���`�F�b�N
                        if (playerController.CheckPlayerSquare())
                        {
                            //�ړ��\�ȃp�l��������
                            playerController.DestroyMovable();
                            //�I�������v���C���[�ňړ��\�ȃp�l���𐶐�
                            playerController.ShowMovableToSelectedPlayer();
                        }
                        else if (playerController.CheckMovable())
                        {
                            //�v���C���[�𓮂���
                            playerController.Move();
                            //�ړ��\�ȃp�l��������
                            playerController.DestroyMovable();

                            state = gameState.interval;
                        }
                        
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
                        state = gameState.enter;
                        IsGameStop = true;
                        return;
                    }
                }

                state = gameState.enemy;
                break;
            case gameState.enemy:
                //Debug.Log("�G�^�[��");
                time += Time.deltaTime;
                if (time > interval)
                {
                    time = 0;
                    enemyManager.MoveAllEnemy();
                    state = gameState.end;
                }
                break;
            case gameState.end:
                //Debug.Log("�^�[���I��");

                //�Q�[���I�[�o�[�`�F�b�N
                if (gameoverManager.IsGameover)
                {
                    gameoverManager.ShowResult();
                    IsGameStop = true;
                }

                state = gameState.enter;
                break;
            default:
                break;
        }
    }
}
