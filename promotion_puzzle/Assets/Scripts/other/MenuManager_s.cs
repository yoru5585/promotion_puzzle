using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager_s : MonoBehaviour
{
    [SerializeField] GameObject menuBG;
    [SerializeField] UnityEvent openEvent;
    [SerializeField] UnityEvent closeEvent;


    public void OpenMenu()
    {
        openEvent?.Invoke();
        menuBG.SetActive(true);
    }

    public void CloseMenu()
    {
        closeEvent?.Invoke();
        menuBG.SetActive(false);
    }

    public void MainSceneButton()
    {
        ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData0", () =>
        {
            menuBG.SetActive(false);
            FadeManager.Instance.LoadScene("MainScene", 3.0f);
        });
    }
}
