using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RuiterController : MonoBehaviour, EnemyBase
{
    //敵のナイトの位置をまとめたリスト
    List<EnemySquare> ruiterList = new List<EnemySquare>();
    //移動可能マス
    List<Vector2> movableList = new List<Vector2>();
    //生成する敵のPrefab
    [SerializeField] GameObject ruiterPrefab;

    SquareController squareController;
    StageDatas stageDatas;

    private void Start()
    {
        squareController = GetComponent<SquareController>();
        stageDatas = GetComponent<StageDatas>();
    }

    public void Init(int stageNum)
    {
        SetEnemyList(stageNum);
        EnemyCreate();
    }

    public void SetEnemyList(int stageNum)
    {
        ruiterList.Clear();
        if (stageDatas.stageDataList[stageNum].ruiterOriginSqu != null)
        {
            foreach (EnemySquare origin in stageDatas.stageDataList[stageNum].ruiterOriginSqu)
            {
                ruiterList.Add(origin.Clone());
            }
        }
        
    }
    //敵を生成
    public void EnemyCreate()
    {
        foreach (EnemySquare ruiter in ruiterList)
        {
            squareController.SquareArray[(int)ruiter.currentPos.x, (int)ruiter.currentPos.y].state = Square.SquareState.Enemy;
            GameObject obj = Instantiate(ruiterPrefab);
            Vector3 pos = squareController.SquareArray[(int)ruiter.currentPos.x, (int)ruiter.currentPos.y].position;
            obj.transform.localPosition = pos;
            ruiter.currentObj = obj;
        }
    }
    //移動可能なマスを調査
    public void SearchMovableSqu(Vector2 targetVec2)
    {
        int x = (int)targetVec2.x;
        int z = (int)targetVec2.y;

        int px = 0;
        int pz = 0;

        movableList.Clear();

        int index = 0;

        //ナイトは最大八箇所から選択可能
        while (index < 8)
        {
            switch (index)
            {
                case 0:
                    px = 2;
                    pz = 1;
                    break;
                case 1:
                    px = 2;
                    pz = -1;
                    break;
                case 2:
                    px = -2;
                    pz = 1;
                    break;
                case 3:
                    px = -2;
                    pz = -1;
                    break;
                case 4:
                    px = 1;
                    pz = 2;
                    break;
                case 5:
                    px = -1;
                    pz = 2;
                    break;
                case 6:
                    px = 1;
                    pz = -2;
                    break;
                case 7:
                    px = -1;
                    pz = -2;
                    break;
            }

            index++;

            //存在しないマス目は飛ばす
            if (x + px < 0 || x + px > 7 || z + pz < 0 || z + pz > 7)
            {
                continue;
            }

            //マスに何かがあれば移動できないので次のマスにする
            if (squareController.SquareArray[x + px, z + pz].state == Square.SquareState.Enemy)
            {
                continue;
            }

            //返り値に格納
            movableList.Add(new Vector2(x + px, z + pz));
        }
    }
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
                    }
                }
                
            }
        }

        //全てのプレイヤーに対して最も近い座標を返す
        return minVec2;
    }

    public void Move()
    {
        for (int i = 0; i < ruiterList.Count; i++)
        {
            SearchMovableSqu(ruiterList[i].currentPos);
            Vector2 nearestSqu = GetNearestPlayerSqu();

            //全てのマスを管理している二次元配列を書き換える
            squareController
                .SquareArray
                [
                    (int)ruiterList[i].currentPos.x,
                    (int)ruiterList[i].currentPos.y
                ]
                .state = Square.SquareState.None;
            squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].state = Square.SquareState.Enemy;

            //敵リストを更新
            ruiterList[i].currentPos = nearestSqu;

            //フロントエンド処理
            //プレイヤーを移動
            Transform trans = ruiterList[i].currentObj.transform;
            Vector3 endPos = squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].position;
            StartCoroutine(MoveToPosAnim.StartAnim(endPos, trans));
        }
    }
}
