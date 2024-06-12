using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Square
{
    //�}�X�ڂ̏��
    public enum SquareState
    {
        None,
        Player,
        Enemy,
        Block,
        Error
    }

    //�A���t�@�x�b�g��
    public string alphabet;
    //�����s
    public int num;
    //���
    public SquareState state;
    //�Q�[����̏ꏊ
    public Vector3 position;

    public Square(string alphabet, int num, SquareState state)
    {
        this.alphabet = alphabet;
        this.num = num;
        this.state = state;
    }
}
