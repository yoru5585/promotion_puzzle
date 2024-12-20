using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Delay
{
    /// <summary>
    /// 指定時間分待ってから処理を実行する
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