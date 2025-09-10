using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateImage : MonoBehaviour
{
    // ȸ�� �ӵ��� �����ϴ� ����
    public float rotationSpeed = 45f; // �ʴ� 45�� ȸ��

    void Update()
    {
        // Y���� �������� ȸ��
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
