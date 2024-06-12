using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ConfirmWindow_s
{
    //�m�F��ʂ�\��
    /// <summary>
    /// �m�F�̂��߂̃|�b�v�A�b�v�E�B���h�E��\�����܂��B
    /// pass:ScriptableObject�̃p�X
    /// action:�{�^�����������Ƃ��ɍs������
    /// </summary>
    /// <param name="pass">ScriptableObject�̃p�X</param>
    /// <param name="action">�{�^�����������Ƃ��ɍs������</param>
    public static void SetWindow(string pass, Action action = null)
    {
        //�\������e�L�X�g�A�{�^���̐��A�A�C�R�����Ǘ�����ScriptableObject���g�p����
        SetConWinText_s setConWinText = GameObject.FindGameObjectWithTag("ConWin").GetComponent<SetConWinText_s>();
        ConWinData_s conWinData = Resources.Load<ConWinData_s>(pass); //�f�[�^��ǂݍ���
        setConWinText.SetText(conWinData); //�g�p����ConWinData���Z�b�g
        setConWinText.SetEvent(action); //�{�^�����������Ƃ��̃C�x���g���Z�b�g
        setConWinText.OpenWindow();
    } 

}
