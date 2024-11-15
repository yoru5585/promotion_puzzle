using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class PlayerSquare
{
    //�A���t�@�x�b�g�s
    public int currentAlphabet;
    //������
    public int currentNum;
    //�v���C���[�I�u�W�F�N�g
    public GameObject currentObj;
    //�S�[��������
    public bool IsGoal;
    public PlayerSquare Clone()
    {
        // Object�^�ŕԂ��Ă���̂ŃL���X�g���K�v
        return (PlayerSquare)MemberwiseClone();
    }
}
public class PlayerController : MonoBehaviour
{
    //�v���C���[�̏����܂Ƃ߂����X�g
    [SerializeField] List<PlayerSquare> playerList = new List<PlayerSquare>();
    //��������v���C���[��prefab
    [SerializeField] GameObject playerPrefab;
    //�ړ��\�ȃp�l����prefab
    [SerializeField] GameObject movablePrefab;
    //�ړ��\�ȃp�l���̃��X�g
    List<GameObject> movableList = new List<GameObject>();
    //�I�������}�X��
    int selectedAlph, selectedNum;
    //�I�������v���C���[
    int selectedPlayer = -1;


    SquareController squareController;
    StageDatas stageDatas;


    private void Start()
    {
        squareController = GetComponent<SquareController>();
        stageDatas = GetComponent<StageDatas>();
    }

    public bool CheckClickedObject()
    {
        //hit�����I�u�W�F�N�g
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.collider.gameObject;
        }

        if (hitObject == null)
        {
            return false;
        }

        if (hitObject.tag == "Square")
        {
            string name = hitObject.transform.parent.name;
            Debug.Log(name);
            selectedAlph = Array.IndexOf(alphabet, name.Split(" ")[0]);
            selectedNum = int.Parse(name.Split(" ")[1]);
            return true;
        }

