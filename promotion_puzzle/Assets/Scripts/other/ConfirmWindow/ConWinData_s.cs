using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Confirm")]
public class ConWinData_s : ScriptableObject
{
    [Header("確認内容"), TextArea(0,6)] public string confirmText;
    [Header("ボタンのテキスト")] public string [] buttonText;
}
