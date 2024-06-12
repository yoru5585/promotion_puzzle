using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    //�v���C���[�̏����ʒu(�}�X�^�[�f�[�^)
    public PlayerSquare[] playerOriginSqu;
    //�S�[���̈ʒu(�}�X�^�[�f�[�^)
    public GoalSquare[] goalOriginSqu;
}
