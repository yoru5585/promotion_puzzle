using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Delay
{
    /// <summary>
    /// w’èŠÔ•ª‘Ò‚Á‚Ä‚©‚çˆ—‚ğÀs‚·‚é
    /// </summary>
    /// <param name="waitTime"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}