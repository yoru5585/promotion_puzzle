using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Confirm")]
public class ConWinData_s : ScriptableObject
{
    [Header("�m�F���e"), TextArea(0,6)] public string confirmText;
    [Header("�{�^���̃e�L�X�g")] public string [] buttonText;
}
