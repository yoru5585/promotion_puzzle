using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    //画面中央に表示するテキストアニメーション
    [SerializeField] Animator stageAnim;
    //生成するオブジェクト
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;
    public bool IsGameover { get; set; }

    public void ShowResult()
    {
        //ゲームオーバーと表示
        Debug.Log("gameover");
        menu.SetActive(false);
        game.SetActive(false);
        stageAnim.gameObject.GetComponent<TextMeshProUGUI>().text = "Gameover...";
        stageAnim.SetTrigger("GameoverTrigger");
        
    }
}
