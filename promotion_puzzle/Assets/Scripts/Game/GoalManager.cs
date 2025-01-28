using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class GoalSquare
{
    //�A���t�@�x�b�g�s
    public int Alphabet;
    //������
    public int Num;
    //�S�[���̃G�t�F�N�g
    public GameObject Effect;

    // MemberwiseClone���\�b�h���g�p
    public GoalSquare Clone()
    {
        // Object�^�ŕԂ��Ă���̂ŃL���X�g���K�v
        return (GoalSquare)MemberwiseClone();
    }
}
public class GoalManager : MonoBehaviour
{
    //�S�[���̏����܂Ƃ߂����X�g
    [SerializeField] List<GoalSquare> goalList = new List<GoalSquare>();
    //�S�[���̃I�u�W�F�N�g�̃��X�g
    List<GameObject> goalObjList = new List<GameObject>();
    //��ʒ����ɕ\������e�L�X�g�A�j���[�V����
    [SerializeField] Animator stageAnim;
    //��������I�u�W�F�N�g
    [SerializeField] GameObject goalPrefab;
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;

    SquareController squareController;
    StageDatas stageDatas;

    private void Start()
    {
        squareController = GetComponent<SquareController>();
        stageDatas = GetComponent<StageDatas>();
    }

    public void Init(int stageNum)
    {
        SetGoalList(stageNum);
        GoalCreate();
        
    }

    void GoalCreate()
    {
        foreach (GoalSquare goal in goalList)
        {
            GameObject obj = Instantiate(goalPrefab);
            obj.transform.position = squareController.SquareArray[goal.Alphabet, goal.Num].position;
            goal.Effect = obj.transform.GetChild(0).gameObject;
            goalObjList.Add(obj);
        }
    }

    public void GoalDeatroy()
    {
        foreach (GameObject goal in goalObjList)
        {
            Destroy(goal); 
        }
        goalObjList.Clear();
    }

    public bool CheckGoal()
    {
        foreach (GoalSquare goal in goalList)
        {
            if (squareController.SquareArray[goal.Alphabet, goal.Num].state == Square.SquareState.Player)
            {
                Destroy(goal.Effect);
                goalList.Remove(goal);
                squareController.SquareArray[goal.Alphabet, goal.Num].state = Square.SquareState.Block;
                return true;
            }
        }

        return false;
    }

    public void ShowResult()
    {
        menu.SetActive(false);
        game.SetActive(false);
        stageAnim.gameObject.GetComponent<TextMeshProUGUI>().text = "Clear!";
        stageAnim.SetTrigger("ClearTrigger");
    }

    public void SetGoalList(int stageNum)
    {
        goalList.Clear();
        foreach (GoalSquare origin in stageDatas.stageDataList[stageNum].goalOriginSqu)
        {
            goalList.Add(origin.Clone());
        }
    }

    public bool CheckClear(int playerNum)
    {
        if (playerNum == 0)
        {
            return true;
        }
        return false;
    }
}
