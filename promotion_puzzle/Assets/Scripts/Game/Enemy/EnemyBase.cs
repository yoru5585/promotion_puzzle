using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyBase
{
    public void Init(int stageNum);
    public void SetEnemyList(int stageNum);
    public void EnemyCreate();
    public void EnemyDestroy();
    public void SearchMovableSqu(Vector2 v);
    public Vector2 GetNearestPlayerSqu();
    public void Move();
}
