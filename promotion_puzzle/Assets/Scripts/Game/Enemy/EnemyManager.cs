using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySquare
{
    //�A���t�@�x�b�g�s
    //������
    public Vector2 currentPos;
    //�G�I�u�W�F�N�g
    public GameObject currentObj;
    public EnemySquare Clone()
    {
        // Object�^�ŕԂ��Ă���̂ŃL���X�g���K�v
        return (EnemySquare)MemberwiseClone();
    }
}

public class EnemyManager : MonoBehaviour
{
    public List<EnemyBase> enemyBaseList = new List<EnemyBase>();

    public void Start()
    {
        enemyBaseList.AddRange(GetComponents<EnemyBase>());
    }

    public void Init(int stageNum)
    {
        foreach (var enemyBase in enemyBaseList)
        {
            enemyBase.Init(stageNum);
        }
    }

    public void MoveAllEnemy()
    {
        foreach (var enemyBase in enemyBaseList)
        {
            enemyBase.Move();
        }
    }

    public void DestroyAllEnemy()
    {
        foreach (var enemyBase in enemyBaseList)
        {
            enemyBase.EnemyDestroy();
        }
    }
}
