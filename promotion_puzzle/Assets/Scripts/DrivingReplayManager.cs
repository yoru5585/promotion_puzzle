using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DrivingReplayManager : MonoBehaviour
{
    // CSVデータを保存するためのリスト
    List<string[]> csvData = new List<string[]>();
    [SerializeField] float moveInterval = 0.5f;
    [SerializeField] Transform carTrans;
    [SerializeField] Transform cameraTrans;
    [SerializeField] bool endFlag = false;
    [SerializeField] bool pauseFlag = true;
    [SerializeField] Slider replaySlider;
    [SerializeField] int index = 0;
    float time = 0;


    // Start is called before the first frame update
    void Start()
    {
        csvData.Clear();
        LoadCSV("TransformData");

        replaySlider.maxValue = csvData.Count - 1;

        //StartCoroutine(Replay());
    }

    void Update()
    {
        if (endFlag)
        {
            return;
        }

        SetTransform(csvData[index]);
        ChangeSlider();
        time += Time.deltaTime;
        
        if (time > moveInterval)
        {
            time = 0;
            if (pauseFlag)
            {
                return;
            }

            index += 1;
            if (index >= csvData.Count)
            {
                index = 0;
                replaySlider.value = 0;
                endFlag = true;
            }
        }
    }

    void LoadCSV(string fileName)
    {
        // ResourcesフォルダからCSVファイルをロード
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile == null)
        {
            Debug.LogError("CSVファイルが見つかりません");
            return;
        }

        // CSVファイルの内容を一行ずつ処理
        StringReader reader = new StringReader(csvFile.text);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            // カンマで分割して配列に保存
            string[] fields = line.Split(',');
            csvData.Add(fields);
        }

        Debug.Log(csvData.Count);

        // データを確認
        //foreach (var row in csvData)
        //{
        //    Debug.Log(string.Join(", ", row));
        //}
    }

    //IEnumerator Replay()
    //{
    //    while (true)
    //    {
    //        if (endFlag)
    //        {
    //            break;
    //        }

    //        SetTransform(csvData[index]);

    //        if (pauseFlag)
    //        {
    //            continue;
    //        }

    //        index += 1;

    //        yield return new WaitForSeconds(moveInterval);

    //    }
    //}

    void SetTransform(string[] data)
    {
        float[] floatData = data
                  .Select(float.Parse)
                  .ToArray();

        carTrans.position = new Vector3(floatData[0], floatData[1], floatData[2]);
        carTrans.rotation = new Quaternion(floatData[3], floatData[4], floatData[5], floatData[6]);

        cameraTrans.localPosition = new Vector3(floatData[7], floatData[8], floatData[9]);
        cameraTrans.localRotation = new Quaternion(floatData[10], floatData[11], floatData[12], floatData[13]);
    }

    void ChangeSlider()
    {
        if (pauseFlag)
        {
            index = (int)(replaySlider.value);
            return;
        }

        replaySlider.value = index;
    }

    public void StopStartButton()
    {
        endFlag = false;
        if (pauseFlag)
        {
            pauseFlag = false;
        }
        else
        {
            pauseFlag = true;
        }
    }
}
