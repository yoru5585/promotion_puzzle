using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerSquare
{
    //アルファベット行
    public int currentAlphabet;
    //数字列
    public int currentNum;
    //プレイヤーオブジェクト
    public GameObject currentObj;
    //ゴールしたか
    public bool IsGoal;
    public PlayerSquare Clone()
    {
        // Object型で返ってくるのでキャストが必要
        return (PlayerSquare)MemberwiseClone();
    }
}
public class PlayerController : MonoBehaviour
{
    //プレイヤーの情報をまとめたリスト
    [SerializeField] List<PlayerSquare> playerList = new List<PlayerSquare>();
    //生成するプレイヤーのprefab
    [SerializeField] GameObject playerPrefab;
    //移動可能なパネルのprefab
    [SerializeField] GameObject movablePrefab;
    //移動可能なパネルのリスト
    List<GameObject> movableList = new List<GameObject>();
    //選択したマス目
    int selectedAlph, selectedNum;
    //選択したプレイヤー
    int selectedPlayer;


    SquareController squareController;
    StageDatas stageDatas;


    private void Start()
    {
        squareController = GetComponent<SquareController>();
        stageDatas = GetComponent<StageDatas>();
    }

    public bool CheckClickedObject()
    {
        //hitしたオブジェクト
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
            selectedAlph = int.Parse(name.Split(" ")[0]);
            selectedNum = int.Parse(name.Split(" ")[1]);
            return CheckMovable();
        }

        return false;
    }

    //クリックしたオブジェクトが移動可能パネルか調べる
    bool CheckMovable()
    {
        List<int[]> around = SearchFourSquaresAround(selectedAlph, selectedNum);

        foreach (int[] squ in around)
        {
            foreach (PlayerSquare player in playerList)
            {
                //ゴールしているプレイヤーは飛ばす
                if (player.IsGoal)
                {
                    continue;
                }

                //プレイヤーを調査
                if (player.currentAlphabet == squ[0] && player.currentNum == squ[1])
                {
                    //選択中のプレイヤーを設定
                    selectedPlayer = playerList.IndexOf(player);
                    return true;
                }
            }
        }

        return false;
    }

    public void Move()
    {
        //全てのマスを管理している二次元配列を書き換える
        squareController
            .SquareArray
            [
                playerList[selectedPlayer].currentAlphabet,
                playerList[selectedPlayer].currentNum
            ]
            .state = Square.SquareState.None;
        squareController.SquareArray[selectedAlph, selectedNum].state = Square.SquareState.Player;

        //プレイヤーリストを更新
        playerList[selectedPlayer].currentAlphabet = selectedAlph;
        playerList[selectedPlayer].currentNum = selectedNum;

        //フロントエンド処理
        //プレイヤーを移動
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

    //ゲーム開始時にプレイヤーオブジェクトを生成
    void PlayerCreate()
    {
        foreach (PlayerSquare player in playerList)
        {
            GameObject obj = Instantiate(playerPrefab);
            Vector3 pos = squareController.SquareArray[player.currentAlphabet, player.currentNum].position;
            obj.transform.localPosition = pos;
            player.currentObj = obj;
        }
    }

    //移動可能なパネルを生成
    public void ShowMovable()
    {
        foreach (PlayerSquare player in playerList)
        {
            //ゴールしているオブジェクトは飛ばす
            if (player.IsGoal)
            {
                continue;
            }

            int x = player.currentAlphabet;
            int z = player.currentNum;

            List<int[]> around = SearchFourSquaresAround(x, z);

            foreach (int[] squ in around)
            {
                //移動可能なパネルを生成
                GameObject obj = Instantiate(movablePrefab);
                Vector3 pos = squareController.SquareArray[squ[0], squ[1]].position;
                obj.transform.localPosition = pos;
                movableList.Add(obj);
            }
        }
    }

    //移動可能なパネルを削除
    public void DestroyMovable()
    {
        foreach (GameObject obj in movableList)
        {
            Destroy(obj);
        }
    }

    public void DestroyPlayer()
    {
        foreach (PlayerSquare player in playerList)
        {
            Destroy(player.currentObj);
        }
    }

    //周囲4マスを取得
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

            //存在しないマス目は飛ばす
            if (x + px < 0 || x + px > 7 || z + pz < 0 || z + pz > 7)
            {
                continue;
            }

            //ブロックは飛ばす
            if (squareController.SquareArray[x + px, z + pz].state == Square.SquareState.Block)
            {
                continue;
            }

            //返り値に格納
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
}
