using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveToPosAnim
{
    public static void StartAnim(Vector3 targetPos, Transform trans)
    {
        Vector3 startPos = trans.position;
        Vector3 endPos = targetPos;
        float speed = 5.0f;

        //二点間の距離を代入(スピード調整に使う)
        float distance_two = Vector3.Distance(startPos, endPos);
        float present_Location;
        int index = 0;
        while (index < 10000)
        {
            // 現在の位置
            present_Location = (Time.time * speed) / distance_two;
            // オブジェクトの移動
            trans.position = Vector3.Lerp(startPos, endPos, present_Location) * Time.deltaTime;
            
            index++;
            Debug.Log(present_Location);

            if (trans.position.Equals(targetPos))
            {
                break;
            }
        }
    }
}
