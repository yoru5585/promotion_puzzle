using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiterController : MonoBehaviour, EnemyBase
{
    //�G�̃i�C�g
    List<int[]> RuiterList = new List<int[]>();
    //�ړ��\���W
    List<int[]> MovableList = new List<int[]>();

    SquareController squareController;

    private void Start()
    {
        squareController = GetComponent<SquareController>();
    }

    public void SearchMovableSqu()
    {

    }

    public int[] GetNearestPlayerSqu()
    {
        //�v���C���[�̍��W������
        //�ړ��\���W�̒�����ł��߂����W��I��
        //�S�Ẵv���C���[�ɑ΂��čł��߂����W��Ԃ�
        List<int[]> result = new List<int[]>();
        foreach (Square squ in squareController.SquareArray)
        {
            //�v���C���[�̏ꍇ
            if (squ.state == Square.SquareState.Player)
            {
                //if (result == null)
                //{
                //    result = new int[2];
                //    result[0] = squ.alphabet;
                //    result[1] = squ.num;
                //}
            }
        }

        return null;
    }

    public void Move()
    {
        SearchMovableSqu();
    }
}
