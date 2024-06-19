using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveToPosAnim
{
    public static IEnumerator StartAnim(Vector3 targetPos, Transform objectToMove, float seconds = 0.6f)
    {
        float elapsedTime = 0;
        Vector3 startPos = objectToMove.position;
        Vector3 endPos = targetPos;

        while (elapsedTime < seconds)
        {
            //Debug.Log(elapsedTime);
            Vector3 tmp = Vector3.Lerp(startPos, endPos, (elapsedTime / seconds));
            objectToMove.position = new Vector3(tmp.x, 0.01f, tmp.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectToMove.position = endPos;
        GameObject.Find("SE").GetComponent<AudioSource>().Play();
    }
}
