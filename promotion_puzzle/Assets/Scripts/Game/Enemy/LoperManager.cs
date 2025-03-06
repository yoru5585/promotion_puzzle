using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoperManager : EnemyMonoBehaviour
{
    public override void SetEnemyList(int stageNum)
    {
        enemyList.Clear();
        if (stageDatas.stageDataList[stageNum].loperOriginSqu != null)
        {
            foreach (EnemySquare origin in stageDatas.stageDataList[stageNum].loperOriginSqu)
            {
                enemyList.Add(origin.Clone());
            }
        }

    }

    //移動可能なマスを調査
    public override void SearchMovableSqu(Vector2 targetVec2)
    {
        int x = (int)targetVec2.x;
        int z = (int)targetVec2.y;

        int px = 0;
        int pz = 0;

        movableList.Clear();

        int index = 0;

        //ビショップはななめ移動
        while (index < 4)
        {
            switch (index)
            {
                case 0:
                    px = 1;
                    pz = 1;
                    break;
                case 1:
                    px = 1;
                    pz = -1;
                    break;
                case 2:
                    px = -1;
                    pz = 1;
                    break;
                case 3:
                    px = -1;
                    pz = -1;
                    break;
            }

            index++;

            int capx = x;
            int capz = z;

            for (int i = 0; i < 2; i++)
            {
                //ななめ方向に移動
                capx += px;
                capz += pz;

                //Debug.Log($"{capx}*{capz}");

                //存在しないマス目に到達すると探索終了
                if (capx < 0 || capx > 7 || capz < 0 || capz > 7)
                {
                    //Debug.Log($"breakします");
                    break;
                }

                //マスに何かがあれば移動できないので次のマスにする
                if (squareController.SquareArray[capx, capz].state == Square.SquareState.Enemy
                    || squareController.SquareArray[capx, capz].state == Square.SquareState.Block)
                {
                    //Debug.Log($"次のマスにします");
                    continue;
                }

                //返り値に格納
                movableList.Add(new Vector2(capx, capz));
                //Debug.Log($"格納します");
            }
        }
    }
}
