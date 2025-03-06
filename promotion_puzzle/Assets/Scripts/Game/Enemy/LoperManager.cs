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

    //�ړ��\�ȃ}�X�𒲍�
    public override void SearchMovableSqu(Vector2 targetVec2)
    {
        int x = (int)targetVec2.x;
        int z = (int)targetVec2.y;

        int px = 0;
        int pz = 0;

        movableList.Clear();

        int index = 0;

        //�r�V���b�v�͂ȂȂ߈ړ�
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
                //�ȂȂߕ����Ɉړ�
                capx += px;
                capz += pz;

                //Debug.Log($"{capx}*{capz}");

                //���݂��Ȃ��}�X�ڂɓ��B����ƒT���I��
                if (capx < 0 || capx > 7 || capz < 0 || capz > 7)
                {
                    //Debug.Log($"break���܂�");
                    break;
                }

                //�}�X�ɉ���������Έړ��ł��Ȃ��̂Ŏ��̃}�X�ɂ���
                if (squareController.SquareArray[capx, capz].state == Square.SquareState.Enemy
                    || squareController.SquareArray[capx, capz].state == Square.SquareState.Block)
                {
                    //Debug.Log($"���̃}�X�ɂ��܂�");
                    continue;
                }

                //�Ԃ�l�Ɋi�[
                movableList.Add(new Vector2(capx, capz));
                //Debug.Log($"�i�[���܂�");
            }
        }
    }
}
