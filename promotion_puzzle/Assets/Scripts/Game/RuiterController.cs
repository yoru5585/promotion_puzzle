using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuiterController : MonoBehaviour, EnemyBase
{
    //敵のナイト
    List<int[]> RuiterList = new List<int[]>();
    //移動可能座標
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
        //プレイヤーの座標を検索
        //移動可能座標の中から最も近い座標を選択
        //全てのプレイヤーに対して最も近い座標を返す
        List<int[]> result = new List<int[]>();
        foreach (Square squ in squareController.SquareArray)
        {
            //プレイヤーの場合
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
