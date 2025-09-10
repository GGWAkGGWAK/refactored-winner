using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float PlayTime;
    int Min;
    public TextMeshPro text_Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayTime += Time.deltaTime;
        text_Timer.text = string.Format("{0:D2}:{1:D2}", Min, (int)PlayTime);
        if ((int)PlayTime > 59) //1���� 60�� �̱� ������ �ʴ� 59�� ������ ������ ����
        {
            PlayTime = 0; //sec�� �⺻���� 0
            Min++;  //sec�� 59���� Ŀ���� 1���� �ɶ� Min(��) �� Ŀ����.
        }
    }
}
