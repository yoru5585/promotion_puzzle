using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
    public List<EnemyMonoBehaviour> EnemyList = new List<EnemyMonoBehaviour>();

    public void Start()
    {
        EnemyList.AddRange(GetComponents<EnemyMonoBehaviour>());
    }

    public void Init(int stageNum)
    {
        foreach (var enemy in EnemyList)
        {
            enemy.Init(stageNum);
        }
    }

    public void MoveAllEnemy()
    {
        foreach (var enemy in EnemyList)
        {
            enemy.Move();
        }
    }

    public void DestroyAllEnemy()
    {
        foreach (var enemy in EnemyList)
        {
            enemy.EnemyDestroy();
        }
    }
}
