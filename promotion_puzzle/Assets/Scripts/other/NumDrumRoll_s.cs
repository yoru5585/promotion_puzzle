using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class NumDrumRoll_s
{
    /// <summary>
    /// ”š‚ğƒhƒ‰ƒ€ƒ[ƒ‹®‚Å•\¦‚·‚é
    /// </summary>
    /// <param name="text"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static IEnumerator OnNumDrumRoll(TextMeshProUGUI text, int min, int max)
    {
        for (int i = min; i <= max; i += 10)
        {
            text.text = i.ToString("N0");
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("numdrumroll comp");
    }
}
