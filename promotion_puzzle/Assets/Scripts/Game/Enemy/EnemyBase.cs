using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMonoBehaviour : MonoBehaviour
{
    protected GameoverManager gameoverManager;
    protected SquareController squareController;
    protected StageDatas stageDatas;

    //敵の位置をまとめたリスト
    protected List<EnemySquare> enemyList = new List<EnemySquare>();
    //移動可能マス
    protected List<Vector2> movableList = new List<Vector2>();
    //生成する敵のPrefab
    [SerializeField] protected GameObject prefab;

    private void Start()
    {
        gameoverManager = GetComponentInParent<GameoverManager>();
        squareController = GetComponentInParent<SquareController>();
        stageDatas = GetComponentInParent<StageDatas>();
    }
    public void Init(int stageNum)
    {
        SetEnemyList(stageNum);
        EnemyCreate();
    }
    public abstract void SetEnemyList(int stageNum);
    //敵を生成
    public void EnemyCreate()
    {
        foreach (EnemySquare enemy in enemyList)
        {
            squareController.SquareArray[(int)enemy.currentPos.x, (int)enemy.currentPos.y].state = Square.SquareState.Enemy;
            GameObject obj = Instantiate(prefab);
            Vector3 pos = squareController.SquareArray[(int)enemy.currentPos.x, (int)enemy.currentPos.y].position;
            obj.transform.localPosition = pos;
            enemy.currentObj = obj;
        }
    }
    //敵を削除
    public void EnemyDestroy()
    {
        foreach (EnemySquare enemy in enemyList)
        {
            Destroy(enemy.currentObj);
        }
    }
    //移動可能なマスを調査
    public abstract void SearchMovableSqu(Vector2 targetVec2);

    //最もプレイヤーに近いマスを取得
    public Vector2 GetNearestPlayerSqu()
    {
        //プレイヤーの座標を検索
        float minDistance = 100;
        Vector2 minVec2 = Vector2.zero;
        foreach (Square squ in squareController.SquareArray)
        {
            //プレイヤーの場合
            if (squ.state == Square.SquareState.Player)
            {
                foreach (Vector2 mov in movableList)
                {
                    //二点間距離を計算
                    Vector2 playerVec2 = new Vector2(squ.alphabet, squ.num);
                    float tmp = Vector2.Distance(playerVec2, mov);
                    if (tmp < minDistance)
                    {
                        minDistance = tmp;
                        minVec2 = mov;
                        //Debug.Log(minVec2);
                    }
                }

            }
        }

        //全てのプレイヤーに対して最も近い座標を返す
        return minVec2;
    }

    public void Move()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            SearchMovableSqu(enemyList[i].currentPos);
            Vector2 nearestSqu = GetNearestPlayerSqu();

            //全てのマスを管理している二次元配列を書き換える
            squareController
                .SquareArray
                [
                    (int)enemyList[i].currentPos.x,
                    (int)enemyList[i].currentPos.y
                ]
                .state = Square.SquareState.None;
            //書き込み先マスがPlayerであればゲームオーバー
            if (squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].state == Square.SquareState.Player)
            {
                gameoverManager.IsGameover = true;
            }
            else
            {
                squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].state = Square.SquareState.Enemy;
            }

            //敵リストを更新
            enemyList[i].currentPos = nearestSqu;

            //フロントエンド処理
            //プレイヤーを移動
            Transform trans = enemyList[i].currentObj.transform;
            Vector3 endPos = squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].position;
            StartCoroutine(MoveToPosAnim.StartAnim(endPos, trans));
        }
    }
}
