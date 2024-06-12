using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    //�A���t�@�x�b�g
    string[] alphabet = {"A", "B", "C", "D", "E", "F", "G", "H"};
    //�S�Ẵ}�X�ڂ̏�Ԃ��Ǘ�
    public Square[,] SquareArray = new Square[8, 8];
    //�}�X�ڂ̐e�I�u�W�F�N�g
    [SerializeField] Transform squareParentTrans;

    public void Init()
    {
        //�Q�[���J�n���̏����ݒ�
        squareCreate();
    }

    //�}�X�ڂ̃R���C�_�[�𐶐�
    void squareCreate()
    {
        //A1���擾
        GameObject originObj = squareParentTrans.GetChild(0).gameObject;
        //���_
        Vector3 originPos = originObj.transform.localPosition;
        for (int z = 0; z < 8; z++)
        {
            for (int x = 0; x < 8; x++)
            {
                //���X�g�ɐݒ�
                Square squ = new Square(alphabet[x], z, 0);
                SquareArray[x, z] = squ;

                //�ꏊ��ݒ�
                squ.position = new Vector3(originPos.x + (x * 0.06f), 0, originPos.z + (z * 0.06f));

                //A1�͊��ɂ���̂Ŕ�΂�
                if (x == 0 && z == 0) continue;

                //�I�u�W�F�N�g�𐶐�
                GameObject obj = Instantiate(originObj, squareParentTrans);
                obj.transform.localPosition = squ.position;
                obj.name = $"{x} {z}";
            }
        }
    }
}
