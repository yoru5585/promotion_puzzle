using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RuiterController : EnemyMonoBehaviour
{
    public override void SetEnemyList(int stageNum)
    {
        enemyList.Clear();
        if (stageDatas.stageDataList[stageNum].ruiterOriginSqu != null)
        {
            foreach (EnemySquare origin in stageDatas.stageDataList[stageNum].ruiterOriginSqu)
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
            if (squareController.SquareArray[x + px, z + pz].state == Square.SquareState.Enemy 
                || squareController.SquareArray[x + px, z + pz].state == Square.SquareState.Block)
            {
                continue;
            }

            //返り値に格納
            movableList.Add(new Vector2(x + px, z + pz));
        }
    }
    
}
