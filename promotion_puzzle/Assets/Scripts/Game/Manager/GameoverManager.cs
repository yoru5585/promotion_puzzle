using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    //��ʒ����ɕ\������e�L�X�g�A�j���[�V����
    [SerializeField] Animator stageAnim;
    //��������I�u�W�F�N�g
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;
    public bool IsGameover { get; set; }

    public void ShowResult()
    {
        //�Q�[���I�[�o�[�ƕ\��
        Debug.Log("gameover");
        menu.SetActive(false);
        game.SetActive(false);
        stageAnim.gameObject.GetComponent<TextMeshProUGUI>().text = "Gameover...";
        stageAnim.SetTrigger("GameoverTrigger");
        
    }
}
