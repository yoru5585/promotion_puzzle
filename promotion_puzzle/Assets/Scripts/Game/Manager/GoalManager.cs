using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class GoalSquare
{
    //アルファベット行
    public int Alphabet;
    //数字列
    public int Num;
    //ゴールのエフェクト
    public GameObject Effect;

    // MemberwiseCloneメソッドを使用
    public GoalSquare Clone()
    {
        // Object型で返ってくるのでキャストが必要
        return (GoalSquare)MemberwiseClone();
    }
}
public class GoalManager : MonoBehaviour
{
    //ゴールの情報をまとめたリスト
    [SerializeField] List<GoalSquare> goalList = new List<GoalSquare>();
    //ゴールのオブジェクトのリスト
    List<GameObject> goalObjList = new List<GameObject>();
    //画面中央に表示するテキストアニメーション
    [SerializeField] Animator stageAnim;
    //生成するオブジェクト
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
