using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    //マスターデータ

    //プレイヤーの初期位置
    public PlayerSquare[] playerOriginSqu;
    //ゴールの位置
    public GoalSquare[] goalOriginSqu;
    //ブロックの位置
    public Vector2[] blockOriginSqu;
    //ナイトの初期位置
    public EnemySquare[] ruiterOriginSqu;
}