        return false;
    }

    //�N���b�N�����I�u�W�F�N�g���v���C���[�����ׂ�
    //�Ώۂ̃v���C���[��ݒ�
    public bool CheckPlayerSquare()
    {
        foreach (PlayerSquare player in playerList)
        {
            //�S�[�����Ă���v���C���[�͔�΂�
            if (player.IsGoal)
            {
                continue;
            }

            //�v���C���[�𒲍�
            if (player.currentAlphabet == selectedAlph && player.currentNum == selectedNum)
            {
                //�I�𒆂̃v���C���[��ݒ�
                selectedPlayer = playerList.IndexOf(player);
                return true;
            }
        }
        return false;
    }

    //�N���b�N�����I�u�W�F�N�g���ړ��\�p�l�������ׂ�
    public bool CheckMovable()
    {
        if (selectedPlayer == -1) return false;
        
        //�I�𒆂̃v���C���[���擾
        PlayerSquare player = playerList[selectedPlayer];

        List<int[]> around = SearchFourSquaresAround(selectedAlph, selectedNum);

        foreach (int[] squ in around)
        {
            //�v���C���[�𒲍�
            if (player.currentAlphabet == squ[0] && player.currentNum == squ[1])
            {
                return true;
            }
        }

        return false;
    }

    public void Move()
    {
        //�S�Ẵ}�X���Ǘ����Ă���񎟌��z�������������
        squareController
            .SquareArray
            [
                playerList[selectedPlayer].currentAlphabet,
                playerList[selectedPlayer].currentNum
            ]
            .state = Square.SquareState.None;
        squareController.SquareArray[selectedAlph, selectedNum].state = Square.SquareState.Player;

        //�v���C���[���X�g���X�V
        playerList[selectedPlayer].currentAlphabet = selectedAlph;
        playerList[selectedPlayer].currentNum = selectedNum;

        //�t�����g�G���h����
        //�v���C���[���ړ�
        Transform trans = playerList[selectedPlayer].currentObj.transform;
        Vector3 endPos = squareController.SquareArray[selectedAlph, selectedNum].position;
        StartCoroutine(MoveToPosAnim.StartAnim(endPos, trans));
        //playerList[selectedPlayer].currentObj.transform.position = squareController.SquareArray[selectedAlph, selectedNum].position;
        //Debug.Log("end");
    }

    public void Init(int stageNum)
    {
        SetPlayerList(stageNum);
        PlayerCreate();
        
    }

    //�Q�[���J�n���Ƀv���C���[�I�u�W�F�N�g�𐶐�
    void PlayerCreate()
    {
        foreach (PlayerSquare player in playerList)
        {
            squareController.SquareArray[player.currentAlphabet, player.currentNum].state = Square.SquareState.Player;
            GameObject obj = Instantiate(playerPrefab);
            Vector3 pos = squareController.SquareArray[player.currentAlphabet, player.currentNum].position;
            obj.transform.localPosition = pos;
            player.currentObj = obj;
        }
    }

    //�ړ��\�ȃp�l���𐶐�(���ׂẴv���C���[)
    public void ShowMovableAllPlayer()
    {
        foreach (PlayerSquare player in playerList)
        {
            //�S�[�����Ă���I�u�W�F�N�g�͔�΂�
            if (player.IsGoal)
            {
                continue;
            }

            int x = player.currentAlphabet;
            int z = player.currentNum;

            List<int[]> around = SearchFourSquaresAround(x, z);

            foreach (int[] squ in around)
            {
                //�ړ��\�ȃp�l���𐶐�
                GameObject obj = Instantiate(movablePrefab);
                Vector3 pos = squareController.SquareArray[squ[0], squ[1]].position;
                obj.transform.localPosition = pos;
                movableList.Add(obj);
            }
        }
    }

    //�ړ��\�ȃp�l���𐶐�(�v���C���[�w��)
    public void ShowMovableToSelectedPlayer()
    {
        //�I�𒆂̃v���C���[���擾
        PlayerSquare player = playerList[selectedPlayer];

        //�S�[�����Ă���I�u�W�F�N�g�͔�΂�
        if (player.IsGoal)
        {
            return;
        }

        int x = player.currentAlphabet;
        int z = player.currentNum;

        List<int[]> around = SearchFourSquaresAround(x, z);

        foreach (int[] squ in around)
        {
            //�}�X�ɉ���������Έړ��ł��Ȃ��̂Ŏ��̃}�X�ɂ���
            Debug.Log(squareController.SquareArray[squ[0], squ[1]].state);
            if (squareController.SquareArray[squ[0], squ[1]].state != Square.SquareState.None)
            {
                continue;
            }

            //�ړ��\�ȃp�l���𐶐�
            GameObject obj = Instantiate(movablePrefab);
            Vector3 pos = squareController.SquareArray[squ[0], squ[1]].position;
            obj.transform.localPosition = pos;
            movableList.Add(obj);
        }
    }

    //�ړ��\�ȃp�l�����폜
    public void DestroyMovable()
    {
        foreach (GameObject obj in movableList)
        {
            Destroy(obj);
        }
        //���X�g���N���A
        movableList.Clear();
    }

    public void DestroyPlayer()
    {
        foreach (PlayerSquare player in playerList)
        {
            Destroy(player.currentObj);
        }
    }

    //����4�}�X���擾
    List<int[]> SearchFourSquaresAround(int targetX, int targetY)
    {
        int x = targetX;
        int z = targetY;

        int px = 0;
        int pz = 0;

        List<int[]> result = new List<int[]>();

        int index = 0;

        while (index < 4)
        {
            switch (index)
            {
                case 0:
                    px = 1;
                    pz = 0;
                    break;
                case 1:
                    px = 0;
                    pz = 1;
                    break;
                case 2:
                    px = -1;
                    pz = 0;
                    break;
                case 3:
                    px = 0;
                    pz = -1;
                    break;
            }

            index++;

            //���݂��Ȃ��}�X�ڂ͔�΂�
            if (x + px < 0 || x + px > 7 || z + pz < 0 || z + pz > 7)
            {
                continue;
            }

            //�u���b�N�͔�΂�
            if (squareController.SquareArray[x + px, z + pz].state == Square.SquareState.Block)
            {
                continue;
            }

            //�Ԃ�l�Ɋi�[
            int[] tmp = { x + px, z + pz };
            result.Add(tmp);
        }

        return result;
    }

    public void ReplaceGoalPlayer()
    {
        playerList[selectedPlayer].currentObj.transform.GetChild(1).gameObject.SetActive(true);
        playerList[selectedPlayer].currentObj.transform.GetChild(0).gameObject.SetActive(false);
        playerList[selectedPlayer].IsGoal = true;
    }

    public void SetPlayerList(int stageNum)
    {
        playerList.Clear();
        foreach (PlayerSquare origin in stageDatas.stageDataList[stageNum].playerOriginSqu)
        {
            playerList.Add(origin.Clone());
        }
    }

    public int GetCurrentPlayerNum()
    {
        int num = 0;
        foreach (PlayerSquare player in playerList)
        {
            if (player.IsGoal)
            {
                continue;
            }
            num++;
        }
        return num;
    }

    public void ResetSelectedPlayer()
    {
        //�v���C���[�̑I����Ԃ�����
        selectedPlayer = -1;
    }
}
