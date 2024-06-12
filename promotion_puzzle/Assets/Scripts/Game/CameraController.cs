using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Transform cameraTrans;
    bool isRotation;
    int direction;
    public float rotationSpeed = 100f;  // ��]���x
    public float decelerationRate = 2f;  // ������
    private float currentSpeed = 0f;  // ���݂̉�]���x

    private void Update()
    {
        // �{�^����������Ă���ԁA��]���x��ݒ肷��
        if (isRotation)
        {
            currentSpeed = rotationSpeed * direction;
        }
        else
        {
            // �{�^���������ꂽ�Ƃ��A���X�ɉ�]���x������������
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * decelerationRate);
        }

        // �I�u�W�F�N�g����]������
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
