using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    //プレイヤーの初期位置(マスターデータ)
    public PlayerSquare[] playerOriginSqu;
    //ゴールの位置(マスターデータ)
    public GoalSquare[] goalOriginSqu;
}
