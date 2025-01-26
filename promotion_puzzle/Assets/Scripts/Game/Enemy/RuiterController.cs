using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RuiterController : MonoBehaviour, EnemyBase
{
    //�G�̃i�C�g�̈ʒu���܂Ƃ߂����X�g
    List<EnemySquare> ruiterList = new List<EnemySquare>();
    //�ړ��\�}�X
    List<Vector2> movableList = new List<Vector2>();
    //��������G��Prefab
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
    //�G�𐶐�
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
    //�ړ��\�ȃ}�X�𒲍�
    public void SearchMovableSqu(Vector2 targetVec2)
    {
        int x = (int)targetVec2.x;
        int z = (int)targetVec2.y;

        int px = 0;
        int pz = 0;

        movableList.Clear();

        int index = 0;

        //�i�C�g�͍ő唪�ӏ�����I���\
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

            //���݂��Ȃ��}�X�ڂ͔�΂�
            if (x + px < 0 || x + px > 7 || z + pz < 0 || z + pz > 7)
            {
                continue;
            }

            //�}�X�ɉ���������Έړ��ł��Ȃ��̂Ŏ��̃}�X�ɂ���
            if (squareController.SquareArray[x + px, z + pz].state == Square.SquareState.Enemy)
            {
                continue;
            }

            //�Ԃ�l�Ɋi�[
            movableList.Add(new Vector2(x + px, z + pz));
        }
    }
    //�ł��v���C���[�ɋ߂��}�X���擾
    public Vector2 GetNearestPlayerSqu()
    {
        //�v���C���[�̍��W������
        float minDistance = 100;
        Vector2 minVec2 = Vector2.zero;
        foreach (Square squ in squareController.SquareArray)
        {
            //�v���C���[�̏ꍇ
            if (squ.state == Square.SquareState.Player)
            {
                foreach (Vector2 mov in movableList)
                {
                    //��_�ԋ������v�Z
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

        //�S�Ẵv���C���[�ɑ΂��čł��߂����W��Ԃ�
        return minVec2;
    }

    public void Move()
    {
        for (int i = 0; i < ruiterList.Count; i++)
        {
            SearchMovableSqu(ruiterList[i].currentPos);
            Vector2 nearestSqu = GetNearestPlayerSqu();

            //�S�Ẵ}�X���Ǘ����Ă���񎟌��z�������������
            squareController
                .SquareArray
                [
                    (int)ruiterList[i].currentPos.x,
                    (int)ruiterList[i].currentPos.y
                ]
                .state = Square.SquareState.None;
            squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].state = Square.SquareState.Enemy;

            //�G���X�g���X�V
            ruiterList[i].currentPos = nearestSqu;

            //�t�����g�G���h����
            //�v���C���[���ړ�
            Transform trans = ruiterList[i].currentObj.transform;
            Vector3 endPos = squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].position;
            StartCoroutine(MoveToPosAnim.StartAnim(endPos, trans));
        }
    }
}
