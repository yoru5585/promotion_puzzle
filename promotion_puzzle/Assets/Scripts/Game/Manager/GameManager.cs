using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ターン
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
        //マスターデータをロード
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
        //デバッグ用
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
                //Debug.Log("ターン開始");
                playerController.ResetSelectedPlayer();
                state = gameState.player;

                break;
            case gameState.player:
                //Debug.Log("プレイヤーターン");
                if (Input.GetMouseButtonDown(0))
                {
                    //クリックしたオブジェクトをチェック
                    if (playerController.CheckClickedObject())
                    {
                        //クリックしたマスをチェック
                        if (playerController.CheckPlayerSquare())
                        {
                            //移動可能なパネルを消す
                            playerController.DestroyMovable();
                            //選択したプレイヤーで移動可能なパネルを生成
                            playerController.ShowMovableToSelectedPlayer();
                        }
                        else if (playerController.CheckMovable())
                        {
                            //プレイヤーを動かす
                            playerController.Move();
                            //移動可能なパネルを消す
                            playerController.DestroyMovable();

                            state = gameState.interval;
                        }
                        
                    }
                }
                break;
            case gameState.interval:
                //Debug.Log("ゴールチェック");
                if (goalManager.CheckGoal())
                {
                    //プレイヤーオブジェクトをクイーンに置き換える
                    playerController.ReplaceGoalPlayer();

                    if (goalManager.CheckClear(playerController.GetCurrentPlayerNum()))
                    {
                        //クリア
                        goalManager.ShowResult();
                        state = gameState.enter;
                        IsGameStop = true;
                        return;
                    }
                }

                state = gameState.enemy;
                break;
            case gameState.enemy:
                //Debug.Log("敵ターン");
                time += Time.deltaTime;
                if (time > interval)
                {
                    time = 0;
                    enemyManager.MoveAllEnemy();
                    state = gameState.end;
                }
                break;
            case gameState.end:
                //Debug.Log("ターン終了");

                //ゲームオーバーチェック
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
