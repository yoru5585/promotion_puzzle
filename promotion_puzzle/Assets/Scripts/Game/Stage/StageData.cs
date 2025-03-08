using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    //マスターデータ

    //ID(ファイル名)
    public string fileName;
    //ステージ名
    [TextArea(0,2)]public string stageName;
    //プレイヤーの初期位置
    public List<PlayerSquare> playerOriginSqu = new();
    //ゴールの位置
    public List<GoalSquare> goalOriginSqu = new();
    //ブロックの位置
    public List<Vector2> blockOriginSqu = new();
    //ナイトの初期位置
    public List<EnemySquare> ruiterOriginSqu = new();
    //ビショップの初期位置
    public List<EnemySquare> loperOriginSqu = new();
    //ルークの初期位置
    public List<EnemySquare> trmOriginSqu = new();
}
