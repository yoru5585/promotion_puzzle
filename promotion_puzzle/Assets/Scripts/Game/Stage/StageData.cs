using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    //�}�X�^�[�f�[�^

    //�v���C���[�̏����ʒu
    public PlayerSquare[] playerOriginSqu;
    //�S�[���̈ʒu
    public GoalSquare[] goalOriginSqu;
    //�u���b�N�̈ʒu
    public Vector2[] blockOriginSqu;
    //�i�C�g�̏����ʒu
    public EnemySquare[] ruiterOriginSqu;
}
