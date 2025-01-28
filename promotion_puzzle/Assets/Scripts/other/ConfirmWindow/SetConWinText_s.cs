using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SetConWinText_s : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject originButton;
    [SerializeField] GameObject panel;
    List<GameObject> createButtons = new List<GameObject>();

    public void SetText(ConWinData_s conWinData)
    {
        mainText.text = conWinData.confirmText; //�m�F���e���Z�b�g
        buttonText.text = conWinData.buttonText[0]; //��ڂ̃{�^���̃e�L�X�g���Z�b�g
        for (int i = 1; i < conWinData.buttonText.Length; i++) //��ȏ�{�^�����g�p����ꍇ
        {
            GameObject obj = Instantiate(originButton, originButton.transform.parent); //�{�^���𐶐�
            createButtons.Add(obj); //�I�u�W�F�N�g�����X�g�ɒǉ�
            obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = conWinData.buttonText[i]; //�e�L�X�g���Z�b�g
        }
    }

    public void SetEvent(Action action = null)
    {
        if (action != null) //���ɉ����C�x���g���Ȃ��ꍇ�̓E�B���h�E����邾��
        {
            originButton.GetComponent<Button>().onClick.AddListener(() => action.Invoke());
            //����u�͂��v���������Ƃ��̃C�x���g�����p�ӂ��Ȃ��B
            //�����{�^���ł��ꂼ��Ǝ��̃C�x���g�𔭐����������Ȃ�����V���ɍ��B
        }

    }

    public void OpenWindow()
    {
        panel.SetActive(true); //�E�B���h�E���J��
    }

    public void CloseWindow()
    {
        panel.SetActive(false); //�E�B���h�E�����
        originButton.GetComponent<Button>().onClick.RemoveAllListeners(); //�{�^���̃C�x���g�������[�u
        DeleteAddButton();
    }

    void DeleteAddButton()
    {
        foreach (GameObject obj in createButtons)
        {
            Destroy(obj); //���������{�^�����폜
        }
        createButtons.Clear(); //���X�g��������
    }
}
