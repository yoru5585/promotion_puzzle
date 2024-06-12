using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] GameObject credit;
    [SerializeField] GameObject title;

    public void ShowCredit()
    {
        credit.SetActive(true);
        title.SetActive(false);
    }

    public void BackTitle()
    {
        credit.SetActive(false);
        title.SetActive(true);
    }
}
