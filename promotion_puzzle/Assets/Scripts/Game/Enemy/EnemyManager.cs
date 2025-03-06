using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
