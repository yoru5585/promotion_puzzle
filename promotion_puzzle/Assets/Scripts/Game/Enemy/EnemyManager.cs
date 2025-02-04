using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySquare
{
    //アルファベット行
    //数字列
    public Vector2 currentPos;
    //敵オブジェクト
    public GameObject currentObj;
    public EnemySquare Clone()
    {
        // Object型で返ってくるのでキャストが必要
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
