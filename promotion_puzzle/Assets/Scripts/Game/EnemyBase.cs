using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyBase
{
    public void SearchMovableSqu();
    public int[] GetNearestPlayerSqu();
    public void Move();
}
