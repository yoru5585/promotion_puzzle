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

    private void Awake()
    {
        //マスターデータをロード
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
                //Debug.Log("ターン開始");
                //移動可能なパネルを生成
                playerController.ShowMovable();

                state = gameState.player;
                break;
            case gameState.player:
                //Debug.Log("プレイヤーターン");
                if (Input.GetMouseButtonDown(0))
                {
                    //クリックしたオブジェクトをチェック
                    if (playerController.CheckClickedObject())
                    {
                        //プレイヤーを動かす
                        playerController.Move();
                        //移動可能なパネルを消す
                        playerController.DestroyMovable();

                        state = gameState.interval;
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
                        IsGameStop = true;
                    }
                }

                state = gameState.enemy;
                break;
            case gameState.enemy:
                //Debug.Log("敵ターン");


                state = gameState.end;
                break;
            case gameState.end:
                //Debug.Log("ターン終了");


                state = gameState.enter;
                break;
            default:
                break;
        }
    }
}
