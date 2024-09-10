using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class SaveTransform : MonoBehaviour
{
    //csvの書き出し
    //https://qiita.com/tak001/items/e69029e9246d80d1279b

    [SerializeField] float saveInterval = 0.5f;
    [SerializeField] Transform carTrans = null;
    [SerializeField] Transform cameraTrans = null;
    [SerializeField] bool endFlag = false;

    List<string> saveStrList = new List<string>();

    StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {
        if (carTrans == null || cameraTrans == null)
        {
            Debug.Log("error");
            return;
        }

        // 新しくcsvファイルを作成して、{}の中の要素分csvに追記をする
        sw = new StreamWriter(@"Assets\Resources\TransformData.csv", false, Encoding.GetEncoding("Shift_JIS"));
        
        StartCoroutine(SaveRoutine());
    }

    IEnumerator SaveRoutine()
    {
        while (true)
        {
            if (endFlag)
            {
                foreach (var data in saveStrList)
                {
                    sw.WriteLine(data);
                }
                Debug.Log("writeLine_end");
                saveStrList.Clear();
                sw.Close();
                break;
            }

            //car( position x, y, z  rotation x, y, z, w ) camera( position x, y, z  rotation x, y, z, w ) 
            SaveTransformData();

            // 次のキャプチャまで待機
            yield return new WaitForSeconds(saveInterval);
        }
    }

    void SaveTransformData()
    {
        //car
        Vector3 carPos = carTrans.position;
        Quaternion carRot = carTrans.rotation;

        //camera
        Vector3 cameraPos = cameraTrans.localPosition;
        Quaternion cameraRot = cameraTrans.localRotation;

        float[] s1 = { carPos.x, carPos.y, carPos.z, carRot.x, carRot.y, carRot.z, carRot.w , 
            cameraPos.x, cameraPos.y, cameraPos.z, cameraRot.x, cameraRot.y, cameraRot.z, cameraRot.w };
        string s2 = string.Join(",", s1);
        Debug.Log(s2);
        saveStrList.Add(s2);
    }
}
