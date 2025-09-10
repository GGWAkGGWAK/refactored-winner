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
        if ((int)PlayTime > 59) //1분은 60초 이기 때문에 초는 59초 까지로 범위를 설정
        {
            PlayTime = 0; //sec의 기본값은 0
            Min++;  //sec가 59보다 커질때 1분이 될때 Min(분) 은 커진다.
        }
    }
}
