using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Transform cameraTrans;
    bool isRotation = false;
    int direction = 1;
    public float rotationSpeed = 100f;  // 回転速度
    public float decelerationRate = 2f;  // 減速率
    private float currentSpeed = 0f;  // 現在の回転速度
    private float sensitivity = 1.8f; // いわゆるマウス感度

    private void Update()
    {

        isRotation = Input.GetMouseButton(1);

        // ボタンが押されている間、回転速度を設定する
        if (isRotation)
        {
            float mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;
            currentSpeed = mouse_move_x * rotationSpeed * direction;

            //currentSpeed = rotationSpeed * direction;
        }
        else
        {
            // ボタンが離されたとき、徐々に回転速度を減少させる
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * decelerationRate);
        }

        // オブジェクトを回転させる
        cameraTrans.Rotate(Vector3.up, currentSpeed * Time.deltaTime);
    }

    public void Rotate(int direction)
    {
        isRotation = !isRotation;
        this.direction = direction;
    }

    public void Stop()
    {
        currentSpeed = 0;
        cameraTrans.rotation = new Quaternion(0, 0, 0, 1);
    }
}
