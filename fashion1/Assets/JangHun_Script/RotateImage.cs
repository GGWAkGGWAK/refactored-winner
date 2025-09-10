using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateImage : MonoBehaviour
{
    // 회전 속도를 조절하는 변수
    public float rotationSpeed = 45f; // 초당 45도 회전

    void Update()
    {
        // Y축을 기준으로 회전
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
