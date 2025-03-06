using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField] GameObject setting;
    [SerializeField] GameObject title;
    [SerializeField] GameObject help;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SE;

    public void OnSetting()
    {
        title.SetActive(false);
        setting.SetActive(true);
    }

    public void BackTitle()
    {
        title.SetActive(true);
        setting.SetActive(false);
    }

    public void volBGM(float input)
    {
        BGM.volume = input / 10;
    }

    public void volSE(float input)
    {
        SE.volume = input / 10;
    }

    public void SwitchHelpImg(bool b)
    {
        help.SetActive(b);
    }
}
