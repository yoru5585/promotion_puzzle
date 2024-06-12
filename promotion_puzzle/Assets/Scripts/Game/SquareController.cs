using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    //アルファベット
    string[] alphabet = {"A", "B", "C", "D", "E", "F", "G", "H"};
    //全てのマス目の状態を管理
    public Square[,] SquareArray = new Square[8, 8];
    //マス目の親オブジェクト
    [SerializeField] Transform squareParentTrans;

    public void Init()
    {
        //ゲーム開始時の初期設定
        squareCreate();
    }

    //マス目のコライダーを生成
    void squareCreate()
    {
        //A1を取得
        GameObject originObj = squareParentTrans.GetChild(0).gameObject;
        //原点
        Vector3 originPos = originObj.transform.localPosition;
        for (int z = 0; z < 8; z++)
        {
            for (int x = 0; x < 8; x++)
            {
                //リストに設定
                Square squ = new Square(alphabet[x], z, 0);
                SquareArray[x, z] = squ;

                //場所を設定
                squ.position = new Vector3(originPos.x + (x * 0.06f), 0, originPos.z + (z * 0.06f));

                //A1は既にあるので飛ばす
                if (x == 0 && z == 0) continue;

                //オブジェクトを生成
                GameObject obj = Instantiate(originObj, squareParentTrans);
                obj.transform.localPosition = squ.position;
                obj.name = $"{x} {z}";
            }
        }
    }
}
