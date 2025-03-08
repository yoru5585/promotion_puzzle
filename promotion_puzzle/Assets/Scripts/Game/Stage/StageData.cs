using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/CreateStageData")]
public class StageData : ScriptableObject
{
    //�}�X�^�[�f�[�^

    //ID(�t�@�C����)
    public string fileName;
    //�X�e�[�W��
    [TextArea(0,2)]public string stageName;
    //�v���C���[�̏����ʒu
    public List<PlayerSquare> playerOriginSqu = new();
    //�S�[���̈ʒu
    public List<GoalSquare> goalOriginSqu = new();
    //�u���b�N�̈ʒu
    public List<Vector2> blockOriginSqu = new();
    //�i�C�g�̏����ʒu
    public List<EnemySquare> ruiterOriginSqu = new();
    //�r�V���b�v�̏����ʒu
    public List<EnemySquare> loperOriginSqu = new();
    //���[�N�̏����ʒu
    public List<EnemySquare> trmOriginSqu = new();
}
