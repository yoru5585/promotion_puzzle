using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Transform cameraTrans;
    bool isRotation = false;
    int direction = 1;
    public float rotationSpeed = 100f;  // ��]���x
    public float decelerationRate = 2f;  // ������
    private float currentSpeed = 0f;  // ���݂̉�]���x
    private float sensitivity = 1.8f; // ������}�E�X���x

    private void Update()
    {

        isRotation = Input.GetMouseButton(1);

        // �{�^����������Ă���ԁA��]���x��ݒ肷��
        if (isRotation)
        {
            float mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;
            currentSpeed = mouse_move_x * rotationSpeed * direction;

            //currentSpeed = rotationSpeed * direction;
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
