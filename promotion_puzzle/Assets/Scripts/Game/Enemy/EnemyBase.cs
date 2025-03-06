using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMonoBehaviour : MonoBehaviour
{
    protected GameoverManager gameoverManager;
    protected SquareController squareController;
    protected StageDatas stageDatas;

    //�G�̈ʒu���܂Ƃ߂����X�g
    protected List<EnemySquare> enemyList = new List<EnemySquare>();
    //�ړ��\�}�X
    protected List<Vector2> movableList = new List<Vector2>();
    //��������G��Prefab
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
    //�G�𐶐�
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
    //�G���폜
    public void EnemyDestroy()
    {
        foreach (EnemySquare enemy in enemyList)
        {
            Destroy(enemy.currentObj);
        }
    }
    //�ړ��\�ȃ}�X�𒲍�
    public abstract void SearchMovableSqu(Vector2 targetVec2);

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
                        //Debug.Log(minVec2);
                    }
                }

            }
        }

        //�S�Ẵv���C���[�ɑ΂��čł��߂����W��Ԃ�
        return minVec2;
    }

    public void Move()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            SearchMovableSqu(enemyList[i].currentPos);
            Vector2 nearestSqu = GetNearestPlayerSqu();

            //�S�Ẵ}�X���Ǘ����Ă���񎟌��z�������������
            squareController
                .SquareArray
                [
                    (int)enemyList[i].currentPos.x,
                    (int)enemyList[i].currentPos.y
                ]
                .state = Square.SquareState.None;
            //�������ݐ�}�X��Player�ł���΃Q�[���I�[�o�[
            if (squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].state == Square.SquareState.Player)
            {
                gameoverManager.IsGameover = true;
            }
            else
            {
                squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].state = Square.SquareState.Enemy;
            }

            //�G���X�g���X�V
            enemyList[i].currentPos = nearestSqu;

            //�t�����g�G���h����
            //�v���C���[���ړ�
            Transform trans = enemyList[i].currentObj.transform;
            Vector3 endPos = squareController.SquareArray[(int)nearestSqu.x, (int)nearestSqu.y].position;
            StartCoroutine(MoveToPosAnim.StartAnim(endPos, trans));
        }
    }
}
