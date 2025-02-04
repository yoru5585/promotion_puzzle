using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Square
{
    //マス目の状態
    public enum SquareState
    {
        None,
        Player,
        Enemy,
        Block,
        Error
    }

    //アルファベット列
    public int alphabet;
    //数字行
    public int num;
    //状態
    public SquareState state;
    //ゲーム上の場所
    public Vector3 position;

    public Square(int alphabet, int num, SquareState state)
    {
        this.alphabet = alphabet;
        this.num = num;
        this.state = state;
    }
}
