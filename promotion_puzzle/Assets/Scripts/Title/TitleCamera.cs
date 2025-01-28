using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTrans;
    bool isStop = false;
    void Update()
    {
        if (isStop) return;
        // オブジェクトを回転させる
        cameraTrans.Rotate(Vector3.up, 2.0f * Time.deltaTime);
    }

    public void ChangeCamera()
    {
        cameraTrans.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        isStop = true;
    }
}
